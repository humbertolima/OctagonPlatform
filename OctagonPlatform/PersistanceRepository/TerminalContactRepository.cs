using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System.Collections.Generic;

namespace OctagonPlatform.PersistanceRepository
{
    public class TerminalContactRepository: GenericRepository<TerminalContact>, ITerminalContactRepository
    {
        public IEnumerable<TerminalContact> GetAllTerminalContacts()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TerminalContact> Search(string search)
        {
            throw new System.NotImplementedException();
        }

        public TerminalContactFormViewModel RenderTerminalContactFormViewModel()
        {
            throw new System.NotImplementedException();
        }

        public TerminalContactFormViewModel TerminalContactToEdit(int id)
        {
            throw new System.NotImplementedException();
        }

        public void SaveTerminalContact(TerminalContactFormViewModel viewModel, string action)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteTerminalContact(int id)
        {
            throw new System.NotImplementedException();
        }

        public TerminalContactFormViewModel InitializeNewFormViewModel(TerminalContactFormViewModel viewModel)
        {
            throw new System.NotImplementedException();
        }
    }
}