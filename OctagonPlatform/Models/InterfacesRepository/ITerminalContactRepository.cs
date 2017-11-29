using OctagonPlatform.Models.FormsViewModels;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface ITerminalContactRepository
    {
        //IEnumerable<TerminalContact> GetAllTerminalContacts(int prtnerId);

        TerminalContact Details(int id);

        //IEnumerable<TerminalContact> Search(string search, int partnerId);

        TerminalContactFormViewModel RenderTerminalContactFormViewModel(int terminalId);

        TerminalContactFormViewModel TerminalContactToEdit(int id);

        void SaveTerminalContact(TerminalContactFormViewModel viewModel, string action);

        void DeleteTerminalContact(int id);

        TerminalContactFormViewModel InitializeNewFormViewModel(TerminalContactFormViewModel viewModel);
    }
}
