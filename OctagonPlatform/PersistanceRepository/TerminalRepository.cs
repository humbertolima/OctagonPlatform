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
    public class TerminalRepository : GenericRepository<Terminal>, ITerminalRepository
    {
        public IEnumerable<Terminal> GetAllTerminals(int partnerId)
        {
            try
            {
                var terminals = new List<Terminal>();

                var partner = Context.Partners.Where(x => x.Id == partnerId && !x.Deleted)
                    .Include(x => x.Partners)
                    .SingleOrDefault();


                if (partner == null) return terminals;
                {
                    var partnerTerminals = Table.Where(x => x.PartnerId == partnerId && !x.Deleted)
                        .Include(x => x.Partner)
                        .Include(x => x.Country)
                        .Include(x => x.State)
                        .Include(x => x.City)
                        .Include(x => x.LocationType)
                        .ToList();

                    terminals.AddRange(partnerTerminals);

                    foreach (var item in partner.Partners)
                    {
                        if (item.Id != partnerId)
                        {
                            terminals.AddRange(Table.Where(x => x.PartnerId == item.Id)
                                .Include(x => x.Partner)
                                .Include(x => x.Country)
                                .Include(x => x.State)
                                .Include(x => x.City)
                                .Include(x => x.LocationType)
                                .ToList());
                        }
                    }
                }

                return terminals;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Terminal> Search(string search, int partnerId)
        {
            try
            {
                return Table.Where(x => !x.Deleted && x.PartnerId == partnerId &&
                                        (x.LocationType.Name.Contains(search) || x.Model.Name.Contains(search) ||
                                         x.Make.Name.Contains(search)))
                    .Include(x => x.Partner)
                    .Include(x => x.Country)
                    .Include(x => x.State)
                    .Include(x => x.City)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TerminalFormViewModel RenderTerminalFormViewModel(int partnerId)
        {
            try
            {
                return new TerminalFormViewModel()
                {
                    Countries = Context.Countries.ToList(),
                    States = Context.States.Where(x => x.CountryId == 231).ToList(),
                    Status = StatusType.Status.Active,
                    Cities = Context.Cities.Where(x => x.StateId == 3930).ToList(),
                    Partners = Context.Partners.Where(x => !x.Deleted).ToList(),
                    PartnerId = partnerId,
                    Partner = Context.Partners.SingleOrDefault(x => x.Id == partnerId),
                    LocationTypes = Context.LocationTypes.ToList(),
                    Makes = Context.Makes.ToList(),
                    Models = Context.Models.ToList(),
                    LocationTypeId = 5,
                    MakeId = 1,
                    ModelId = 1,
                    CommunicationType = CommunicationType.Communication.TcpIp,
                    EmvReady = true,
                    WhoInitiates = Initiate.Who.Host,
                    BankAccounts = Context.BankAccounts.Where(x => !x.Deleted).ToList(),
                    SurchargeType = SurchargeType.SurchargeTypes.Greater
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TerminalFormViewModel TerminalToEdit(int id)
        {
            try
            {
                var terminal = Table.Where(x => x.Id == id)
                    .Include(x => x.Partner)
                    .Include(x => x.Country)
                    .Include(x => x.State)
                    .Include(x => x.City)
                    .Include(x => x.Make)
                    .Include(x => x.Model)
                    .Include(x => x.LocationType)
                    .Include(x => x.DefaultBankAccount)
                    .SingleOrDefault();
                if (terminal == null) throw new Exception("Terminal does not exist in our records!!!");
                {
                    var terminalViewModel = Mapper.Map<Terminal, TerminalFormViewModel>(terminal);
                    terminalViewModel.Countries = Context.Countries.ToList();
                    terminalViewModel.States = Context.States.Where(x => x.CountryId == terminal.CountryId).ToList();
                    terminalViewModel.Cities = Context.Cities.Where(x => x.StateId == terminal.StateId).ToList();
                    terminalViewModel.Partners = Context.Partners.Where(x => !x.Deleted).ToList();
                    terminalViewModel.Partner = terminal.Partner;
                    terminalViewModel.LocationTypes = Context.LocationTypes.ToList();
                    terminalViewModel.Makes = Context.Makes.ToList();
                    terminalViewModel.Models = Context.Models.ToList();
                    terminalViewModel.DefaultBankAccount = terminalViewModel.DefaultBankAccount;
                    terminalViewModel.BankAccounts = Context.BankAccounts.Where(x => !x.Deleted).ToList();
                    return terminalViewModel;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SaveTerminal(TerminalFormViewModel viewModel, string action)
        {
            try
            {

                if (action == "Edit")
                {
                    var terminalToEdit = Table.SingleOrDefault(x => x.Id == viewModel.Id && !x.Deleted);
                    if (terminalToEdit == null) throw new Exception("Terminal does not exist in our records!!!");
                    {
                        Mapper.Map(viewModel, terminalToEdit);
                        Edit(terminalToEdit);
                    }
                }
                else
                {
                    var terminalNew = Table.SingleOrDefault(x => x.LocationTypeId == viewModel.LocationTypeId && x.Address1 == viewModel.Address1 && x.Address2 == viewModel.Address2 && x.MachineSerialNumber == viewModel.MachineSerialNumber);

                    if (terminalNew != null && !terminalNew.Deleted)
                        throw new Exception("Terminal already exists!!!");

                    if (terminalNew != null && terminalNew.Deleted)
                        Table.Remove(terminalNew);

                    var terminal = Mapper.Map<TerminalFormViewModel, Terminal>(viewModel);

                    terminal.TerminalId = "000000000";
                    Add(terminal);

                    terminal.TerminalId = TerminalIdGenerator.Generator(terminal.Id);
                    Edit(terminal);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Terminal TerminalDetails(int id)
        {
            try
            {
                return Table.Where(x => x.Id == id && !x.Deleted)
                    .Include(x => x.Partner)
                    .Include(x => x.Country)
                    .Include(x => x.State)
                    .Include(x => x.City)
                    .Include(x => x.LastTransaction)
                    .Include(x => x.LocationType)
                    .Include(x => x.Cassettes)
                    .Include(x => x.DefaultBankAccount)
                    .Include(x => x.Contracts)
                    .Include(x => x.Documents)
                    .Include(x => x.Events)
                    .Include(x => x.InterChanges)
                    .Include(x => x.LoadCashs)
                    .Include(x => x.Make)
                    .Include(x => x.Model)
                    .Include(x => x.Users)
                    .Include(x => x.VaultCash)
                    .Include(x => x.Surcharges)
                    .Include(x => x.Notes)
                    .Include(x => x.TerminalContacts)
                    .Include(x => x.TerminalPictures)
                    .Include(x => x.TransactionStatistics)
                    .Include(x => x.Cassettes)
                    .Include(x => x.BindedKey)
                    .Include(x => x.Disputes)
                    .Include(x => x.TerminalAlertConfigs)
                    .Include(x => x.DefaultBankAccount)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteTerminal(int id)
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

        public TerminalFormViewModel InitializeNewFormViewModel(TerminalFormViewModel viewModel)
        {
            try
            {
                viewModel.Countries = Context.Countries.ToList();
                viewModel.States = Context.States.Where(x => x.CountryId == 231).ToList();
                viewModel.Cities = Context.Cities.Where(x => x.StateId == 3930).ToList();
                viewModel.Partners = Context.Partners.Where(x => !x.Deleted).ToList();
                viewModel.Partner = Context.Partners.SingleOrDefault(x => x.Id == viewModel.PartnerId);
                viewModel.LocationTypes = Context.LocationTypes.ToList();
                viewModel.Makes = Context.Makes.ToList();
                viewModel.Models = Context.Models.ToList();
                viewModel.LocationTypeId = 5;
                viewModel.MakeId = 1;
                viewModel.ModelId = 1;
                viewModel.CommunicationType = CommunicationType.Communication.TcpIp;
                viewModel.EmvReady = true;
                viewModel.WhoInitiates = Initiate.Who.Host;
                viewModel.BankAccounts = Context.BankAccounts.Where(x => !x.Deleted).ToList();
                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TerminalAlertConfig GetConfigNotification(int terminalId)
        {
            Terminal terminal = Table
                .Include(c => c.TerminalAlertConfigs)
                .FirstOrDefault(c => c.Id == terminalId);          //Context.TerminalAlertConfigs.FirstOrDefault(c => c.TerminalId == terminalId);

            var terminalConfigs = new TerminalAlertConfig();
            if (terminal.TerminalAlertConfigs == null)
            {
                terminalConfigs = new TerminalAlertConfig();
            }
            else { terminalConfigs = terminal.TerminalAlertConfigs; }

            return terminalConfigs;
        }

        public TerminalAlertConfig SetConfigNotification(TerminalAlertConfig terminalAlertConfig, int terminalId)
        {
            var terminal = Table
                .Include(c => c.TerminalAlertConfigs)
                .FirstOrDefault(c => c.Id == terminalId);

            terminal.TerminalAlertConfigs = terminalAlertConfig;

            Edit(terminal);

            return terminal.TerminalAlertConfigs;
        }
    }
}