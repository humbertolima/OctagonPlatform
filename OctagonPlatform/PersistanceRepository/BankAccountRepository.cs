using AutoMapper;
using OctagonPlatform.Helpers;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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

        public BAEditFVModel RenderBaFormViewModel(int partnerId)
        {
            BAEditFVModel viewModel = new BAEditFVModel();

            viewModel.Partners = Context.Partners.ToList();
            viewModel.Partner = Context.Partners.SingleOrDefault(x => x.Id == partnerId);
            viewModel.Countries = Context.Countries.ToList();
            viewModel.States = Context.States.Where(x => x.CountryId == 231).ToList();
            viewModel.Cities = Context.Cities.Where(x => x.StateId == 3930).ToList();
            viewModel.Status = StatusType.Status.Active;
            viewModel.AccountType = AccountType.TypeName.Checkings;
            return viewModel;
        }

        public BAEditFVModel BankAccountToEdit(int id)
        {
            var model = Table.Where(x => x.Id == id)
                .Include(c => c.Partner)
                .Include(c => c.Country)
                .Include(c => c.State)
                .Include(c => c.City)
                .Single(c => c.Id == id)
                ;
            var editViewModel = Mapper.Map<BankAccount, BAEditFVModel>(model);
            editViewModel.Partners = Context.Partners.ToList();
            editViewModel.Partner = model.Partner;
            editViewModel.Countries = Context.Countries.ToList();
            editViewModel.States = Context.States.Where(x => x.CountryId == model.CountryId).ToList();
            editViewModel.Cities = Context.Cities.Where(x => x.StateId == model.StateId).ToList();

            return editViewModel;
        }

        public void DeleteBankAccount(int id)
        {
            Delete(id);
        }

        public void SaveBankAccount(BAEditFVModel editViewModel, string action)
        {
            if (action == "Edit")
            {
                var model = Table.SingleOrDefault(c => c.Id == editViewModel.Id);
                if (model == null) return;
                {
                    Mapper.Map(editViewModel, model);
                    Edit(model);
                }
            }
            else
            {
                var model = Mapper.Map<BAEditFVModel, BankAccount>(editViewModel);
                //Revisar este codigo para que no salve si no se le hizo modificacion al entity.
                //db.Entry(bankAccount).State = EntityState.Modified;
                Add(model);
            }
        }

        public IEnumerable<BAccountFVModel> Search(string search)
        {
            var bankAccounts = Table.Where(c => !c.Deleted && (c.NickName.Contains(search) || c.Partner.BusinessName.Contains(search)))
                .Include(x => x.Partner)
                .ToList();

            List<BAccountFVModel> viewModel = new List<BAccountFVModel>();

            foreach (var item in bankAccounts)
            {   //creado porque no se puede mapear una lista de tipos de objetos. Solo se mapea un tipo de objeto.
                viewModel.Add(Mapper.Map<BankAccount, BAccountFVModel>(item));
            }

            return viewModel;
        }
    }
}