using OctagonPlatform.Models;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace OctagonPlatform.PersistanceRepository
{
    public class TerminalAlertRepository : GenericRepository<TerminalAlert>, ITerminalAlertRepository
    {
        public IEnumerable GetAllAlerts()
        {
            var result = Table.Include("Terminal").Select(c => c).ToList();

            return result;
        }
    }
}