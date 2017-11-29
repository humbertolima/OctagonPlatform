using OctagonPlatform.Models.FormsViewModels;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface IVaultCashRepository
    {
        VaultCash GetVaultCash(int terminalId);

        VaultCashFormViewModel RenderVaultCashFormViewModel(int terminalId);

        VaultCashFormViewModel VaultCashToEdit(int id);

        void SaveVaultCash(VaultCashFormViewModel viewModel, string action);

        VaultCash VaultCashDetails(int id);

        void DeleteVaultCash(int id);

        VaultCashFormViewModel InitializeNewVaultCashFormViewModel(VaultCashFormViewModel viewModel);
    }
}
