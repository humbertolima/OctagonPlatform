using OctagonPlatform.Models.FormsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface IDisputeRepository
    {
        DisputeViewModel GetDispute(int disputeId);
        
        IEnumerable<DisputeViewModel> GetAllDispute();

        DisputeViewModel GetTerminalTransaction(string terminalId);

        void DisputeAdd(DisputeViewModel viewModel);

        void DisputeUpdate(DisputeViewModel viewModel);
    }
}
