using OctagonPlatform.Models.FormsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctagonPlatform.Models.InterfacesRepository
{
   public interface IBankAccountRepository
    {
        IEnumerable<BankAccount> GetAllBankAccount();

        UserEditFormViewModel BankAccountToEdit(int id);

        UserFormViewModel RenderBAFormViewModel();
        
        void SaveBankAccount(BAccountFVModel viewModel, string action);

        BankAccount BAccountDetails(int id);
        
        void DeleteBankAccount(int id);

        //UserFormViewModel InitializeNewFormViewModel(UserFormViewModel viewModel);

    }
}
