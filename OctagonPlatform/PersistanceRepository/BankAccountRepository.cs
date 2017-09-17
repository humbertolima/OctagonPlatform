using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using System.Data.Entity;
using System.Threading.Tasks;

namespace OctagonPlatform.PersistanceRepository
{
    public class BankAccountRepository : GenericRepository<BankAccount>, IBankAccountRepository
    {

        public IEnumerable<BankAccount> GetAllBankAccount()
        {
            var result = Table.Where(c => !c.Deleted)
                .ToList();

            return result;
        }

        public BankAccount BAccountDetails(int id)
        {
            throw new NotImplementedException();
        }

        public UserEditFormViewModel BankAccountToEdit(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteBankAccount(int id)
        {
            throw new NotImplementedException();
        }


        public UserFormViewModel RenderBAFormViewModel()
        {
            throw new NotImplementedException();
        }

        public void SaveBankAccount(BAccountFVModel viewModel, string action)
        {
            throw new NotImplementedException();
        }
    }
}