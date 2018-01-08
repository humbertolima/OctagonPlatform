
using OctagonPlatform.Models;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace OctagonPlatform.PersistanceRepository
{
    public class SubscriptionRepository : GenericRepository<SubscriptionModel>, ISubscription         
    {
        
    }
}