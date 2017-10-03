using OctagonPlatform.Models.FormsViewModels;
using System.Collections.Generic;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface ITerminalRepository
    {
        IEnumerable<Terminal> GetAllTerminals(int partnerId);

        TerminalAlertConfig GetConfigNotification(int terminalId);

        IEnumerable<Terminal> Search(string search, int partnerId);

        TerminalFormViewModel RenderTerminalFormViewModel(int partnerId);

        TerminalFormViewModel TerminalToEdit(int id);

        void SaveTerminal(TerminalFormViewModel viewModel, string action);

        Terminal TerminalDetails(int id);

        void DeleteTerminal(int id);

        TerminalFormViewModel InitializeNewFormViewModel(TerminalFormViewModel viewModel);
    }
}
