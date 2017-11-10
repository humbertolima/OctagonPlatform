using AutoMapper;
using Newtonsoft.Json;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
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

        public DisputeViewModel GetTerminalTransaction(string terminalId)
        {
            //pendiente pasarle por parametro el secuencialnumber
            var url = "http://apiatm.azurewebsites.net/api/request/transactions/" + "NH061617" + "/" + "0088";
            var json = new WebClient().DownloadString(url);
            var transaction = JsonConvert.DeserializeObject<List<Transaction>>(json);

            var viewModel = new DisputeViewModel() { Transaction = transaction[0] };

            return viewModel;
        }

        public void DisputeAdd(DisputeViewModel viewModel)
        {
            try
            {
                var dispute = Mapper.Map<DisputeViewModel, Dispute>(viewModel);

                Table.Add(dispute);
                Save();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}