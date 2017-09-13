using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System.Collections.Generic;

namespace OctagonPlatform.PersistanceRepository
{
    public class TerminalRepository: GenericRepository<Terminal>, ITerminalRepository
    {
        public IEnumerable<Terminal> GetAllTerminals()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Terminal> Search(string search)
        {
            throw new System.NotImplementedException();
        }

        public TerminalFormViewModel RenderTerminalFormViewModel()
        {
            throw new System.NotImplementedException();
        }

        public TerminalFormViewModel TerminalToEdit(int id)
        {
            throw new System.NotImplementedException();
        }

        public void SaveTerminal(TerminalFormViewModel viewModel, string action)
        {
            throw new System.NotImplementedException();
        }

        public Terminal TerminalDetails(int id)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteTerminal(int id)
        {
            throw new System.NotImplementedException();
        }

        public TerminalFormViewModel InitializeNewFormViewModel(TerminalFormViewModel viewModel)
        {
            throw new System.NotImplementedException();
        }
    }
}