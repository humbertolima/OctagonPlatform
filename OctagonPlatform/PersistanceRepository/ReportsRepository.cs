
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
        public IEnumerable<ReportModel> GetAllReports( int partnerId, int userId)
        {
            try
            {
                IEnumerable<Partner> listpartner = GetPartnerByParentId(partnerId);
                var list4 = (from q in listpartner                            
                             join u in Context.Users on q.Id equals u.PartnerId
                             join m in Table on q.Id equals m.UserId
                             where  u.Id == userId
                             select m).ToList();
                return list4;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}