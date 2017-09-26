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

        List<BankAccount> GetAllBankAccount(string userId);

        void AddBankAccountToUser(string userId, string[] bankAccounts);

        User DeattachBankAccountToUser(int userId, int bankAccountID, string[] bankAccounts);

        void SaveUser(UserFormViewModel viewModel, string action);

        User UserDetails(int id);

        //Task<User> Validate(User user);

        void DeleteUser(int id);

        UserFormViewModel InitializeNewFormViewModel(UserFormViewModel viewModel);
        
    }
}
