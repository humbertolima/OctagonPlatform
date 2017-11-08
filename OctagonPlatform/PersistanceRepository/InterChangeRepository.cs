using OctagonPlatform.Migrations;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;

namespace OctagonPlatform.PersistanceRepository
{
    public class InterChangeRepository:GenericRepository<InterChange>, IInterChangeRepository
    {
        public IEnumerable<Models.InterChange> GetAllInterChanges(int terminalId)
        {
            throw new NotImplementedException();
        }

        public InterChangeFormViewModel RenderInterChangeFormViewModel(int terminalId)
        {
            throw new NotImplementedException();
        }

        public InterChangeFormViewModel InterChangeToEdit(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveSurcharge(InterChangeFormViewModel viewModel, string action)
        {
            throw new NotImplementedException();
        }

        public Models.InterChange InterChangeDetails(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteInterChange(int id)
        {
            throw new NotImplementedException();
        }

        public InterChangeFormViewModel InitializeNewInterChangeFormViewModel(InterChangeFormViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}