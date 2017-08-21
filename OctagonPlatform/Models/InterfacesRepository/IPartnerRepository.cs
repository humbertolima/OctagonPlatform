﻿using OctagonPlatform.Models.FormsViewModels;
using System.Collections.Generic;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface IPartnerRepository
    {
        IEnumerable<Partner> GetAllPartners();

        PartnerFormViewModel RenderPartnerFormViewModel();

        void SavePartner(PartnerFormViewModel viewModel, string action);

        PartnerFormViewModel PartnerToEdit(int id);

        void DeletePartner(int id);

    }
}
