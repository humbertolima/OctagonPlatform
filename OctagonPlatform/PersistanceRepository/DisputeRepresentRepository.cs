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
            
            Image img = Image.FromStream(viewModel.File.InputStream, true, true);

            viewModel.Image = ImageToByteArray(img);

            var disputeRepresent = Mapper.Map<DisputeRepresentVM, DisputeRepresent>(viewModel);
            //disputeRepresent.AttachData = data;
            Table.Add(disputeRepresent);
            Save();
            return disputeRepresent;

        }

        private byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
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