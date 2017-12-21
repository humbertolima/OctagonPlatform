using OctagonPlatform.Models.FormsViewModels;
using System.Collections.Generic;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface IInterChangeRepository
    {
        IEnumerable<InterChange> GetAllInterChanges(int terminalId);

        InterChangeFormViewModel RenderInterChangeFormViewModel(int terminalId);

        InterChangeFormViewModel InterChangeToEdit(int id);

        void SaveInterChange(InterChangeFormViewModel viewModel, string action);

        InterChange InterChangeDetails(int id);

        void DeleteInterChange(int id);

        InterChangeFormViewModel InitializeNewInterChangeFormViewModel(InterChangeFormViewModel viewModel);
    }
}
