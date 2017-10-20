using OctagonPlatform.Helpers;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OctagonPlatform.PersistanceRepository
{
    public class PartnerRepository: GenericRepository<Partner>, IPartnerRepository
    {
        
        public Partner GetAllPartners(int parentId)
        {
            try
            {
                return Table.Where(c => c.Id == parentId && !c.Deleted)
                    .Include(x => x.Partners)
                    .Include(x => x.Parent)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Partner> Search(string search, int parentId)
        {
            try
            {
                return Table.Where(c =>
                    (c.BusinessName.Contains(search) || c.Parent.BusinessName.Contains(search)));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PartnerFormViewModel RenderPartnerFormViewModel(int parentId)
        {
            try
            {
                return new PartnerFormViewModel()
                {
                    Parents = Table.Where(x => (x.Id == parentId || x.ParentId == parentId) && !x.Deleted).ToList(),
                    ParentId = parentId,
                    Status = StatusType.Status.Active,
                    Parent = Context.Partners.SingleOrDefault(x => x.Id == parentId),
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
        
        public void SavePartner(PartnerFormViewModel viewModel, string action)
        {
            try
            {
                var partnerBName = Table.SingleOrDefault(x => string.Equals(x.BusinessName.ToLower(), viewModel.BusinessName.ToLower(), StringComparison.CurrentCultureIgnoreCase));
                if (action == "Edit")
                {
                    var partnerToEdit = Table.SingleOrDefault(x => x.Id == viewModel.Id && !x.Deleted);
                    if (partnerToEdit == null) throw new Exception("Partner does not exist in our records!!!");
                    if (partnerBName != null)
                    {

                        if (!partnerBName.Deleted && partnerToEdit.Id != partnerBName.Id)
                            throw new Exception("Partner already exists. ");
                        if (partnerBName.Deleted)
                            Table.Remove(partnerBName);
                    }
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

                    if (partnerBName != null)
                    {
                        if (!partnerBName.Deleted)
                            throw new Exception("Partner already exists!!!");
                        Table.Remove(partnerBName);
                    }
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
                        Interchange = InterchangeConstants.ClientInterchangeAmount
                        };
                    Add(partner);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public PartnerFormViewModel PartnerToEdit(int id)
        {
            try
            {
                var partner = Table.Where(x => x.Id == id && !x.Deleted)
                    .Include(x => x.Country)
                    .Include(x => x.State)
                    .Include(x => x.City)
                    .Include(x => x.Parent)
                    .SingleOrDefault();
                if (partner == null) throw new Exception("Partner does not exits in our records!!!");
                {
                    return new PartnerFormViewModel()
                    {
                        Id = partner.Id,
                        ParentId = partner.ParentId,
                        Parent = partner.Parent,
                        Parents = Table.Where(x => (x.Id == partner.ParentId || x.ParentId == partner.ParentId) && !x.Deleted).ToList(),
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
                        WebSite = partner.WebSite
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Partner PartnerDetails(int id)
        {
            try
            {
                var partner = Table.Where(x => x.Id == id && !x.Deleted)
                    .Include(x => x.Parent)
                    .Include(x => x.PartnerContacts)
                    .Include(x => x.Users)
                    .Include(x => x.Partners)
                    .Include(x => x.Country)
                    .Include(x => x.State)
                    .Include(x => x.City)
                    .Include(x => x.BankAccounts)
                    .Include(x => x.Partners)
                    .Include(x => x.Terminals)
                    .FirstOrDefault();
                if(partner == null) throw new Exception("Partner does not exist in our records. ");

               
                return partner;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void DeletePartner(int id)
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

        public PartnerFormViewModel InitializeNewFormViewModel(PartnerFormViewModel viewModel)
        {
            try
            {
                return new PartnerFormViewModel()
                {
                    Id = viewModel.Id,
                    ParentId = viewModel.ParentId,
                    Parent = Context.Partners.SingleOrDefault(x => x.Id == viewModel.ParentId),
                    Parents = Table.Where(x => (x.Id == viewModel.ParentId || x.ParentId == viewModel.ParentId) && !x.Deleted).ToList(),
                    BusinessName = viewModel.BusinessName,
                    Address1 = viewModel.Address1,
                    Address2 = viewModel.Address2,
                    CountryId = viewModel.CountryId,
                    Countries = Context.Countries.ToList(),
                    StateId = viewModel.StateId,
                    States = Context.States.Where(x => x.CountryId == viewModel.CountryId).ToList(),
                    CityId = viewModel.CityId,
                    Cities = Context.Cities.Where(x => x.StateId == viewModel.StateId).ToList(),
                    Status = StatusType.Status.Active,
                    Email = viewModel.Email,
                    WorkPhone = viewModel.WorkPhone,
                    Mobile = viewModel.Mobile,
                    Fax = viewModel.Fax,
                    WebSite = viewModel.WebSite,

                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}