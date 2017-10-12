using OctagonPlatform.Models.FormsViewModels;
using System.Collections.Generic;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface ISurchargeRepository
    {
        IEnumerable<Surcharge> GetAllSurcharges(int terminalId);

        SurchargeFormViewModel RenderSurchargeFormViewModel(int terminalId);

        SurchargeFormViewModel SurchargeToEdit(int id);

        void SaveSurcharge(SurchargeFormViewModel viewModel, string action);

        Surcharge SurchargeDetails(int id);

        void DeleteSurcharge(int id);

        SurchargeFormViewModel InitializeNewSurchargeFormViewModel(SurchargeFormViewModel viewModel);
    }
}
