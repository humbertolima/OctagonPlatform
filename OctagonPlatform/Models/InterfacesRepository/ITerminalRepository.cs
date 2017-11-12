using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.PersistanceRepository;
using System.Collections.Generic;
using OctagonPlatform.Helpers;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface ITerminalRepository
    {
        IEnumerable<Terminal> GetAllTerminals(int partnerId);

        KeyManager GetKey(string messagesId);

        BindKeyViewModel SetBindKey(string messagesId, string serial1, string serial2);

        TerminalAlertIngnoredViewModel GetConfigNotification(int terminalId);

        Terminal CassettesSet(bool autoRecord, int denomination, int terminalId);

        Terminal CassettesEdit(bool autoRecord, int denomination, int terminalId, int cassetteId);

        void CassettesDelete(int cassetteId);

        Terminal SetConfigNotification(TerminalAlertIngnoredViewModel terminalAlertIngnoredViewModel);

        Terminal SetWorkingHours(TerminalAlertIngnoredViewModel terminalAlertIngnoredViewModel, string WorkingHoursEdit);
        
        Terminal AddWorkingHours(TerminalAlertIngnoredViewModel terminalAlertIngnoredViewModel);
        
        IEnumerable<Terminal> Search(string search, int partnerId);

        TerminalFormViewModel RenderTerminalFormViewModel(int partnerId);

        TerminalFormViewModel TerminalToEdit(int id);

        void SaveTerminal(TerminalFormViewModel viewModel, string action);

        Terminal TerminalDetails(int id);

        void DeleteTerminal(int id);

        TerminalFormViewModel InitializeNewFormViewModel(TerminalFormViewModel viewModel);
    }
}
