using OctagonPlatform.Models.FormsViewModels;
using System.Collections.Generic;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface ITerminalContactRepository
    {
        IEnumerable<TerminalContact> GetAllTerminalContacts();

        TerminalContact Details(int id);

        IEnumerable<TerminalContact> Search(string search);

        TerminalContactFormViewModel RenderTerminalContactFormViewModel(int terminalId);

        TerminalContactFormViewModel TerminalContactToEdit(int id);

        void SaveTerminalContact(TerminalContactFormViewModel viewModel, string action);

        void DeleteTerminalContact(int id);

        TerminalContactFormViewModel InitializeNewFormViewModel(TerminalContactFormViewModel viewModel);
    }
}
