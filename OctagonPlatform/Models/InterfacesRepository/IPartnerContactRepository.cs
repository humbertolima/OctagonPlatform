using OctagonPlatform.Models.FormsViewModels;
using System.Collections.Generic;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface IPartnerContactRepository
    {
        IEnumerable<PartnerContact> GetAllPartners();

        PartnerContactFormViewModel RenderPartnerContactFormViewModel();

        PartnerContactFormViewModel PartnerToEdit(int id);

        void SavePartner(PartnerContactFormViewModel viewModel, string action);

        PartnerContact PartnerContactDetails(int id);

        void DeletePartner(int id);
    }
}
