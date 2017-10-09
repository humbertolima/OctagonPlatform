using AutoMapper;
using OctagonPlatform.Helpers;
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
                if(vaulcash == null) throw new Exception("Vault Cash not found.");
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
                    TerminalId = terminalId,
                    Terminal = Context.Terminals.SingleOrDefault(x => x.Id == terminalId && !x.Deleted),
                    BankAccounts = Context.BankAccounts.Where(x => !x.Deleted).ToList(),
                    SettledType = Settled.SettledType.Monthly,
                    StartDate = DateTime.Now,
                    StopDate = DateTime.Now
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
            throw new NotImplementedException();
        }

        public VaultCash VaultCashDetails(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteVaultCash(int id)
        {
            throw new NotImplementedException();
        }

        public VaultCashFormViewModel InitializeNewVaultCashFormViewModel(VaultCashFormViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}