using OctagonPlatform.Models.FormsViewModels;
using System.Collections.Generic;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface IBankAccountRepository
    {
        IEnumerable<BankAccount> GetAllBankAccount();

        BAEditFVModel BankAccountToEdit(int id);

        BAEditFVModel RenderBaFormViewModel(int partnerId);
        
        void SaveBankAccount(BAEditFVModel viewModel, string action);

        BankAccount BAccountDetails(int id);
        
        void DeleteBankAccount(int id);

        IEnumerable<BankAccount> Search(string search);

    }
}
