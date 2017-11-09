﻿using AutoMapper;
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

        public void SaveSurcharge(InterChangeFormViewModel viewModel, string action)
        {
            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Models.InterChange InterChangeDetails(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteInterChange(int id)
        {
            throw new NotImplementedException();
        }

        public InterChangeFormViewModel InitializeNewInterChangeFormViewModel(InterChangeFormViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        
    }
}