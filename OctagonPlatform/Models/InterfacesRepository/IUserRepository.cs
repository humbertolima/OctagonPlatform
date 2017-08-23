using OctagonPlatform.Models.FormsViewModels;
using System.Collections.Generic;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();

        UserFormViewModel UserToEdit(int id);

        UserFormViewModel RenderUserFormViewModel();

        void SaveUser(UserFormViewModel viewModel, string action);

        User UserDetails(int id);

        void DeleteUser(int id);
    }
}
