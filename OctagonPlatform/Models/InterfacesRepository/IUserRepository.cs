using OctagonPlatform.Models.FormsViewModels;
using System.Collections.Generic;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        IEnumerable<User> GetAllUsers(int partnerId);

        IEnumerable<User> Search(string search, int partnerId);

        UserEditFormViewModel UserToEdit(int id);

        UserFormViewModel RenderUserFormViewModel(int parentId);

        ICollection<Permission> AddPermissionToUser(string[] permissions);

        UserBAViewModel GetAllBankAccount(string userId, bool toAttach);

        UserBAViewModel AddBankAccountToUser(string userId, string bankAccountId);

        List<Terminal> AddTerminalToUser(int terminalId, int userId);

        List<Terminal> DeleteTerminalToUser(int terminalId, int userId);

        UserBAViewModel DeAttachBankAccountToUser(int userId, int bankAccountId);

        void SaveUser(UserFormViewModel viewModel, string action);

        User UserDetails(int id);

        //Task<User> Validate(User user);

        void DeleteUser(int id);

        UserFormViewModel InitializeNewFormViewModel(UserFormViewModel viewModel);
        IEnumerable<dynamic> GetAllUser(string term,int partnerId);
        User GetReportsUser(int Id);
    }
}
