using OctagonPlatform.Models.FormsViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();

        UserEditFormViewModel UserToEdit(int id);

        UserEditFormViewModel RenderUserFormViewModel();

        void SaveUser(UserFormViewModel viewModel, string action);

        User UserDetails(int id);

        Task<User> Validate(User user);

        void DeleteUser(int id);

        UserFormViewModel InitializeNewFormViewModel(UserFormViewModel viewModel);
    }
}
