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

            return Table.Where(c => c.Deleted == false)
                .Include(x => x.Parent)
                .ToList();
        }

        public PartnerFormViewModel RenderPartnerFormViewModel()
        {
            
            return new PartnerFormViewModel()
            {
                // Comentado porque lo llenare en ajax en dependencia de lo que se necesite.
                Parents = Table.ToList(),
                Countries = Context.Countries.ToList(),
                //States = Context.States.ToList(),
                //Cities = Context.Cities.ToList()
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
            var partner = Table.SingleOrDefault(x => x.Id == id);
            if (partner != null)
            {
                return new PartnerFormViewModel()
                {
                    Id = partner.Id,
                    ParentId = partner.ParentId,
                    Parents = Table.ToList(),
                    BusinessName = partner.BusinessName,
                    Address1 = partner.Address1,
                    Address2 = partner.Address2,
                    CountryId = partner.CountryId,
                    Countries = Context.Countries,
                    StateId = partner.StateId,
                    States = Context.States,
                    CityId = partner.CityId,
                    Cities = Context.Cities,
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
                //.Include(x => x.BankAccounts)
                //.Include(x => x.Terminals)
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
                Parents = Table.ToList(),
                BusinessName = viewModel.BusinessName,
                Address1 = viewModel.Address1,
                Address2 = viewModel.Address2,
                CountryId = viewModel.CountryId,
                Countries = Context.Countries,
                StateId = viewModel.StateId,
                States = Context.States,
                CityId = viewModel.CityId,
                Cities = Context.Cities,
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