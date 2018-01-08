
using OctagonPlatform.Models;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace OctagonPlatform.PersistanceRepository
{
    public class ScheduleRepository : GenericRepository<Schedule>, ISchedule
    {
        public IEnumerable<Schedule> GetScheduleByUser(int userId, int partnerId)
        {
            IEnumerable<Partner> listpartner = GetPartnerByParentId(partnerId);
            var list4 = (from q in listpartner
                         join m in Table on q.Id equals m.PartnerId
                         join u in Context.Users on q.Id equals u.PartnerId
                         where u.PartnerId == partnerId && u.Id == userId
                         select m).ToList();

            return list4;
        }
        public IEnumerable<string> GetAllSchedule(string value, int partnerId, int userId)
        {
            try
            {
                IEnumerable<Partner> listpartner = GetPartnerByParentId(partnerId);
                var list4 = (from q in listpartner
                             join m in Table on q.Id equals m.PartnerId
                             join u in Context.Users on q.Id equals u.PartnerId
                             where u.PartnerId == partnerId && u.Id == userId
                             select m).ToList();
                return list4.Where(b => b.Name.ToLower().Contains(value.ToLower())).Select(b => b.Name).ToList();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}