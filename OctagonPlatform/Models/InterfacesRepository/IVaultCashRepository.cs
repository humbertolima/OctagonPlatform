using OctagonPlatform.Models.FormsViewModels;
using System.Collections.Generic;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface IVaultCashRepository
    {
        IEnumerable<VaultCash> GetAllVaultCash();

        IEnumerable<VaultCash> Search(string search);

        VaultCashFormViewModel RenderVaultCashFormViewModel(int terminalId);

        VaultCashFormViewModel VaultCashToEdit(int id);

        void SaveVaultCash(VaultCashFormViewModel viewModel, string action);

        VaultCash VaultCashDetails(int id);

        void DeleteVaultCash(int id);

        VaultCashFormViewModel InitializeNewVaultCashFormViewModel(VaultCashFormViewModel viewModel);
    }
}
