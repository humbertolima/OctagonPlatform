using OctagonPlatform.Models.FormsViewModels;
using System.Collections.Generic;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface ITerminalRepository
    {
        IEnumerable<Terminal> GetAllTerminals();

        IEnumerable<Terminal> Search(string search);

        TerminalFormViewModel RenderTerminalFormViewModel(int partnerId);

        TerminalFormViewModel TerminalToEdit(int id);

        void SaveTerminal(TerminalFormViewModel viewModel, string action);

        Terminal TerminalDetails(int id);

        void DeleteTerminal(int id);

        TerminalFormViewModel InitializeNewFormViewModel(TerminalFormViewModel viewModel);
    }
}
