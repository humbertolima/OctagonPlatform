using OctagonPlatform.Models.FormsViewModels;
using System.Collections.Generic;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface IPartnerContactRepository
    {
        IEnumerable<PartnerContact> GetAllPartners();

        IEnumerable<PartnerContact> Search(string search);

        PartnerContactFormViewModel RenderPartnerContactFormViewModel(int partnerId);

        PartnerContactFormViewModel PartnerContactToEdit(int id);

        void SavePartner(PartnerContactFormViewModel viewModel, string action);

        void DeletePartner(int id);

        PartnerContactFormViewModel InitializeNewFormViewModel(PartnerContactFormViewModel viewModel);
    }
}
