using OctagonPlatform.Models.FormsViewModels;
using System.Collections.Generic;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface IPartnerRepository
    {
        IEnumerable<Partner> GetAllPartners();

        PartnerFormViewModel RenderPartnerFormViewModel();

        PartnerFormViewModel PartnerToEdit(int id);

        void SavePartner(PartnerFormViewModel viewModel, string action);

        Partner PartnerDetails(int id);

        void DeletePartner(int id);

    }
}
