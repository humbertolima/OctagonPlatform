
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
        public IEnumerable<Subreport> GetSubscriptionsIncluding(int userId)
        {
            return Table.Where(p => p.UserId == userId).Include(p => p.ReportFilters).Include(p => p.Schedule).Include(p => p.User)
                 .Join(Context.Users, // the source table of the inner join
      table => table.CreatedBy,        // Select the primary key (the first part of the "on" clause in an sql "join" statement)
      user => user.Id,   // Select the foreign key (the second part of the "on" clause)
      (table, user) => new Subreport { Model =table, Username =user.Name+user.LastName }) // selection
                .ToList();
        }
    }
    public class Subreport
    {
        public SubscriptionModel Model { get; set; }
        public string Username { get; set; }
    }
}