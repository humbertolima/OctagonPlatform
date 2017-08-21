﻿using OctagonPlatform.Models;
using OctagonPlatform.Models.DetailsViewModels;
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
            
            return Table.Include(x => x.Country)
                    .Include(x => x.State)
                    .Include(x => x.City)
                    .Include(x => x.Parent)
                    .ToList();
        }

        public PartnerFormViewModel RenderPartnerFormViewModel()
        {
            var formViewModel = new PartnerFormViewModel()
            {
                Parents = Table.ToList(),
                Countries = Context.Countries.ToList(),
                States = Context.States.ToList(),
                Cities = Context.Cities.ToList()
            };
            return formViewModel;
        }
        
        public void SavePartner(PartnerFormViewModel viewModel, string action)
        {
            if (action == "Edit")
            {
                var partnerToEdit = Table.SingleOrDefault(x => x.Id == viewModel.Id);
                if (partnerToEdit != null)
                {
                    Edit(partnerToEdit);
                }

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
                    BusinessName = partner.BusinessName,
                    Address1 = partner.Address1,
                    Address2 = partner.Address2,
                    CountryId = partner.CountryId,
                    StateId = partner.StateId,
                    CityId = partner.CityId,
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

        public PartnerDetailsViewModel PartnerDetails(int id)
        {
           return new PartnerDetailsViewModel();
        }

        public void DeletePartner(int id)
        {
            Delete(id);

        }

    }

}