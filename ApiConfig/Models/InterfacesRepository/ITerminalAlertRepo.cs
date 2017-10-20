using OctagonPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiConfig.Models.InterfacesRepository
{
    public interface ITerminalAlertRepo
    {
        void SaveAlerts(List<KeyValuePair<string, string>> alerts);

        void NotifAlertToUser(TerminalAlert terminalAlert);
    }
}
