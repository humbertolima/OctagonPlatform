using AutoMapper;
using OctagonPlatform.Helpers;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using InterChange = OctagonPlatform.Models.InterChange;

namespace OctagonPlatform.PersistanceRepository
{
    public class InterChangeRepository:GenericRepository<InterChange>, IInterChangeRepository
    {
        public IEnumerable<InterChange> GetAllInterChanges(int terminalId)
        {
            try
            {
                var terminal = Context.Terminals.SingleOrDefault(x => x.Id == terminalId && !x.Deleted);
                if (terminal == null) throw new Exception("Terminal not found. ");

                var result = Table.Where(x => x.TerminalId == terminalId && !x.Deleted)
                    .Include(x => x.Terminal)
                    .Include(x => x.BankAccount).ToList();
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message + " InterChanges not found. ");
            }
        }

        public InterChangeFormViewModel RenderInterChangeFormViewModel(int terminalId)
        {
            try
            {
                var terminal = Context.Terminals.SingleOrDefault(x => x.Id == terminalId && !x.Deleted);
                if (terminal == null) throw new Exception("InterChange not found. ");

                return new InterChangeFormViewModel()
                {
                    TerminalId = terminalId,
                    Terminal = terminal,
                    CalculationMethod = CalculationMethod.Method.PerTransaction,
                    BankAccounts = Context.BankAccounts.Where(x => x.PartnerId == terminal.PartnerId && !x.Deleted).ToList(),
                    StartDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day + 1),
                    StopDate = new DateTime(DateTime.UtcNow.Year + 1, DateTime.UtcNow.Month, DateTime.UtcNow.Day)
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + " InterChange not found. ");
            }
        }

        public InterChangeFormViewModel InterChangeToEdit(int id)
        {
            try
            {
                var interChange = Table.Where(x => x.Id == id && !x.Deleted)
                    .Include(x => x.Terminal).SingleOrDefault();
                if(interChange == null) throw new Exception("InterChange not found. ");

                var interChangeToEdit = Mapper.Map<InterChange, InterChangeFormViewModel>(interChange);

                interChangeToEdit.BankAccounts =
                    Context.BankAccounts.Where(x => !x.Deleted && x.PartnerId == interChange.Terminal.PartnerId).ToList();
                return interChangeToEdit;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message + " InterChange not found. ");
            }
        }

        public void SaveInterChange(InterChangeFormViewModel viewModel, string action)
        {
            try
            {
                CheckSplitAmountsEntry.CheckInterChange(viewModel.TerminalId, viewModel.Id, viewModel.SplitAmount);

                if (viewModel.StartDate > viewModel.StopDate) throw new Exception("Stop Date must be after Start Date. ");

                var interChange = Table.SingleOrDefault(x => x.BankAccountId == viewModel.BankAccountId);
                if (action == "Edit")
                {
                    var interChangeToEdit = Table.SingleOrDefault(x => x.Id == viewModel.Id && !x.Deleted);
                    if (interChangeToEdit == null) throw new Exception("InterChange not found. ");




                    if (interChange != null)
                    {
                        if (interChange.Id != interChangeToEdit.Id && !interChange.Deleted)
                            throw new Exception("This Terminal already has this InterChange account. ");
                        if (interChange.Deleted)
                            Table.Remove(interChange);
                    }


                    Mapper.Map(viewModel, interChangeToEdit);

                    Edit(interChangeToEdit);
                }
                else
                {



                    if (interChange != null)
                    {
                        if (!interChange.Deleted)
                            throw new Exception("This Terminal already has this InterChange account. ");
                        Table.Remove(interChange);
                    }


                    var result = Mapper.Map<InterChangeFormViewModel, InterChange>(viewModel);
                    Add(result);

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + " Could not add InterChange split amount, please check the entered values. ");
            }
        }

        public Models.InterChange InterChangeDetails(int id)
        {
            try
            {
                var interchange = Table.Where(x => x.Id == id)
                    .Include(x => x.BankAccount).Include(x => x.Terminal).SingleOrDefault();
                if(interchange == null) throw new Exception("InterChange not found. ");

                return interchange;
            }
            catch (Exception e)
            {
               throw new Exception(e.Message + " InterChange not found. ");
            }
        }

        public void DeleteInterChange(int id)
        {
            try
            {
                var interchange = Table.Where(x => x.Id == id)
                    .Include(x => x.BankAccount).Include(x => x.Terminal).SingleOrDefault();
                if (interchange == null) throw new Exception("InterChange not found. ");

                Delete(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + " InterChange not found. ");
            }
        }

        public InterChangeFormViewModel InitializeNewInterChangeFormViewModel(InterChangeFormViewModel viewModel)
        {
            try
            {
                if (viewModel == null) throw new Exception("Model not found. ");

                viewModel.Terminal = Context.Terminals.SingleOrDefault(x => x.Id == viewModel.TerminalId && !x.Deleted);
                viewModel.BankAccounts = Context.BankAccounts.Where(x => !x.Deleted && x.PartnerId == viewModel.Terminal.PartnerId).ToList();
                viewModel.StartDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month,
                    DateTime.UtcNow.Day + 1);
                viewModel.StopDate = new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day);
                viewModel.CalculationMethod = CalculationMethod.Method.PerTransaction;
                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "InterChange not found. ");
            }
        }

        
    }
}