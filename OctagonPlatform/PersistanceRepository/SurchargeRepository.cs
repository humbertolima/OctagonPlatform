using AutoMapper;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OctagonPlatform.PersistanceRepository
{
    public class SurchargeRepository:GenericRepository<Surcharge>, ISurchargeRepository
    {
        public IEnumerable<Surcharge> GetAllSurcharges(int terminalId)
        {
            try
            {
                var surcharges = Table.Where(x => x.TerminalId == terminalId && !x.Deleted)
                    .Include(x => x.Terminal)
                    .Include(x => x.BankAccount)
                    .ToList();
                if(surcharges.Count <= 0) throw new Exception("Model not found. ");
                return surcharges;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Page not found. ");
            }
        }

        public SurchargeFormViewModel RenderSurchargeFormViewModel(int terminalId)
        {
            try
            {
                var terminal = Context.Terminals.SingleOrDefault(x => x.Id == terminalId);
                if (terminal == null) throw new Exception("Page not found. ");

                return new SurchargeFormViewModel()
                {
                    TerminalId = terminalId,
                    Terminal = Context.Terminals.SingleOrDefault(x => x.Id == terminalId && !x.Deleted),
                    BankAccounts = Context.BankAccounts.Where(x => x.PartnerId == terminal.PartnerId && !x.Deleted),
                    StartDate = DateTime.UtcNow,
                    StopDate = new DateTime(DateTime.UtcNow.Year + 1, DateTime.UtcNow.Month, DateTime.UtcNow.Day)
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Page not found. ");
            }
        }

        public SurchargeFormViewModel SurchargeToEdit(int id)
        {
            try
            {
                var surcharge = Table.Where(x => x.Id == id && !x.Deleted)
                    .Include(x => x.Terminal)
                    .Include(x => x.BankAccount).SingleOrDefault();

                if(surcharge == null) throw new Exception("Model not found. ");

                var result = Mapper.Map<Surcharge, SurchargeFormViewModel>(surcharge);
                result.Terminal = surcharge.Terminal;
                result.BankAccount = surcharge.BankAccount;
                result.BankAccounts = Context.BankAccounts.Where(x => x.PartnerId == surcharge.Terminal.PartnerId && !x.Deleted).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Page not found. ");
            }
        }

        public void SaveSurcharge(SurchargeFormViewModel viewModel, string action)
        {
            try
            {
                if (viewModel.StartDate > viewModel.StopDate) throw new Exception("Stop Date must be after Start Date");

                if (action == "Edit")
                {
                    var surcharge = Table.SingleOrDefault(x => x.Id == viewModel.Id && !x.Deleted);
                    if(surcharge == null) throw new Exception("Model not found. ");
                    Mapper.Map(viewModel, surcharge);
                    Edit(surcharge);
                }
                else
                {
                    var surcharge = Table.SingleOrDefault(x => (x.Id == viewModel.Id || x.BankAccountId == viewModel.BankAccountId) && !x.Deleted);
                    if (surcharge != null && !surcharge.Deleted) throw new Exception("This Terminal already has this Surcharge account. ");
                    if (surcharge != null && surcharge.Deleted)
                        Table.Remove(surcharge);
                    var result = Mapper.Map<SurchargeFormViewModel, Surcharge>(viewModel);
                    Add(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Page not found. ");
            }
        }

        public Surcharge SurchargeDetails(int id)
        {
            try
            {
                var surcharge = Table.Where(x => x.Id == id && !x.Deleted)
                    .Include(x => x.Terminal)
                    .Include(x => x.BankAccount)
                    .SingleOrDefault();
                if(surcharge == null) throw new Exception("Model not found. ");
                return surcharge;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Page not found. ");
            }
        }

        public void DeleteSurcharge(int id)
        {
            try
            {
                var surcharge = Table.SingleOrDefault(x => x.Id == id && !x.Deleted);
                if (surcharge == null) throw new Exception("Model not found. ");
                Delete(surcharge);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Page not found. ");
            }
        }

        public SurchargeFormViewModel InitializeNewSurchargeFormViewModel(SurchargeFormViewModel viewModel)
        {
            try
            {
                viewModel.Terminal = Context.Terminals.SingleOrDefault(x => x.Id == viewModel.TerminalId && !x.Deleted);
                viewModel.BankAccounts = Context.BankAccounts.Where(x => !x.Deleted && x.PartnerId == viewModel.Terminal.PartnerId).ToList();
                viewModel.StartDate = DateTime.Now;
                viewModel.StopDate = new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day);
                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Page not found. ");
            }
        }
    }
}