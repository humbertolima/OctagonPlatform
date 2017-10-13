using AutoMapper;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OctagonPlatform.PersistanceRepository
{
    public class VaultCashRespository: GenericRepository<VaultCash>, IVaultCashRepository
    {
        public VaultCash GetVaultCash(int terminalId)
        {
            try
            {
                var vaulcash = Table.Where(x => x.Id == terminalId && !x.Deleted)
                    .Include(x => x.Terminal)
                    .Include(x => x.BankAccount)
                    .SingleOrDefault();
                return vaulcash;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public VaultCashFormViewModel RenderVaultCashFormViewModel(int terminalId)
        {
            try
            {
                return new VaultCashFormViewModel()
                {
                    Id = terminalId,
                    Terminal = Context.Terminals.SingleOrDefault(x => x.Id == terminalId && !x.Deleted),
                    BankAccounts = Context.BankAccounts.Where(x => !x.Deleted).ToList(),
                    StartDate = DateTime.Now,
                    StopDate = new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day)
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public VaultCashFormViewModel VaultCashToEdit(int id)
        {
            try
            {
                var vaultcash = Table.Where(x => x.Id == id && !x.Deleted)
                    .Include(x => x.BankAccount)
                    .Include(x => x.Terminal)
                    .SingleOrDefault();
                if(vaultcash == null) throw new HttpException("Vault cash not found.");
                var result = Mapper.Map<VaultCash, VaultCashFormViewModel>(vaultcash);
                result.Terminal = vaultcash.Terminal;
                result.BankAccount = vaultcash.BankAccount;
                result.BankAccounts = Context.BankAccounts.Where(x => !x.Deleted).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SaveVaultCash(VaultCashFormViewModel viewModel, string action)
        {
            try
            {
                if(viewModel.StartDate > viewModel.StopDate) throw new Exception("Stop Date must be after Start Date");

                if (action == "Edit")
                {
                    var vaultcashToEdit = Table.SingleOrDefault(x => x.Id == viewModel.Id && !x.Deleted);
                    if(vaultcashToEdit == null) throw new Exception("Vault Cash does not exist in our records. ");
                    Mapper.Map(viewModel, vaultcashToEdit);
                    Edit(vaultcashToEdit);
                }
                else
                {
                    var vaultcash = Table.SingleOrDefault(x => x.Id == viewModel.Id);
                    if(vaultcash != null && !vaultcash.Deleted) throw new Exception("This Terminal already has a Vaultcash account. ");
                    if (vaultcash != null && vaultcash.Deleted)
                        Table.Remove(vaultcash);
                    var vaultcashNew = Mapper.Map<VaultCashFormViewModel, VaultCash>(viewModel);
                    Add(vaultcashNew);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public VaultCash VaultCashDetails(int id)
        {
            try
            {
                var vaulcash = Table.Where(x => x.Id == id && !x.Deleted)
                    .Include(x => x.Terminal)
                    .Include(x => x.BankAccount)
                    .SingleOrDefault();
                if (vaulcash == null) throw new Exception("Vault Cash not found.");
                return vaulcash;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteVaultCash(int id)
        {
            try
            {
                var vaulcash = Table.SingleOrDefault(x => x.Id == id && !x.Deleted);
                if (vaulcash == null) throw new Exception("Vault Cash not found.");
                Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public VaultCashFormViewModel InitializeNewVaultCashFormViewModel(VaultCashFormViewModel viewModel)
        {
            try
            {
                viewModel.Terminal = Context.Terminals.SingleOrDefault(x => x.Id == viewModel.Id && !x.Deleted);
                viewModel.BankAccounts = Context.BankAccounts.Where(x => !x.Deleted).ToList();
                viewModel.StartDate = DateTime.Now;
                viewModel.StopDate = new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day);
                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}