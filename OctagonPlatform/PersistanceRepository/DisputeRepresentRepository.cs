using AutoMapper;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Web;

namespace OctagonPlatform.PersistanceRepository
{
    public class DisputeRepresentRepository : GenericRepository<DisputeRepresent>, IDisputeRepresentRepository
    {
        public void AddRepresent(DisputeRepresentVM viewModel)
        {

            byte[] data = new byte[viewModel.File.ContentLength];                     //convierte a byte[] el length de el file que viene
            viewModel.File.InputStream.Read(data, 0, viewModel.File.ContentLength);

            var disputeRepresent = Mapper.Map<DisputeRepresentVM, DisputeRepresent>(viewModel);

            Table.Add(disputeRepresent);
            Save();
        }
    }
}