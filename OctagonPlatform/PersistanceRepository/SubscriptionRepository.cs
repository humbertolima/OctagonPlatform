
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
        public IEnumerable<SubscriptionModel> GetSubscriptionsIncluding(int userId)
        {
            return Table.Where(p => p.UserId == userId).Include(p => p.ReportFilters).Include(p => p.Schedule).Include(p => p.User).ToList();
        }
    }
}