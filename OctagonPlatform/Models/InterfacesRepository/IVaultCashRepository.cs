using OctagonPlatform.Models.FormsViewModels;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface IVaultCashRepository
    {
        VaultCashVM GetVaultCash(int terminalId);

        VaultCashVM RenderVaultCashFormViewModel(int terminalId);

        VaultCashVM VaultCashToEdit(int id);

        void SaveVaultCash(VaultCashVM viewModel, string action);

        VaultCash VaultCashDetails(int id);

        void DeleteVaultCash(int id);

        VaultCashVM InitializeNewVaultCashFormViewModel(VaultCashVM viewModel);
    }
}
