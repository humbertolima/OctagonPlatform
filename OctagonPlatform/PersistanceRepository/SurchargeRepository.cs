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
                    StartDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day + 1),
                    StopDate = new DateTime(DateTime.UtcNow.Year + 1, DateTime.UtcNow.Month, DateTime.UtcNow.Day),
                    SettledType = SurchargeSettled.SettledType.Daily
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
                var surchargeDefault = Table.SingleOrDefault(x => x.BankAccountId == viewModel.BankAccountId);
                

                if (action == "Edit")
                {


                    var surcharge = Table.SingleOrDefault(x => x.Id == viewModel.Id);
                    if (surcharge == null) throw new Exception("Surcharge not found. ");

                    var sumAmountFee = Table.Where(x => x.Id != surcharge.Id).Sum(x => x.SplitAmount) + viewModel.SplitAmount;
                    var sumAmoutPercent = Table.Where(x => x.Id != surcharge.Id).Sum(x => x.SplitAmountPercent) + viewModel.SplitAmountPercent;

                    var terminal = Context.Terminals.SingleOrDefault(x => x.Id == surcharge.TerminalId);
                    if (terminal == null) throw new Exception("This terminal does not exist. ");
                    {
                        if (sumAmountFee > terminal.SurchargeAmountFee)
                            throw new Exception("This Amount Fee is biger that the remaining Surcharge Amount");
                        if (sumAmoutPercent > terminal.SurchargePercentageFee)
                            throw new Exception("This Percent Amount Fee is biger that the remaining Surcharge Amount");

                    }

                    if (surchargeDefault != null)
                    {
                        if (!surchargeDefault.Deleted && surchargeDefault.Id != surcharge.Id)
                            throw new Exception("This Terminal already has a Surcharge with the same Bank Account. ");
                        if(surchargeDefault.Deleted)
                            Table.Remove(surchargeDefault);

                    }

                    
                    Mapper.Map(viewModel, surcharge);
                    Edit(surcharge);
                }
                else
                {
                    var sumAmountFee = Table.Sum(x => x.SplitAmount) + viewModel.SplitAmount;
                    var sumAmoutPercent = Table.Sum(x => x.SplitAmountPercent) + viewModel.SplitAmountPercent;

                    var terminal = Context.Terminals.SingleOrDefault(x => x.Id == viewModel.TerminalId);
                    if (terminal == null) throw new Exception("This terminal does not exist. ");
                    {
                        if (sumAmountFee > terminal.SurchargeAmountFee)
                            throw new Exception("This Amount Fee is biger that the remaining Surcharge Amount");
                        if (sumAmoutPercent > terminal.SurchargePercentageFee)
                            throw new Exception("This Percent Amount Fee is biger that the remaining Surcharge Amount");

                    }

                    if (surchargeDefault != null)
                    {
                        if (!surchargeDefault.Deleted)
                            throw new Exception("This Terminal already has this Surcharge account. ");
                        Table.Remove(surchargeDefault);
                    }
                    
                    
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
                var surcharge = Table.Where(x => x.Id == id && !x.Deleted)
                    .Include(x => x.Terminal).SingleOrDefault();
                
                if (surcharge == null) throw new Exception("Model not found. ");
                surcharge.Terminal.RemainingSurchargeAmountFee += surcharge.SplitAmount;
                surcharge.Terminal.RemainingSurchargePercentFee += surcharge.SplitAmountPercent;
                GenericRepository<Terminal> t = new TerminalRepository();
                t.Edit(surcharge.Terminal);
                Delete(id);
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
                viewModel.StartDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month,
                    DateTime.UtcNow.Day + 1);
                viewModel.StopDate = new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day);
                viewModel.SettledType = SurchargeSettled.SettledType.Daily;
                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Page not found. ");
            }
        }
    }
}