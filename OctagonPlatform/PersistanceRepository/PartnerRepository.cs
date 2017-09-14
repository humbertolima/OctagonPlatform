using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OctagonPlatform.PersistanceRepository
{
    public class PartnerRepository: GenericRepository<Partner>, IPartnerRepository
    {
       
        public IEnumerable<Partner> GetAllPartners()
        {

            return Table.Where(c => !c.Deleted)
                .Include(x => x.Parent)
                .ToList();
        }

        public IEnumerable<Partner> Search(string search)
        {
            return Table.Where(c => !c.Deleted && (c.BusinessName.Contains(search) || c.Parent.BusinessName.Contains(search)))
                .Include(x => x.Parent)
                .ToList();
        }

        public PartnerFormViewModel RenderPartnerFormViewModel()
        {
            
            return new PartnerFormViewModel()
            {
                
                Parents = Table.Where(x => !x.Deleted).ToList(),
                
                Countries = Context.Countries.ToList(),
                States = Context.States.Where(x => x.CountryId == 231).ToList(),
                Cities = Context.Cities.Where(x => x.StateId == 3930).ToList()
                
            };
        }
        
        public void SavePartner(PartnerFormViewModel viewModel, string action)
        {
            if (action == "Edit")
            {
                var partnerToEdit = Table.SingleOrDefault(x => x.Id == viewModel.Id);
                if (partnerToEdit == null) return;
                partnerToEdit.ParentId = viewModel.ParentId;
                partnerToEdit.BusinessName = viewModel.BusinessName;
                partnerToEdit.Address1 = viewModel.Address1;
                partnerToEdit.Address2 = viewModel.Address2;
                partnerToEdit.CountryId = viewModel.CountryId;
                partnerToEdit.StateId = viewModel.StateId;
                partnerToEdit.CityId = viewModel.CityId;
                partnerToEdit.Status = viewModel.Status;
                partnerToEdit.Email = viewModel.Email;
                partnerToEdit.WorkPhone = viewModel.WorkPhone;
                partnerToEdit.Mobile = viewModel.Mobile;
                partnerToEdit.Fax = viewModel.Fax;
                partnerToEdit.WebSite = viewModel.WebSite;
                Edit(partnerToEdit);
            }
            else
            {
                var partner = new Partner()
                {
                    ParentId = viewModel.ParentId,
                    BusinessName = viewModel.BusinessName,
                    Address1 = viewModel.Address1,
                    Address2 = viewModel.Address2,
                    CountryId = viewModel.CountryId,
                    StateId = viewModel.StateId,
                    CityId = viewModel.CityId,
                    Status = viewModel.Status,
                    Email = viewModel.Email,
                    WorkPhone = viewModel.WorkPhone,
                    Mobile = viewModel.Mobile,
                    Fax = viewModel.Fax,
                    WebSite = viewModel.WebSite,
                    
                };
                Add(partner);
            }
            
        }

        public PartnerFormViewModel PartnerToEdit(int id)
        {
            var partner = Table.Where(x => x.Id == id)
                .Include(x => x.Country)
                .Include(x => x.State)
                .Include(x => x.City)
                .Include(x => x.Parent)
                .SingleOrDefault();
            if (partner != null)
            {
                return new PartnerFormViewModel()
                {
                    Id = partner.Id,
                    ParentId = partner.ParentId,
                    Parents = Table.Where(x => !x.Deleted).ToList(),
                    BusinessName = partner.BusinessName,
                    Address1 = partner.Address1,
                    Address2 = partner.Address2,
                    CountryId = partner.CountryId,
                    Country = partner.Country,
                    Countries = Context.Countries.ToList(),
                    StateId = partner.StateId,
                    State = partner.State,
                    States = Context.States.Where(x => x.CountryId == partner.CountryId).ToList(),
                    CityId = partner.CityId,
                    City = partner.City,
                    Cities = Context.Cities.Where(x => x.StateId == partner.StateId).ToList(),
                    Status = partner.Status,
                    Email = partner.Email,
                    WorkPhone = partner.WorkPhone,
                    Mobile = partner.Mobile,
                    Fax = partner.Fax,
                    WebSite = partner.WebSite,

                };
            }
            return RenderPartnerFormViewModel();
        }

        public Partner PartnerDetails(int id)
        {
           return Table.Where(x => x.Id == id)
                .Include(x => x.Parent)
                .Include(x => x.PartnerContacts)
                .Include(x => x.Users)
                .Include(x => x.Partners)
                .Include(x => x.Country)
                .Include(x => x.State)
                .Include(x => x.City)
                //.Include(x => x.BankAccounts)
                .Include(x => x.Terminals)
                .FirstOrDefault();
           
        }

        public void DeletePartner(int id)
        {
            Delete(id);

        }

        public PartnerFormViewModel InitializeNewFormViewModel(PartnerFormViewModel viewModel)
        {
            return new PartnerFormViewModel()
            {
                Id = viewModel.Id,
                ParentId = viewModel.ParentId,
                Parents = Table.Where(x => !x.Deleted).ToList(),
                BusinessName = viewModel.BusinessName,
                Address1 = viewModel.Address1,
                Address2 = viewModel.Address2,
                CountryId = viewModel.CountryId,
                Countries = Context.Countries.ToList(),
                StateId = viewModel.StateId,
                States = Context.States.Where(x => x.CountryId == viewModel.CountryId).ToList(),
                CityId = viewModel.CityId,
                Cities = Context.Cities.Where(x => x.StateId == viewModel.StateId).ToList(),
                Status = viewModel.Status,
                Email = viewModel.Email,
                WorkPhone = viewModel.WorkPhone,
                Mobile = viewModel.Mobile,
                Fax = viewModel.Fax,
                WebSite = viewModel.WebSite,

            };
        }
    }

}