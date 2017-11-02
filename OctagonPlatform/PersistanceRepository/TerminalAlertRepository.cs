using OctagonPlatform.Models;
using OctagonPlatform.Models.InterfacesRepository;
using System.Collections;
using System.Linq;

namespace OctagonPlatform.PersistanceRepository
{
    public class TerminalAlertRepository : GenericRepository<TerminalAlert>, ITerminalAlertRepositoryAPI
    {
        public IEnumerable GetAllAlerts()
        {
            var result = Table.Include("Terminal").Select(c => c).ToList();

            return result;
        }
    }
}