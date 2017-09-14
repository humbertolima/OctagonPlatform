using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OctagonPlatform.PersistanceRepository
{
    public class PartnerContactRepository: GenericRepository<PartnerContact>, IPartnerContactRepository
    {
        public IEnumerable<PartnerContact> GetAllPartners()
        {
            return Table.Where(c => !c.Deleted)
                .Include(x => x.Country)
                .Include(x => x.State)
                .Include(x => x.City)
                .Include(x => x.Partner)
                .ToList();
        }

        public IEnumerable<PartnerContact> Search(string search)
        {
            return Table.Where(c => !c.Deleted && (c.Name.Contains(search) || c.Partner.BusinessName.Contains(search)))
                .Include(x => x.Country)
                .Include(x => x.State)
                .Include(x => x.City)
                .Include(x => x.Partner)
                .ToList();
        }

        public PartnerContactFormViewModel RenderPartnerContactFormViewModel(int partnerId)
        {
            return new PartnerContactFormViewModel()
            {
                Countries = Context.Countries.ToList(),
                States = Context.States.Where(x => x.CountryId == 231).ToList(),
                Cities = Context.Cities.Where(x => x.StateId == 3930).ToList(),
                PartnerId = partnerId,
                ContactTypes = Context.ContactTypes.ToList()
            };
        }

        public PartnerContactFormViewModel PartnerContactToEdit(int id)
        {
            var partnerContact = Table.Where(x => x.Id == id)
                .Include(x => x.Country)
                .Include(x => x.State)
                .Include(x => x.City)
                .Include(x => x.Partner)
                .SingleOrDefault();

            if (partnerContact != null)
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
            return RenderPartnerContactFormViewModel(id);
        }

        public void SavePartner(PartnerContactFormViewModel viewModel, string action)
        {
            if (action == "Edit")
            {
                var partnerContactToEdit = Table.SingleOrDefault(x => x.Id == viewModel.Id);
                if (partnerContactToEdit == null) return;
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

        
        public void DeletePartner(int id)
        {
            Delete(id);
        }

        public PartnerContactFormViewModel InitializeNewFormViewModel(PartnerContactFormViewModel viewModel)
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
    }
}