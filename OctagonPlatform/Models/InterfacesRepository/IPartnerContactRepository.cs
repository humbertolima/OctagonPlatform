using OctagonPlatform.Models.FormsViewModels;
using System.Collections.Generic;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface IPartnerContactRepository
    {
        IEnumerable<PartnerContact> GetAllPartners(int partnerId);

        PartnerContact Details(int id);

        IEnumerable<PartnerContact> Search(string search, int partnerId);

        PartnerContactFormViewModel RenderPartnerContactFormViewModel(int partnerId);

        PartnerContactFormViewModel PartnerContactToEdit(int id);

        void SavePartner(PartnerContactFormViewModel viewModel, string action);

        void DeletePartner(int id);

        PartnerContactFormViewModel InitializeNewFormViewModel(PartnerContactFormViewModel viewModel);
    }
}
