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
            return new TerminalFormViewModel()
            {
                Countries = Context.Countries.ToList(),
                States = Context.States.Where(x => x.CountryId == 231).ToList(),
                Cities = Context.Cities.Where(x => x.StateId == 3930).ToList(),
                Partners = Context.Partners.Where(x => !x.Deleted).ToList(),
                
            };
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
            return Table.Where(x => x.Id == id)
                .Include(x => x.Cassettes)
                .Include(x => x.DefaultBankAccount)
                .Include(x => x.Partner)
                .Include(x => x.Country)
                .Include(x => x.State)
                .Include(x => x.City)
                .Include(x => x.Contracts)
                .Include(x => x.Documents)
                .Include(x => x.Events)
                .Include(x => x.InterChanges)
                .Include(x => x.LoadCashs)
                .Include(x => x.Make)
                .Include(x => x.Model)
                .Include(x => x.Users)
                .Include(x => x.VaultCash)
                .Include(x => x.Surcharges)
                .Include(x => x.Notes)
                .Include(x => x.TerminalContacts)
                .Include(x => x.TerminalPictures)
                .Include(x => x.Transactions)
                .FirstOrDefault();
        }

        public void DeleteTerminal(int id)
        {
           Delete(id);
        }

        public TerminalFormViewModel InitializeNewFormViewModel(TerminalFormViewModel viewModel)
        {
            throw new System.NotImplementedException();
        }
    }
}