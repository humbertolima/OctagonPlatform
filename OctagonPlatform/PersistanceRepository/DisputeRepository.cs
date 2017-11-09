using Newtonsoft.Json;
using OctagonPlatform.Models;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace OctagonPlatform.PersistanceRepository
{
    public class DisputeRepository : GenericRepository<Dispute>, IDisputeRepository
    {
        public IEnumerable<Dispute> GetAllDispute()
        {
            var disputes = Table.ToList();

            return disputes;
        }

        public Transaction GetTerminalTransaction(string terminalId)
        {
            //pendiente pasarle por parametro el secuencialnumber
            var url = "http://apiatm.azurewebsites.net/api/key/getbyterminal/" + terminalId +"/"+0088;
            var json = new WebClient().DownloadString(url);
            var transaction = JsonConvert.DeserializeObject<Transaction>(json);

            return transaction;
        }
    }
}