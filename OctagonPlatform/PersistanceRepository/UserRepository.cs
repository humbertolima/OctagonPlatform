using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System.Collections.Generic;
using System.Linq;

namespace OctagonPlatform.PersistanceRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
       
        public IEnumerable<User> GetAllUsers()
        {
            var result = Table
                //.Include("ReportsGroups")
                //.Include("Reports")
                .Select(c => c)
                .Where(c => c.Deleted == false)
                .ToList();

            return result;
        }

        public UserFormViewModel UserToEdit(int id)
        {
            var result = Table.SingleOrDefault(c => c.Id == id);

            if (result != null)
            {
                var userForm = new UserFormViewModel()
                {
                    Email = result.Email,
                    Id = result.Id,
                    IsLocked = result.IsLocked,
                    LastName = result.LastName,
                    Name = result.Name,
                    Partner = result.Partner,
                    PartnerId = result.PartnerId,
                    Permissions = result.Permissions,
                    Phone = result.Phone,
                    Status = result.Status,
                    UserName = result.UserName
                };

                return userForm;
            }
            return RenderUserFormViewModel();
        }

        public void SaveUser(UserFormViewModel viewModel, string action)
        {
            if (action == "Edit")
            {

                var result = Table.SingleOrDefault(c => c.Id == viewModel.Id);

                if (result != null)
                {

                    result.Email = viewModel.Email;
                    result.LastName = viewModel.LastName;
                    result.Name = viewModel.Name;
                    result.Phone = viewModel.Phone;
                    result.Status = viewModel.Status;
                    result.UserName = viewModel.UserName;

                    Edit(result);
                }
            }
            else if (action == "Create")
            {
                var user = new User()
                {
                    PartnerId = viewModel.PartnerId,
                    Email = viewModel.Email,
                    LastName = viewModel.LastName,
                    Name = viewModel.Name,
                    Password = viewModel.Password, // crear metodo privado que haga hash de la BD
                    Phone = viewModel.Phone,
                    Status = viewModel.Status,
                    UserName = viewModel.UserName,
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

        public UserFormViewModel RenderUserFormViewModel()
        {
            //Revisar que dependencias tienen los usuarios para mostrar ademas de sus datos.
            // reportes y grupos.
            //alertas y notificaciones.

            return new UserFormViewModel();
        }

        

    }
}