using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OctagonPlatform.PersistanceRepository
{
    public class TerminalRepository: GenericRepository<Terminal>, ITerminalRepository
    {
        public IEnumerable<Terminal> GetAllTerminals()
        {
            return Table.Where(x => !x.Deleted)
                .Include(x => x.Partner)
                .Include(x => x.Country)
                .Include(x => x.State)
                .Include(x => x.City)
                .ToList();
        }

        public IEnumerable<Terminal> Search(string search)
        {
            return Table.Where(x => !x.Deleted)
                .Include(x => x.Partner)
                .Include(x => x.Country)
                .Include(x => x.State)
                .Include(x => x.City)
                .ToList();
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