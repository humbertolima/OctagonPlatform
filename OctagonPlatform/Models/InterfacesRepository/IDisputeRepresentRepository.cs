using OctagonPlatform.Models.FormsViewModels;
using System.Collections.Generic;
using System.Drawing;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface IDisputeRepresentRepository
    {
        DisputeRepresent AddRepresent(DisputeRepresentVM viewModel);

        IEnumerable<DisputeRepresent> GetAllDispute();
    }
}
