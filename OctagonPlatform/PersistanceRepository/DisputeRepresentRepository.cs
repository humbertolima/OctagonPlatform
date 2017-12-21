using AutoMapper;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Drawing;

namespace OctagonPlatform.PersistanceRepository
{
    public class DisputeRepresentRepository : GenericRepository<DisputeRepresent>, IDisputeRepresentRepository
    {
        public DisputeRepresent AddRepresent(DisputeRepresentVM viewModel)
        {
            viewModel.Image =Helpers.ConvertTo.ImageToByteArray(viewModel.File);

            var disputeRepresent = Mapper.Map<DisputeRepresentVM, DisputeRepresent>(viewModel);
            //disputeRepresent.AttachData = data;
            Table.Add(disputeRepresent);
            Save();
            return disputeRepresent;

        }

        public IEnumerable<DisputeRepresent> GetAllDispute()
        {
            try
            {
                var disputesRepresents = Table.ToList();
                return disputesRepresents;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}