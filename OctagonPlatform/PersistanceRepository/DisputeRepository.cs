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


        public IEnumerable<DisputeViewModel> GetAllDispute()
        {
            var disputes = Table.Include("Terminal").Include("DisputeRepresent").ToList();
            var viewModel = new List<DisputeViewModel>();

            foreach (var item in disputes)
            {
                viewModel.Add(Mapper.Map<Dispute, DisputeViewModel>(item));

            }
            return viewModel;
        }

        public DisputeViewModel GetDispute(int id)
        {
            try
            {
                var dispute = Table.Include("Terminal").Include("DisputeRepresent").FirstOrDefault(m => m.Id == id);

                var viewModel = new DisputeViewModel();

                viewModel = Mapper.Map<Dispute, DisputeViewModel>(dispute);

                return viewModel;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public DisputeViewModel GetTerminalTransaction(string terminalId)
        {
            //pendiente pasarle por parametro el secuencialnumber
            var url = "http://apiatm.azurewebsites.net/api/request/transactions/" + "NH061617" + "/" + "0088";
            var json = new WebClient().DownloadString(url);
            var transaction = JsonConvert.DeserializeObject<List<Transaction>>(json);


            if (transaction.Count() > 0)
            {
                var viewModel = new DisputeViewModel()
                {
                    TerminalId = transaction.FirstOrDefault().TerminalId,
                    SecuenceNumber = transaction.FirstOrDefault().SequenceNumber,
                    TransacNo = transaction.FirstOrDefault().Id,
                };
                return viewModel;
            }
            else
            {
                return new DisputeViewModel(); 
            }


            
        }

        public void DisputeAdd(DisputeViewModel viewModel)
        {
            try
            {
                //var dispute = new Dispute() { IndexId = viewModel.Terminal.Id };
                var dispute = Mapper.Map<DisputeViewModel, Dispute>(viewModel);

                Table.Add(dispute);
                Save();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DisputeUpdate(DisputeViewModel viewModel)
        {
            try
            {
                var dispute = new Dispute() { IndexId = viewModel.Terminal.Id };

                dispute = Mapper.Map<DisputeViewModel, Dispute>(viewModel);

                Edit(dispute);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}