using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using System.Data.Entity;
using AutoMapper;

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
                bankAccount = Table
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

        public BAEditFVModel RenderBAFormViewModel()
        {
            BAEditFVModel viewModel = new BAEditFVModel();

            viewModel.Partners = Context.Partners.ToList();
            viewModel.Countries = Context.Countries.ToList();
            viewModel.States = Context.States.Where(x => x.CountryId == 231).ToList();
            viewModel.Cities = Context.Cities.Where(x => x.StateId == 3930).ToList();

            return viewModel;
        }

        public BAEditFVModel BankAccountToEdit(int id)
        {
            var model = Table
                .Include(c => c.Partner)
                .Include(c => c.Country)
                .Include(c => c.State)
                .Include(c => c.City)
                .Single(c => c.Id == id)
                ;
            var editViewModel = Mapper.Map<BankAccount, BAEditFVModel>(model);
            editViewModel.Partners = Context.Partners.ToList();
            editViewModel.Countries = Context.Countries.ToList();
            editViewModel.Cities = Context.Cities.ToList();
            editViewModel.States = Context.States.ToList();

            return editViewModel;
        }

        public void DeleteBankAccount(int id)
        {
            Delete(id);
        }

        public void SaveBankAccount(BAEditFVModel editViewModel, string action)
        {
            if (action == "Create")
            {
                var model = Table.SingleOrDefault(c => c.Id == editViewModel.Id);
                if (model == null)
                {
                    model = Mapper.Map<BAEditFVModel, BankAccount>(editViewModel);
                    Add(model);
                }
            }
            else
            {
                var model = Mapper.Map<BAEditFVModel, BankAccount>(editViewModel);
                //Revisar este codigo para que no salve si no se le hizo modificacion al entity.
                //db.Entry(bankAccount).State = EntityState.Modified;
                Edit(model);
            }
        }
    }
}