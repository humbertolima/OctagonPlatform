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
            try
            {
                var parent = Context.Partners.SingleOrDefault(x => x.Id == partnerId && !x.Deleted);
                if (parent == null) throw new Exception("Parent not found. ");

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
                throw new Exception(ex.Message + ", Bank accounts not found. ");
            }

        }

        public BankAccount BAccountDetails(int id)
        {
            try
            {
                
                var bankAccount = Table
                    .Include(c => c.Partner)
                    .Include(c => c.City)
                    .Include(c => c.Country)
                    .Include(c => c.State)
                    .SingleOrDefault(c => !c.Deleted && c.Id == id);

                if(bankAccount == null) throw new Exception("Bank account not found. ");
                return bankAccount;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Bank account not found. ");
            }

        }

        public BAEditFVModel RenderBaFormViewModel(int partnerId)
        {
            try
            {
                var partner = Context.Partners.SingleOrDefault(x => x.Id == partnerId && !x.Deleted);
                if (partner == null) throw new Exception("Bank account not found. ");

                var viewModel = new BAEditFVModel
                {
                    Partners = Context.Partners.Where(x => (x.Id == partnerId || x.ParentId == partnerId) && !x.Deleted).ToList(),
                    Partner = partner,
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
                throw new Exception(ex.Message + "Bank account not found. ");
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
                if (model == null) throw new Exception("Bank account does not exist in our records. ");
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
                throw new Exception(ex.Message + "Bank account not found. ");
            }

        }

        public void DeleteBankAccount(int id)
        {
            try
            {
                var bankaccount = Table.SingleOrDefault(x => x.Id == id && !x.Deleted);
                if(bankaccount == null) throw new Exception("Bank account not found. ");
                Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Bank account not found. ");
            }

        }

        public void SaveBankAccount(BAEditFVModel editViewModel, string action)
        {
            try
            {
                var current = Table.SingleOrDefault(c => c.AccountNumber == editViewModel.AccountNumber || c.RoutingNumber == editViewModel.RoutingNumber || c.FedTax == editViewModel.FedTax || c.Ssn == editViewModel.Ssn || c.NickName == editViewModel.NickName.Trim());

                if (action == "Edit")
                {
                    var model = Table.SingleOrDefault(c => c.Id == editViewModel.Id && !c.Deleted);
                    if (model == null) throw new Exception("Bank account not found, check entered values. ");
                    if (current != null)
                    {
                        if(!current.Deleted && current.Id != editViewModel.Id)
                            throw new Exception("Bank account already exists. ");
                        
                    }
                    {
                        
                        Mapper.Map(editViewModel, model);
                        Edit(model);
                    }
                }
                else
                {
                    if (current != null)
                    {
                        if (!current.Deleted)
                            throw new Exception("Bank account already exists. ");


                    }

                    var modelNew = Mapper.Map<BAEditFVModel, BankAccount>(editViewModel);
                    
                    Add(modelNew);
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Please check entered values. ");
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
                throw new Exception(ex.Message + "Bank account not found. ");
            }
        }
    }
}