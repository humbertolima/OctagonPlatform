using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.PersistanceRepository;
using System.Collections.Generic;
using OctagonPlatform.Helpers;
using OctagonPlatform.Controllers.Reports.JSON;
using OctagonPlatform.Views.ReportsSmart.ViewModels;
using System;
using System.Web;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface ITerminalRepository : IGenericRepository<Terminal>
    {
       System.Threading.Tasks.Task<List<JsonLoadCash>> GetCashLoad(DateTime start, DateTime end, string terminalId);

        Terminal GetTerminal(string terminalId);

        IEnumerable<Terminal> GetAllTerminals(int partnerId);

        KeyManager GetKey(string messagesId);

        BindKeyViewModel SetBindKey(string messagesId, string serial1, string serial2);

        TerminalAlertIngnoredViewModel GetConfigNotification(int terminalId);

        Terminal SetDocuments(int indexTerminalId, HttpPostedFileBase archive, int? documentId);

        Terminal SetPictures(int indexTerminalId, HttpPostedFileBase archive, int? pictureId);

        Terminal PictureDelete(int indexTerminalId, int pictureId);

        Terminal DocumentDelete(int indexTerminalId, int documentId);

        Terminal SetNotes(int indexTerminalId, string note, int? noteId);

        Terminal DeleteNotes(int indexTerminalId, int noteId);

        Terminal CassettesSet(bool autoRecord, int denomination, int terminalId);

        Terminal CassettesEdit(bool autoRecord, int denomination, int terminalId, int cassetteId);
        
        void CassettesDelete(int cassetteId);

        Terminal SetConfigNotification(TerminalAlertIngnoredViewModel terminalAlertIngnoredViewModel);

        Terminal SetWorkingHours(TerminalAlertIngnoredViewModel terminalAlertIngnoredViewModel, string WorkingHoursEdit);

        Terminal DeteteWorkingHours(int terminalId, int WorkingHoursId);

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
        IEnumerable<TerminalTableVM> GetTerminalsReport(TerminalListViewModel vmodel, string[] listtn);
        IEnumerable<dynamic> TerminalStatus(List<JsonTerminalStatusReport> list, StatusType.Status status, int partnerId, int city,int state, string zipcode);
        IEnumerable<dynamic> TransDailyList(List<JsonDailyTransactionSummary> list, int partnerid);

    }
}
