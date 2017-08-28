using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Web;

namespace OctagonPlatform.PersistanceRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {

        public IEnumerable<User> GetAllUsers()
        {
            return Table.Where(u => u.Deleted == false)  //Seleccionar los que no esten borrados. Bloqueados sis
                                                         //.Include(x => x.Alerts)
                                                         //.Include(x => x.Reports)
                .Include(x => x.Partner)
                .ToList();
        }

        public UserFormViewModel RenderUserFormViewModel()
        {
            //Revisar que dependencias tienen los usuarios para mostrar ademas de sus datos.
            // reportes y grupos.
            //alertas y notificaciones.

            return new UserFormViewModel()
            {
                Partners = Context.Partners.ToList(),
                Permissions = new List<Permission>()
            };
        }

        public UserFormViewModel UserToEdit(int id)
        {
            var result = Table.SingleOrDefault(c => c.Id == id);

            if (result != null)
            {
                return new UserFormViewModel()
                {
                    Email = result.Email,
                    Id = result.Id,
                    IsLocked = result.IsLocked,
                    LastName = result.LastName,
                    Name = result.Name,
                    PartnerId = result.PartnerId,
                    Partners = Context.Partners,
                    Permissions = result.Permissions,
                    Phone = result.Phone,
                    Status = result.Status,
                    UserName = result.UserName
                };

            }
            return RenderUserFormViewModel();
        }

        public void SaveUser(UserFormViewModel viewModel, string action)
        {

            if (action == "Edit")
            {
                var user = Table.SingleOrDefault(c => c.Id == viewModel.Id);

                if (user != null)
                {
                    user.Email = viewModel.Email.Trim();
                    user.LastName = viewModel.LastName.Trim();
                    user.Name = viewModel.Name.Trim();
                    user.Phone = viewModel.Phone.Trim();
                    user.Status = viewModel.Status;
                    user.UserName = viewModel.UserName.Trim();
                    user.IsLocked = viewModel.IsLocked;
                    user.PartnerId = viewModel.PartnerId;
                    user.Password = viewModel.Password.Trim();
                    
                    Edit(user);
                }
            }
            else if (action == "Create")
            {
                //pongo en single y con el delete = false para que cuando se seleccione un userName y existe dos usuarios iguales con delete true, el single da un Exception por venir mas de dos. 

                var user = new User()
                {
                    PartnerId = viewModel.PartnerId,
                    Email = viewModel.Email.Trim(),
                    LastName = viewModel.LastName.Trim(),
                    Name = viewModel.Name.Trim(),
                    Password = viewModel.Password.Trim(), // crear metodo privado que haga hash de la BD
                    Phone = viewModel.Phone.Trim(),
                    Status = viewModel.Status,
                    UserName = viewModel.UserName.Trim(),
                    IsLocked = viewModel.IsLocked,
                    
                };

                Add(user);
            }
        }

        public User UserDetails(int id)
        {
            return new User();
        }

        public void DeleteUser(int id)
        {
            Delete(id);
        }

        public async Task<User> Validate(User user)
        {
            var result = await Context.Users.SingleAsync(c => c.UserName == user.UserName);
            return result;
        }
    }
}