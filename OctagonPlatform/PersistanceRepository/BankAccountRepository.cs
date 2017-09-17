using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using System.Data.Entity;

namespace OctagonPlatform.PersistanceRepository
{
    public class BankAccountRepository : GenericRepository<BankAccount>, IBankAccountRepository
    {
        public IEnumerable<BankAccount> GetAllBankAccount()
        {
            var result = Table.Where(c => !c.Deleted)
                .Include(c => c.Partner)
                .Include(c => c.City)
                .Include(c => c.Country)
                .Include(c => c.State)
                .ToList();

            return result;
        }

        public BankAccount BAccountDetails(int id)
        {
            BankAccount bankAccount = new BankAccount();
            try
            {
                bankAccount  = Table
                .Include(c => c.Partner)
                .Include(c => c.City)
                .Include(c => c.Country)
                .Include(c => c.State)
                .Single(c => !c.Deleted && c.Id == id)
                ;
            }
            #region Exeption
            catch (ArgumentNullException Aex)
            {
                throw new Exception("Error Arguments is null. ", Aex);
            }
            catch (InvalidOperationException Iex)
            {
                throw new Exception("Error, invalid operation. ", Iex);
            }
#endregion

            return bankAccount;
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