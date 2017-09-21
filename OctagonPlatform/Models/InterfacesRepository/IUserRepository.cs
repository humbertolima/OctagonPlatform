using OctagonPlatform.Models.FormsViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();

        IEnumerable<User> Search(string search);

        UserEditFormViewModel UserToEdit(int id);

        UserFormViewModel RenderUserFormViewModel();

        ICollection<Permission> AddPermissionToUser(string[] permissions);

        void AddBankAccountToUser(int userId, string[] bankAccounts);

        void SaveUser(UserFormViewModel viewModel, string action);

        User UserDetails(int id);

        //Task<User> Validate(User user);

        void DeleteUser(int id);

        UserFormViewModel InitializeNewFormViewModel(UserFormViewModel viewModel);
        
    }
}
