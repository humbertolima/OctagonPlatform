using OctagonPlatform.Models;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctagonPlatform.PersistanceRepository
{
    public class DisputeRepository : GenericRepository<Dispute>, IDisputeRepository
    {
        public IEnumerable<Dispute> GetAllDispute()
        {
            throw new NotImplementedException();
        }
    }
}