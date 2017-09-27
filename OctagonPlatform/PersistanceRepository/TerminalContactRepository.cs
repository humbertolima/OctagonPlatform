using AutoMapper;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OctagonPlatform.PersistanceRepository
{
    public class TerminalContactRepository: GenericRepository<TerminalContact>, ITerminalContactRepository
    {
        public IEnumerable<TerminalContact> GetAllTerminalContacts()
        {
            return Table.Where(x => !x.Deleted).ToList();
        }

        public TerminalContact Details(int id)
        {
            return Table.Where(x => x.Id == id && !x.Deleted)
                .Include(x => x.Terminal)
                .Include(x => x.Country)
                .Include(x => x.State)
                .Include(x => x.City)
                .Include(x => x.ContactType)
                .SingleOrDefault();
        }

        public IEnumerable<TerminalContact> Search(string search)
        {
            return Table.Where(x => !x.Deleted && (x.Name.Contains(search) || x.Address1.Contains(search) ||
                                                   x.City.Name.Contains(search) || x.Country.Name.Contains(search) ||
                                                   x.State.Name.Contains(search) ||
                                                   x.Terminal.Partner.BusinessName.Contains(search))).ToList();
        }

        public TerminalContactFormViewModel RenderTerminalContactFormViewModel(int terminalId)
        {
            return new TerminalContactFormViewModel()
            {
                TerminalId = terminalId,
                Terminal = Context.Terminals.SingleOrDefault(x => x.Id == terminalId),
                ContactTypes = Context.ContactTypes.ToList(),
                ContactTypeId = 4,
                Countries = Context.Countries.ToList(),
                States = Context.States.Where(x => x.CountryId == 231).ToList(),
                Cities = Context.Cities.Where(x => x.StateId == 3930).ToList()
            };
        }

        public TerminalContactFormViewModel TerminalContactToEdit(int id)
        {
            var terminalContact = Table.Where(x => x.Id == id)
                .Include(x => x.ContactType)
                .Include(x => x.Country)
                .Include(x => x.State)
                .Include(x => x.City)
                .Include(x => x.Terminal)
                .SingleOrDefault();
            if (terminalContact != null)
            {

                return new TerminalContactFormViewModel()
                {
                    Id = terminalContact.Id,
                    TerminalId = terminalContact.TerminalId,
                    Name = terminalContact.Name,
                    LastName = terminalContact.LastName,
                    Email = terminalContact.Email,
                    ContactTypeId = terminalContact.ContactTypeId,
                    ContactTypes = Context.ContactTypes.ToList(),
                    Phone = terminalContact.Phone,
                    Address1 = terminalContact.Address1,
                    Address2 = terminalContact.Address2,
                    Zip = terminalContact.Zip,
                    CountryId = terminalContact.CountryId,
                    Country = terminalContact.Country,
                    Countries = Context.Countries.ToList(),
                    StateId = terminalContact.StateId,
                    State = terminalContact.State,
                    States = Context.States.Where(x => x.CountryId == terminalContact.CountryId).ToList(),
                    CityId = terminalContact.CityId,
                    City = terminalContact.City,
                    Cities = Context.Cities.Where(x => x.StateId == terminalContact.StateId).ToList(),
                };
            }
            return null;

        }

        public void SaveTerminalContact(TerminalContactFormViewModel viewModel, string action)
        {
            if (action == "Edit")
            {
                var terminalContactToEdit = Table.SingleOrDefault(x => x.Id == viewModel.Id);
                if (terminalContactToEdit == null) return;
                Mapper.Map(viewModel, terminalContactToEdit);
                Edit(terminalContactToEdit);
            }
            else
            {
                Add(Mapper.Map<TerminalContactFormViewModel, TerminalContact>(viewModel));
            }
        }

        public void DeleteTerminalContact(int id)
        {
            Delete(id);
        }

        public TerminalContactFormViewModel InitializeNewFormViewModel(TerminalContactFormViewModel viewModel)
        {
            var terminalContactFormViewModel =
                Mapper.Map<TerminalContactFormViewModel, TerminalContactFormViewModel>(viewModel);
            terminalContactFormViewModel.ContactTypes = Context.ContactTypes.ToList();
            terminalContactFormViewModel.Countries = Context.Countries.ToList();
            terminalContactFormViewModel.States = Context.States.Where(x => x.CountryId == viewModel.CountryId).ToList();
            terminalContactFormViewModel.Cities = Context.Cities.Where(x => x.StateId == viewModel.StateId).ToList();
            return terminalContactFormViewModel;
        }
    }
}