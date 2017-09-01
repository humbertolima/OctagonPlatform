using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

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

            var viewModel = new UserFormViewModel()
            {
                Partners = Context.Partners.ToList(),
                Permissions = Context.Permissions.Select(p => new SelectListItem
                {
                    Group = new SelectListGroup {Name = p.Type },
                    Text = p.Name,
                    Value = p.Id.ToString()
                }).ToList(),
                SetOfPermissions = Context.SetOfPermissions.Include("Permissions").ToList()
            };

            return viewModel;
        }

        public UserEditFormViewModel UserToEdit(int id)
        {
            var result = Table.SingleOrDefault(c => c.Id == id);

            if (result != null)
            {
                return new UserEditFormViewModel()
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
            throw new System.Exception("User don't exist. ");
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
                    if(!string.IsNullOrEmpty(viewModel.Password))
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
            return Table.Where(x => x.Id == id)
                .Include(x => x.Partner)
                .Include(x => x.Permissions)
                //.Include(x => x.Partner.BankAccounts)
                //.Include(x => x.Partner.Terminals)
                .FirstOrDefault();
        }

        public void DeleteUser(int id)
        {
            Delete(id);
        }

        public UserFormViewModel InitializeNewFormViewModel(UserFormViewModel viewModel)
        {
            return new UserFormViewModel()
            {
                Email = viewModel.Email,
                Id = viewModel.Id,
                IsLocked = viewModel.IsLocked,
                LastName = viewModel.LastName,
                Name = viewModel.Name,
                PartnerId = viewModel.PartnerId,
                Partners = Context.Partners,
                Permissions = viewModel.Permissions,
                Phone = viewModel.Phone,
                Status = viewModel.Status,
                UserName = viewModel.UserName
            };
        }

        public async Task<User> Validate(User user)
        {
            var result = await Context.Users.SingleAsync(c => c.UserName == user.UserName);
            return result;
        }
    }
}