using OctagonPlatform.Models.FormsViewModels;
using System.Collections.Generic;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface IDisputeRepresentRepository
    {
        void AddRepresent(DisputeRepresentVM viewModel);

        IEnumerable<DisputeRepresent> GetAllDispute();
    }
}
