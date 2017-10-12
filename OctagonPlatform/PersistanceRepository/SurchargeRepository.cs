using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System.Collections.Generic;

namespace OctagonPlatform.PersistanceRepository
{
    public class SurchargeRepository:GenericRepository<Surcharge>, ISurchargeRepository
    {
        public IEnumerable<Surcharge> GetAllSurcharges(int terminalId)
        {
            throw new System.NotImplementedException();
        }

        public SurchargeFormViewModel RenderSurchargeFormViewModel(int terminalId)
        {
            throw new System.NotImplementedException();
        }

        public SurchargeFormViewModel SurchargeToEdit(int id)
        {
            throw new System.NotImplementedException();
        }

        public void SaveSurcharge(SurchargeFormViewModel viewModel, string action)
        {
            throw new System.NotImplementedException();
        }

        public Surcharge SurchargeDetails(int id)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteSurcharge(int id)
        {
            throw new System.NotImplementedException();
        }

        public SurchargeFormViewModel InitializeNewSurchargeFormViewModel(SurchargeFormViewModel viewModel)
        {
            throw new System.NotImplementedException();
        }
    }
}