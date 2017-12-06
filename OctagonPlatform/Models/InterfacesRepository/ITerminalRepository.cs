using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.PersistanceRepository;
using System.Collections.Generic;
using OctagonPlatform.Helpers;
using OctagonPlatform.Controllers.Reports.JSON;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface ITerminalRepository : IGenericRepository<Terminal>
    {
        Terminal GetTerminal(string terminalId);

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

        IEnumerable<dynamic> LoadCashList(List<JsonLoadCash> list, StatusType.Status status, int partnerid);

        IEnumerable<string> GetAllTerminalId(string value);
        
        List<Terminal> GetTerminalAssociatedGroup(int partnerId, int state, int city, string zipcode, int? groupId=null);
        IEnumerable<dynamic> GetAllState(string term);
        IEnumerable<dynamic> GetAllCity(string term);
        List<string> GetAllZipCode(string term);
        void EditRange(string[] list, int? groupId);
        IEnumerable<dynamic> LoadCashMngList(List<JsonCashManagement> list, StatusType.Status status, int partnerId);
    }
}
