

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models.InterfacesRepository
{
   public interface IReports : IGenericRepository<ReportModel>
    {
        IEnumerable<ReportModel> GetbyName(string name);
        IEnumerable<ReportModel> GetReportsDasboard();


    }
}