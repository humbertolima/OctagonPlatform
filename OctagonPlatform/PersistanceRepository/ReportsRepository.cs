
using OctagonPlatform.Models;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace OctagonPlatform.PersistanceRepository
{
    public class ReportsRepository : GenericRepository<ReportModel>, IReports               
    {
        public IEnumerable<ReportModel> GetbyName(string name)
        {
            return Table.Where(b => b.Name == name).ToList();
        }      

        public IEnumerable<ReportModel> GetReportsDasboard()
        {
            return Table.Where(b => b.IsShowDashboard == true).ToList();
        }
    }
}