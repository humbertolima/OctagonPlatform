
using OctagonPlatform.Models;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace OctagonPlatform.PersistanceRepository
{
    public class ReportFilterRepository : GenericRepository<ReportFilter>, IReportFilter
    {
        public void DeleteReportFilter(ReportFilter rfilter)
        {
            if (rfilter != null) Table.Remove(rfilter);
        }
    }
}