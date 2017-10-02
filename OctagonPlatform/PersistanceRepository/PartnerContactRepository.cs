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
        public IEnumerable<PartnerContact> GetAllPartners()
        {
            try
            {
                return Table.Where(c => !c.Deleted)
                    .Include(x => x.Country)
                    .Include(x => x.State)
                    .Include(x => x.City)
                    .Include(x => x.Partner)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PartnerContact Details(int id)
        {
            try
            {
                return Table.Where(x => x.Id == id && !x.Deleted)
                    .Include(x => x.Partner)
                    .Include(x => x.ContactType)
                    .Include(x => x.City)
                    .Include(x => x.Country)
                    .Include(x => x.State)
                    .SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<PartnerContact> Search(string search)
        {
            try
            {
                return Table.Where(c => !c.Deleted &&
                                        (c.Name.Contains(search) || c.Partner.BusinessName.Contains(search)))
                    .Include(x => x.Country)
                    .Include(x => x.State)
                    .Include(x => x.City)
                    .Include(x => x.Partner)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PartnerContactFormViewModel RenderPartnerContactFormViewModel(int partnerId)
        {
            try
            {
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
                throw new Exception(ex.Message);
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

                if (partnerContact == null) throw new Exception("Contact does not exists in our records!!!");
                {
                    return new PartnerContactFormViewModel()
                    {
                        Id = partnerContact.Id,
                        PartnerId = partnerContact.PartnerId,
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
                throw new Exception(ex.Message);
            }
        }

        public void SavePartner(PartnerContactFormViewModel viewModel, string action)
        {
            try
            {
                if (action == "Edit")
                {
                    var partnerContactToEdit = Table.SingleOrDefault(x => x.Id == viewModel.Id && !x.Deleted);
                    if (partnerContactToEdit == null) throw new Exception("Contact does not exists in our records!!!");

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
                    var partnerContactNew = Table.SingleOrDefault(x => x.Id == viewModel.Id);
                    if (partnerContactNew != null && !partnerContactNew.Deleted)
                        throw new Exception("Contact already exists!!!");

                    if (partnerContactNew != null && partnerContactNew.Deleted)
                        Table.Remove(partnerContactNew);

                    var partnerContact = new PartnerContact()
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
                    Add(partnerContact);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
        public void DeletePartner(int id)
        {
            Delete(id);
        }

        public PartnerContactFormViewModel InitializeNewFormViewModel(PartnerContactFormViewModel viewModel)
        {
            try
            {
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
                throw new Exception(ex.Message);
            }
        }
    }
}