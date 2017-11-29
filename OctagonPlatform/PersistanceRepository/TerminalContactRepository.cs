using AutoMapper;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Data.Entity;
using System.Linq;

namespace OctagonPlatform.PersistanceRepository
{
    public class TerminalContactRepository: GenericRepository<TerminalContact>, ITerminalContactRepository
    {
        

        public TerminalContact Details(int id)
        {
            try
            {
                var contact = Table.Where(x => x.Id == id && !x.Deleted)
                    .Include(x => x.Terminal)
                    .Include(x => x.Country)
                    .Include(x => x.State)
                    .Include(x => x.City)
                    .Include(x => x.ContactType)
                    .SingleOrDefault();
                if (contact == null) throw new Exception("Contact not found. ");
                return contact;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Contact not found. ");
            }
        }

        //public IEnumerable<TerminalContact> Search(string search, int partnerId)
        //{
        //    try
        //    {
        //        return Table.Where(x => !x.Deleted && (x.Name.Contains(search) || x.Address1.Contains(search) ||
        //                                               x.City.Name.Contains(search) ||
        //                                               x.Country.Name.Contains(search) ||
        //                                               x.State.Name.Contains(search) ||
        //                                               x.Terminal.Partner.BusinessName.Contains(search))).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        public TerminalContactFormViewModel RenderTerminalContactFormViewModel(int terminalId)
        {
            try
            {
                var terminal = Context.Terminals.SingleOrDefault(x => x.Id == terminalId && !x.Deleted);
                if (terminal == null) throw new Exception("Terminal for this contact not found. ");
                return new TerminalContactFormViewModel()
                {
                    TerminalId = terminalId,
                    Terminal = terminal,
                    ContactTypes = Context.ContactTypes.ToList(),
                    ContactTypeId = 4,
                    Countries = Context.Countries.ToList(),
                    States = Context.States.Where(x => x.CountryId == 231).ToList(),
                    Cities = Context.Cities.Where(x => x.StateId == 3930).ToList()
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Contact not found. ");
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
                if (terminalContact == null) throw new Exception("Contact does not exist in our records.");
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
                throw new Exception(ex.Message + "Contact not found. ");
            }

        }

        public void SaveTerminalContact(TerminalContactFormViewModel viewModel, string action)
        {
            try
            {
                var terminalContact = Table.SingleOrDefault(x => (string.Equals(x.Name.Trim().ToLower(),
                                                                      viewModel.Name.Trim().ToLower()) &&
                                                                  string.Equals(x.LastName.Trim().ToLower(),
                                                                      viewModel.LastName.Trim().ToLower()) || x.Email == viewModel.Email || x.Phone == viewModel.Phone));
                if (action == "Edit")
                {
                    var terminalContactToEdit = Table.SingleOrDefault(x => x.Id == viewModel.Id && !x.Deleted);

                    if (terminalContactToEdit == null) throw new Exception("Contact does not exist in our records!!!");
                    if (terminalContact != null)
                    {
                        if(!terminalContact.Deleted && terminalContactToEdit.Id != terminalContact.Id)
                            throw new Exception("Contact already exists. ");
                       

                    }

                    Mapper.Map(viewModel, terminalContactToEdit);
                    Edit(terminalContactToEdit);
                }
                else
                {


                    if (terminalContact != null)
                    {
                        if (!terminalContact.Deleted)
                            throw new Exception("Contact already exists. ");
                   

                    }

                    Add(Mapper.Map<TerminalContactFormViewModel, TerminalContact>(viewModel));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Please check the entered values. ");
            }
        }

        public void DeleteTerminalContact(int id)
        {
            try
            {
                var contact = Table.SingleOrDefault(x => x.Id == id && !x.Deleted);
                if(contact == null) throw new Exception("Contact not found. ");
                Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Contact not found. ");
            }
        }

        public TerminalContactFormViewModel InitializeNewFormViewModel(TerminalContactFormViewModel viewModel)
        {
            try
            {
                if (viewModel == null) throw new Exception("Model not found. ");
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
                throw new Exception(ex.Message + "Contact not found. ");
            }
        }
    }
}