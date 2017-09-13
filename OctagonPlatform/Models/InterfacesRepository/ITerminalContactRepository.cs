using OctagonPlatform.Models.FormsViewModels;
using System.Collections.Generic;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface ITerminalContactRepository
    {
        IEnumerable<TerminalContact> GetAllTerminalContacts();

        IEnumerable<TerminalContact> Search(string search);

        TerminalContactFormViewModel RenderTerminalContactFormViewModel();

        TerminalContactFormViewModel TerminalContactToEdit(int id);

        void SaveTerminalContact(TerminalContactFormViewModel viewModel, string action);

        void DeleteTerminalContact(int id);

        TerminalContactFormViewModel InitializeNewFormViewModel(TerminalContactFormViewModel viewModel);
    }
}
