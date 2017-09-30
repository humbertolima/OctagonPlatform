using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System.Collections.Generic;

namespace OctagonPlatform.PersistanceRepository
{
    public class VaultCashRespository: GenericRepository<VaultCash>, IVaultCashRepository
    {
        public IEnumerable<VaultCash> GetAllVaultCash()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<VaultCash> Search(string search)
        {
            throw new System.NotImplementedException();
        }

        public VaultCashFormViewModel RenderVaultCashFormViewModel(int terminalId)
        {
            throw new System.NotImplementedException();
        }

        public VaultCashFormViewModel VaultCashToEdit(int id)
        {
            throw new System.NotImplementedException();
        }

        public void SaveVaultCash(VaultCashFormViewModel viewModel, string action)
        {
            throw new System.NotImplementedException();
        }

        public VaultCash VaultCashDetails(int id)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteVaultCash(int id)
        {
            throw new System.NotImplementedException();
        }

        public VaultCashFormViewModel InitializeNewVaultCashFormViewModel(VaultCashFormViewModel viewModel)
        {
            throw new System.NotImplementedException();
        }
    }
}