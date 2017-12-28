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

        TerminalConfigViewModel GetConfigNotification(int terminalId);

        TerminalDocumentsVM GetDocuments(int id);

        Terminal SetDocuments(int indexTerminalId, HttpPostedFileBase archive, int? documentId);

        TerminalContactVM GetContacts(int id);

        TerminalPicturesVM GetPictures(int id);

        TerminalVaultCashVM GetVaultCash(int id);

        Terminal SetPictures(int id, HttpPostedFileBase archive, int? pictureId);

        Terminal PictureDelete(int indexTerminalId, int pictureId);

        Terminal DocumentDelete(int indexTerminalId, int documentId);

        Terminal SetNotes(int indexTerminalId, string note, int? noteId);

        Terminal DeleteNotes(int indexTerminalId, int noteId);

        TerminalNotesVM GetNotes(int id);

        TerminalCassetteVM GetCassettes(int id);

        Terminal CassettesEdit(bool autoRecord, int denomination, int id, int? cassetteId);

        void CassettesDelete(int cassetteId);

        TerminalConfigViewModel SetConfiguration(TerminalConfigViewModel terminalConfigViewModel);

        Terminal SetWorkingHours(FormsViewModels.TerminalConfigViewModel terminalAlertIngnoredViewModel, string WorkingHoursEdit);

        Terminal DeteteWorkingHours(int terminalId, int WorkingHoursId);

        Terminal AddWorkingHours(FormsViewModels.TerminalConfigViewModel terminalAlertIngnoredViewModel);

        IEnumerable<Terminal> Search(string search, int partnerId);

        TerminalFormViewModel RenderTerminalFormViewModel(int partnerId);

        TerminalFormViewModel TerminalToEdit(int id);

        void SaveTerminal(TerminalFormViewModel viewModel, string action);

        Terminal TerminalDetails(int id);

        void DeleteTerminal(int id);

        TerminalFormViewModel InitializeNewFormViewModel(TerminalFormViewModel viewModel);

        IEnumerable<dynamic> LoadCashList(List<JsonLoadCash> list, StatusType.Status status, int partnerid);

        IEnumerable<string> GetAllTerminalId(string value);

        List<Terminal> GetTerminalAssociatedGroup(int partnerId, int state, int city, string zipcode, int? groupId = null);

        IEnumerable<dynamic> GetAllState(string term);

        IEnumerable<dynamic> GetAllCity(string term);

        List<string> GetAllZipCode(string term);

        void EditRange(string[] list, int? groupId);

        IEnumerable<dynamic> LoadCashMngList(List<JsonCashManagement> list, StatusType.Status status, int partnerId);
        IEnumerable<TerminalTableVM> GetTerminalsReport(TerminalListViewModel vmodel, string[] listtn);
        IEnumerable<dynamic> TerminalStatus(List<JsonTerminalStatusReport> list, StatusType.Status status, int partnerId, int city, int state, string zipcode);
        IEnumerable<dynamic> TransDailyList(List<JsonDailyTransactionSummary> list, int partnerid);
        IEnumerable<dynamic> TransMonthlyList(List<JsonMonthlyTransactionSummary> list, int partnerid);
        IEnumerable<dynamic> CashBalanceClose(List<JsonCashBalanceClose> list, int partnerId);
    }
}
