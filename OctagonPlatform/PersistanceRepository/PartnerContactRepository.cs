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
            return Table.Where(c => c.Deleted == false)
                .Include(x => x.Partner)
                .ToList();
        }

        public PartnerContactFormViewModel RenderPartnerContactFormViewModel()
        {
            return new PartnerContactFormViewModel()
            {
                Partners =  Context.Partners.ToList(),
                Countries = Context.Countries.ToList(),
                ContactTypes = Context.ContactTypes.ToList()
            };
        }

        public PartnerContactFormViewModel PartnerToEdit(int id)
        {
            var partnerContact = Table.SingleOrDefault(x => x.Id == id);
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
                    Countries = Context.Countries.ToList(),
                    StateId = partnerContact.StateId,
                    States = Context.States.ToList(),
                    CityId = partnerContact.CityId,
                    Cities = Context.Cities.ToList()
                   

                };
            }
            return RenderPartnerContactFormViewModel();
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

        public PartnerContact PartnerContactDetails(int id)
        {
            return new PartnerContact();
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
                States = Context.States.ToList(),
                CityId = viewModel.CityId,
                Cities = Context.Cities.ToList()


            };
        }
    }
}