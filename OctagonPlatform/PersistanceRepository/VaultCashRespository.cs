﻿using AutoMapper;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OctagonPlatform.PersistanceRepository
{
    public class VaultCashRespository : GenericRepository<VaultCash>, IVaultCashRepository
    {
        public VaultCash GetVaultCash(int terminalId)
        {
            try
            {
                var terminal = Context.Terminals.SingleOrDefault(x => x.Id == terminalId && !x.Deleted);
                if (terminal == null) throw new Exception("Terminal not found. ");

                var vaulcash = Table.Where(x => x.Id == terminalId && !x.Deleted)
                    .Include(x => x.Terminal)
                    .Include(x => x.BankAccount)
                    .SingleOrDefault();
                return vaulcash;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Vaultcash not found. ");
            }
        }

        public VaultCashFormViewModel RenderVaultCashFormViewModel(int terminalId)
        {
            try
            {
                var terminal = Context.Terminals.SingleOrDefault(x => x.Id == terminalId);
                if (terminal == null) throw new Exception("Page not found. ");

                return new VaultCashFormViewModel()
                {
                    Id = terminalId,
                    Terminal = Context.Terminals.SingleOrDefault(x => x.Id == terminalId && !x.Deleted),
                    BankAccounts = Context.BankAccounts.Where(x => !x.Deleted && x.PartnerId == terminal.PartnerId).ToList(),
                    StartDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day + 1),
                    StopDate = new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day)


                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Vaultcash not found. ");
            }
        }

        public VaultCashFormViewModel VaultCashToEdit(int id)
        {
            try
            {
                VaultCash vaultcash = Table.Where(x => x.Id == id && !x.Deleted)
                    .Include(x => x.BankAccount)
                    .Include(x => x.Terminal)
                    .SingleOrDefault();
                if (vaultcash == null) throw new HttpException("Vault cash not found.");
                var result = Mapper.Map<VaultCash, VaultCashFormViewModel>(vaultcash);
                result.Terminal = vaultcash.Terminal;
                result.BankAccount = vaultcash.BankAccount;
                result.BankAccounts = Context.BankAccounts.Where(x => !x.Deleted && x.PartnerId == vaultcash.Terminal.PartnerId).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Vaultcash not found. ");
            }
        }

        public void SaveVaultCash(VaultCashFormViewModel viewModel, string action)
        {
            try
            {
                if (viewModel.StartDate > viewModel.StopDate) throw new Exception("Stop Date must be after Start Date");

                if (action == "Edit")
                {
                    var vaultcashToEdit = Table.SingleOrDefault(x => x.Id == viewModel.Id && !x.Deleted);
                    if (vaultcashToEdit == null) throw new Exception("Vault Cash does not exist in our records. ");
                    Mapper.Map(viewModel, vaultcashToEdit);
                    Edit(vaultcashToEdit);
                }
                else
                {
                    var vaultcash = Table.SingleOrDefault(x => x.Id == viewModel.Id && !x.Deleted);
                    if (vaultcash != null && !vaultcash.Deleted) throw new Exception("This Terminal already has a Vaultcash account. ");
                    if (vaultcash != null && vaultcash.Deleted)
                        Table.Remove(vaultcash);
                    var vaultcashNew = Mapper.Map<VaultCashFormViewModel, VaultCash>(viewModel);
                    Add(vaultcashNew);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Please check the entered values. ");
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
                throw new Exception(ex.Message + "Vaultcash not found. ");
            }
        }

        public void DeleteVaultCash(int id)
        {
            try
            {
                var vaulcash = Table.SingleOrDefault(x => x.Id == id);
                if (vaulcash == null) throw new Exception("Vault Cash not found.");
                //Delete(id);       //no puede ser softdelete porque la relacion con terminal en BD es de uno a uno y cuando se agrega
                //otro vaultcash a la misma terminal, sale error en primary Key que no puede estar duplicado.s
                Table.Remove(vaulcash);
                Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Vaultcash not found. ");
            }
        }

        public VaultCashFormViewModel InitializeNewVaultCashFormViewModel(VaultCashFormViewModel viewModel)
        {
            try
            {
                if (viewModel == null) throw new Exception("Model not found. ");

                viewModel.Terminal = Context.Terminals.SingleOrDefault(x => x.Id == viewModel.Id && !x.Deleted);
                viewModel.BankAccounts = Context.BankAccounts.Where(x => !x.Deleted && x.PartnerId == viewModel.Terminal.PartnerId).ToList();
                viewModel.StartDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month,
                    DateTime.UtcNow.Day + 1);
                viewModel.StopDate = new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day);
                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Vaultcash not found. ");
            }
        }
    }
}