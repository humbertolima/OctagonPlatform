using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OctagonPlatform.PersistanceRepository
{
    public class PartnerContactRepository: GenericRepository<PartnerContact>, IPartnerContactRepository
    {
        public IEnumerable<PartnerContact> GetAllPartners(int partnerId)
        {
            try
            {
                var parent = Context.Partners.SingleOrDefault(x => x.Id == partnerId && !x.Deleted);
                if (parent == null) throw new Exception("Partner not found. ");

                return Table.Where(c => !c.Deleted && c.PartnerId == partnerId)
                    .Include(x => x.Country)
                    .Include(x => x.State)
                    .Include(x => x.City)
                    .Include(x => x.Partner)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Contact not found. ");
            }
        }

        public PartnerContact Details(int id)
        {
            try
            {
                var contact = Table.Where(x => x.Id == id && !x.Deleted)
                    .Include(x => x.Partner)
                    .Include(x => x.ContactType)
                    .Include(x => x.City)
                    .Include(x => x.Country)
                    .Include(x => x.State)
                    .SingleOrDefault();
                if (contact == null)
                    throw new Exception("Contact not found. ");
                return contact;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Contact not found. ");
            }
        }

        public IEnumerable<PartnerContact> Search(string search, int partnerId)
        {
            try
            {
                return GetAllPartners(partnerId)
                    .Where(c => c.Name.Contains(search) || c.Partner.BusinessName.Contains(search));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Contact not found. ");
            }
        }

        public PartnerContactFormViewModel RenderPartnerContactFormViewModel(int partnerId)
        {
            try
            {
                var partner = Context.Partners.SingleOrDefault(x => x.Id == partnerId && !x.Deleted);
                if (partner == null) throw new Exception("Contact not found. ");

                return new PartnerContactFormViewModel()
                {
                    Countries = Context.Countries.ToList(),
                    States = Context.States.Where(x => x.CountryId == 231).ToList(),
                    Cities = Context.Cities.Where(x => x.StateId == 3930).ToList(),
                    PartnerId = partnerId,
                    ContactTypes = Context.ContactTypes.ToList(),
                    ContactTypeId = 4
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Contact not found. ");
            }
        }

        public PartnerContactFormViewModel PartnerContactToEdit(int id)
        {
            try
            {
                var partnerContact = Table.Where(x => x.Id == id && !x.Deleted)
                    .Include(x => x.Country)
                    .Include(x => x.State)
                    .Include(x => x.City)
                    .Include(x => x.ContactType)
                    .Include(x => x.Partner)
                    .SingleOrDefault();

                if (partnerContact == null) throw new Exception("Contact not found. ");
                {
                    return new PartnerContactFormViewModel()
                    {
                        Id = partnerContact.Id,
                        PartnerId = partnerContact.PartnerId,
                        Partner = partnerContact.Partner,
                        Name = partnerContact.Name,
                        LastName = partnerContact.LastName,
                        Email = partnerContact.Email,
                        ContactTypeId = partnerContact.ContactTypeId,
                        ContactTypes = Context.ContactTypes.ToList(),
                        Phone = partnerContact.Phone,
                        Address1 = partnerContact.Address1,
                        Address2 = partnerContact.Address2,
                        Zip = partnerContact.Zip,
                        CountryId = partnerContact.CountryId,
                        Country = partnerContact.Country,
                        Countries = Context.Countries.ToList(),
                        StateId = partnerContact.StateId,
                        State = partnerContact.State,
                        States = Context.States.Where(x => x.CountryId == partnerContact.CountryId).ToList(),
                        CityId = partnerContact.CityId,
                        City = partnerContact.City,
                        Cities = Context.Cities.Where(x => x.StateId == partnerContact.StateId).ToList(),


                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Contact not found. ");
            }
        }

        public void SavePartner(PartnerContactFormViewModel viewModel, string action)
        {
            try
            {
                var partnerContact = Table.SingleOrDefault(x => (string.Equals(x.Name.Trim().ToLower(),
                                                                     viewModel.Name.Trim().ToLower()) &&
                                                                 string.Equals(x.LastName.Trim().ToLower(),
                                                                     viewModel.LastName.Trim().ToLower()) || x.Email == viewModel.Email || x.Phone == viewModel.Phone));
                if (action == "Edit")
                {
                    var partnerContactToEdit = Table.SingleOrDefault(x => x.Id == viewModel.Id && !x.Deleted);
                    if (partnerContactToEdit == null) throw new Exception("Contact not found. ");
                    if (partnerContact != null)
                    {
                        if(!partnerContact.Deleted && partnerContactToEdit.Id != partnerContact.Id)
                            throw new Exception("Contact already exists. ");
                       
                    }

                    partnerContactToEdit.PartnerId = viewModel.PartnerId;
                    partnerContactToEdit.Name = viewModel.Name;
                    partnerContactToEdit.LastName = viewModel.LastName;
                    partnerContactToEdit.Email = viewModel.Email;
                    partnerContactToEdit.ContactTypeId = viewModel.ContactTypeId;
                    partnerContactToEdit.Phone = viewModel.Phone;
                    partnerContactToEdit.Address1 = viewModel.Address1;
                    partnerContactToEdit.Address2 = viewModel.Address2;
                    partnerContactToEdit.Zip = viewModel.Zip;
                    partnerContactToEdit.CountryId = viewModel.CountryId;
                    partnerContactToEdit.StateId = viewModel.StateId;
                    partnerContactToEdit.CityId = viewModel.CityId;
                    Edit(partnerContactToEdit);
                }
                else
                {
                    if (partnerContact != null)
                    {
                        if (!partnerContact.Deleted)
                            throw new Exception("Contact already exists.");
    
                    }

                    var partnerContactResult = new PartnerContact()
                    {
                        PartnerId = viewModel.PartnerId,
                        Name = viewModel.Name,
                        LastName = viewModel.LastName,
                        Email = viewModel.Email,
                        ContactTypeId = viewModel.ContactTypeId,
                        Phone = viewModel.Phone,
                        Address1 = viewModel.Address1,
                        Address2 = viewModel.Address2,
                        Zip = viewModel.Zip,
                        CountryId = viewModel.CountryId,
                        StateId = viewModel.StateId,
                        CityId = viewModel.CityId,

                    };
                    Add(partnerContactResult);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Please check entered values. ");
            }
        }

        
        public void DeletePartner(int id)
        {
            try
            {
                var contact = Table.SingleOrDefault(x => x.Id == id && !x.Deleted);
                if (contact == null) throw new Exception("Contact not found. ");
                Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Contact not found. ");
            }
        }

        public PartnerContactFormViewModel InitializeNewFormViewModel(PartnerContactFormViewModel viewModel)
        {
            try
            {
                if (viewModel == null) throw new Exception("Model not found. ");

                return new PartnerContactFormViewModel()
                {
                    Id = viewModel.Id,
                    PartnerId = viewModel.PartnerId,
                    Name = viewModel.Name,
                    LastName = viewModel.LastName,
                    Email = viewModel.Email,
                    ContactTypeId = viewModel.ContactTypeId,
                    ContactTypes = Context.ContactTypes.ToList(),
                    Phone = viewModel.Phone,
                    Address1 = viewModel.Address1,
                    Address2 = viewModel.Address2,
                    Zip = viewModel.Zip,
                    CountryId = viewModel.CountryId,
                    Countries = Context.Countries.ToList(),
                    StateId = viewModel.StateId,
                    States = Context.States.Where(x => x.CountryId == viewModel.CountryId).ToList(),
                    CityId = viewModel.CityId,
                    Cities = Context.Cities.Where(x => x.StateId == viewModel.StateId).ToList(),


                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Contact not found. ");
            }
        }
    }
}