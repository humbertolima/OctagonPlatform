using OctagonPlatform.Models.FormsViewModels;
using System.Collections.Generic;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface IPartnerRepository : IGenericRepository<Partner>
    {
        Partner GetAllPartners(int parentId);

        IEnumerable<Partner> Search(string search, int parentId);

        PartnerFormViewModel RenderPartnerFormViewModel(int parentId);

        PartnerFormViewModel PartnerToEdit(int id);

        void SavePartner(PartnerFormViewModel viewModel, string action);

        Partner PartnerDetails(int id);

        void DeletePartner(int id);

        PartnerFormViewModel InitializeNewFormViewModel(PartnerFormViewModel viewModel);

       
        IEnumerable<dynamic> GetAllPartner(string term, int partnerid);
    }
}
