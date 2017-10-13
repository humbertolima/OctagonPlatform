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
        public IEnumerable<BankAccount> GetAllBankAccount(int partnerId)
        {
            try { 
            var result = Table.Where(c => !c.Deleted && c.PartnerId == partnerId)
                .Include(c => c.Partner)
                .Include(c => c.City)
                .Include(c => c.Country)
                .Include(c => c.State)
                .ToList();

            return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public BankAccount BAccountDetails(int id)
        {
            try { 
            BankAccount bankAccount;
            try
            {
                bankAccount = Table
                    .Include(c => c.Partner)
                    .Include(c => c.City)
                    .Include(c => c.Country)
                    .Include(c => c.State)
                    .Single(c => !c.Deleted && c.Id == id);

            }
            #region Exeption
            catch (ArgumentNullException aex)
            {
                throw new Exception("Error Arguments is null. ", aex);
            }
            catch (InvalidOperationException iex)
            {
                throw new Exception("Error, invalid operation. ", iex);
            }
            #endregion

            return bankAccount;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public BAEditFVModel RenderBaFormViewModel(int partnerId)
        {
            try
            {
                var viewModel = new BAEditFVModel
                {
                    Partners = Context.Partners.Where(x => (x.Id == partnerId || x.ParentId == partnerId) && !x.Deleted).ToList(),
                    Partner = Context.Partners.SingleOrDefault(x => x.Id == partnerId),
                    Countries = Context.Countries.ToList(),
                    States = Context.States.Where(x => x.CountryId == 231).ToList(),
                    Cities = Context.Cities.Where(x => x.StateId == 3930).ToList(),
                    Status = StatusType.Status.Active,
                    AccountType = AccountType.TypeName.Checkings
                };

                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public BAEditFVModel BankAccountToEdit(int id)
        {
            try
            {
                var model = Table.Where(x => x.Id == id && !x.Deleted)
                    .Include(c => c.Partner)
                    .Include(c => c.Country)
                    .Include(c => c.State)
                    .Include(c => c.City)
                    .Single(c => c.Id == id);
                if (model == null) throw new Exception("BankAccount does not exists in our records!!!");
                {
                    var editViewModel = Mapper.Map<BankAccount, BAEditFVModel>(model);
                    editViewModel.Partners = Context.Partners.Where(x => (x.Id == model.PartnerId || x.ParentId == model.PartnerId) && !x.Deleted)
                        .ToList();
                    editViewModel.Partner = model.Partner;
                    editViewModel.Countries = Context.Countries.ToList();
                    editViewModel.States = Context.States.Where(x => x.CountryId == model.CountryId).ToList();
                    editViewModel.Cities = Context.Cities.Where(x => x.StateId == model.StateId).ToList();

                    return editViewModel;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void DeleteBankAccount(int id)
        {
            try
            {
                Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void SaveBankAccount(BAEditFVModel editViewModel, string action)
        {
            try
            {

                if (action == "Edit")
                {
                    var model = Table.SingleOrDefault(c => c.Id == editViewModel.Id && !c.Deleted);
                    if (model == null) throw new Exception("BankAccount does not exist in our records!!!");
                    {
                        
                        Mapper.Map(editViewModel, model);
                        Edit(model);
                    }
                }
                else
                {
                    var model = Table.SingleOrDefault(c => c.AccountNumber == editViewModel.AccountNumber || c.RoutingNumber == editViewModel.RoutingNumber || c.FedTax == editViewModel.FedTax || c.Ssn == editViewModel.Ssn || c.NickName == editViewModel.NickName);
                    if(model != null && !model.Deleted) throw new Exception("BankAccount already exists in our records!!!");

                    if (model != null && model.Deleted) Table.Remove(model);
                        


                    var model1 = Mapper.Map<BAEditFVModel, BankAccount>(editViewModel);
                    
                    Add(model1);
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<BankAccount> Search(string search, int partnerId)
        {
            try
            {
                var result = GetAllBankAccount(partnerId)
                    .Where(c => c.NickName.Contains(search) || c.NameOnCheck.Contains(search));
                    
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}