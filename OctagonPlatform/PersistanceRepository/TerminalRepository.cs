using AutoMapper;
using OctagonPlatform.Helpers;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OctagonPlatform.PersistanceRepository
{
    public class TerminalRepository: GenericRepository<Terminal>, ITerminalRepository
    {
        public IEnumerable<Terminal> GetAllTerminals()
        {
            return Table.Where(x => !x.Deleted)
                .Include(x => x.Partner)
                .Include(x => x.Country)
                .Include(x => x.State)
                .Include(x => x.City)
                .Include(x => x.TransactionStatistics)
                .Include(x => x.LastTransaction)
                .Include(x => x.LocationType)
                .ToList();
        }

        public IEnumerable<Terminal> Search(string search)
        {
            return Table.Where(x => !x.Deleted && (x.LocationType.Name.Contains(search) || x.Model.Name.Contains(search) || x.Make.Name.Contains(search)))
                .Include(x => x.Partner)
                .Include(x => x.Country)
                .Include(x => x.State)
                .Include(x => x.City)
                .Include(x => x.TransactionStatistics)
                .Include(x => x.LastTransaction)
                .ToList();
        }

        public TerminalFormViewModel RenderTerminalFormViewModel(int partnerId)
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
                SurchargeType = SurchargeType.SurchargeTypes.Greater,
                SettledType = Settled.SettledType.Daily,
                WhoInitiates = Initiate.Who.Host

                
            };
        }

        public TerminalFormViewModel TerminalToEdit(int id)
        {
            var terminal = Table.Where(x => x.Id == id)
                .Include(x => x.Partner)
                .Include(x => x.Country)
                .Include(x => x.State)
                .Include(x => x.City)
                .Include(x => x.Make)
                .Include(x => x.Model)
                .Include(x => x.LocationType)
                .SingleOrDefault();
            if (terminal == null) return RenderTerminalFormViewModel(1);
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

                return terminalViewModel;
            }
        }

        public void SaveTerminal(TerminalFormViewModel viewModel, string action)
        {
            if (action == "Edit")
            {
                var terminalToEdit = Table.SingleOrDefault(x => x.Id == viewModel.Id);
                if (terminalToEdit == null) return;
                {
                    Mapper.Map(viewModel, terminalToEdit);
                    Edit(terminalToEdit);
                }
            }
            else
            {
                var terminal = Mapper.Map<TerminalFormViewModel, Terminal>(viewModel);
                Add(terminal);
            }
        }

        public Terminal TerminalDetails(int id)
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
                .FirstOrDefault();
        }

        public void DeleteTerminal(int id)
        {
           Delete(id);
        }

        public TerminalFormViewModel InitializeNewFormViewModel(TerminalFormViewModel viewModel)
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
            viewModel.SurchargeType = SurchargeType.SurchargeTypes.Greater;
            viewModel.SettledType = Settled.SettledType.Daily;
            viewModel.WhoInitiates = Initiate.Who.Host;

            return viewModel;


        }
    }
}