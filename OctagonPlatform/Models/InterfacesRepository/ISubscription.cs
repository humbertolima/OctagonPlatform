

using OctagonPlatform.PersistanceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models.InterfacesRepository
{
   public interface ISubscription : IGenericRepository<SubscriptionModel>
    {
        List<Subreport> GetSubscriptionsIncluding(int partnerId);
        List<Subreport> GetSubscriptionsParent(int partnerid);
        SubscriptionModel GetSubscriptionById(int id);
        
    }
}