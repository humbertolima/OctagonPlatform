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

        BAEditFVModel BankAccountToEdit(int id);

        BAEditFVModel RenderBAFormViewModel();
        
        void SaveBankAccount(BAEditFVModel viewModel, string action);

        BankAccount BAccountDetails(int id);
        
        void DeleteBankAccount(int id);

        IEnumerable<BAccountFVModel> Search(string search);

    }
}
