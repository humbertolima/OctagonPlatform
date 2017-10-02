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
    public class TerminalContactRepository: GenericRepository<TerminalContact>, ITerminalContactRepository
    {
        public IEnumerable<TerminalContact> GetAllTerminalContacts()
        {
            try
            {
                return Table.Where(x => !x.Deleted).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TerminalContact Details(int id)
        {
            try
            {
                return Table.Where(x => x.Id == id && !x.Deleted)
                    .Include(x => x.Terminal)
                    .Include(x => x.Country)
                    .Include(x => x.State)
                    .Include(x => x.City)
                    .Include(x => x.ContactType)
                    .SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<TerminalContact> Search(string search)
        {
            try
            {
                return Table.Where(x => !x.Deleted && (x.Name.Contains(search) || x.Address1.Contains(search) ||
                                                       x.City.Name.Contains(search) ||
                                                       x.Country.Name.Contains(search) ||
                                                       x.State.Name.Contains(search) ||
                                                       x.Terminal.Partner.BusinessName.Contains(search))).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TerminalContactFormViewModel RenderTerminalContactFormViewModel(int terminalId)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TerminalContactFormViewModel TerminalContactToEdit(int id)
        {
            try
            {
                var terminalContact = Table.Where(x => x.Id == id && !x.Deleted)
                    .Include(x => x.ContactType)
                    .Include(x => x.Country)
                    .Include(x => x.State)
                    .Include(x => x.City)
                    .Include(x => x.Terminal)
                    .SingleOrDefault();
                if (terminalContact == null) throw new Exception("Contact does not exist in our records!!!");
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
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void SaveTerminalContact(TerminalContactFormViewModel viewModel, string action)
        {
            try
            {
                if (action == "Edit")
                {
                    var terminalContactToEdit = Table.SingleOrDefault(x => x.Id == viewModel.Id && !x.Deleted);

                    if (terminalContactToEdit == null) throw new Exception("Contact does not exist in our records!!!");

                    Mapper.Map(viewModel, terminalContactToEdit);
                    Edit(terminalContactToEdit);
                }
                else
                {
                    var terminalContactNew = Table.SingleOrDefault(x => x.Id == viewModel.Id);

                    if(terminalContactNew != null && !terminalContactNew.Deleted)
                        throw new Exception("Contact already exists!!!");

                    if (terminalContactNew != null && terminalContactNew.Deleted)
                        Table.Remove(terminalContactNew);

                    Add(Mapper.Map<TerminalContactFormViewModel, TerminalContact>(viewModel));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteTerminalContact(int id)
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

        public TerminalContactFormViewModel InitializeNewFormViewModel(TerminalContactFormViewModel viewModel)
        {
            try
            {
                var terminalContactFormViewModel =
                    Mapper.Map<TerminalContactFormViewModel, TerminalContactFormViewModel>(viewModel);
                terminalContactFormViewModel.ContactTypes = Context.ContactTypes.ToList();
                terminalContactFormViewModel.Countries = Context.Countries.ToList();
                terminalContactFormViewModel.States = Context.States.Where(x => x.CountryId == viewModel.CountryId)
                    .ToList();
                terminalContactFormViewModel.Cities = Context.Cities.Where(x => x.StateId == viewModel.StateId)
                    .ToList();
                return terminalContactFormViewModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}