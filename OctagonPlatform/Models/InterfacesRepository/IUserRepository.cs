using OctagonPlatform.Models.FormsViewModels;
using System.Collections.Generic;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();

        UserFormViewModel UserToEdit(int id);

        void SaveUser(UserFormViewModel viewModel, string action);

        User UserDetails();

        void DeleteUser(int id);
    }
}
