using OctagonPlatform.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationMonitor.Repository
{
    public class TerminalsRepository
    {
        public void GetAllTerminals()
        {
            ApplicationDbContext DbContext = new ApplicationDbContext();

            //var result = DbContext
            //    .Terminals.Include(c => c.WorkingHours).Where();

            //foreach (var item in result)
            //{
            //}
        }
    }

}
