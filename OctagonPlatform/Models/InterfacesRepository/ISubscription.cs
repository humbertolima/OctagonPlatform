

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models.InterfacesRepository
{
   public interface ISubscription : IGenericRepository<SubscriptionModel>
    {
        IEnumerable<SubscriptionModel> GetSubscriptionsIncluding(int partnerId);
    }
}