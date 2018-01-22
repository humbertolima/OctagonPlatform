
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
        public IEnumerable<Schedule> GetScheduleByUser(int userId)
        {         

            return Table.Where(p => p.UserId == userId).ToList();
        }
        public IEnumerable<Schedule> GetScheduleByParent( int partnerId)
        {
            Partner pa = Context.Partners.Find(partnerId);
            int parent = pa.ParentId ?? pa.Id;
            IEnumerable<Partner> listpartner = GetPartnerByParentId(parent);
            var list4 = (from q in listpartner
                         join u in Context.Users on q.Id equals u.PartnerId
                         join m in Table on u.Id equals m.UserId
                         select m).ToList();           

            return list4;
        }
        public IEnumerable<string> GetAllSchedule(string value, int userId)
        {
            try
            {
              //  IEnumerable<Partner> listpartner = GetPartnerByParentId(partnerId);
               /* var list4 = (from q in listpartner
                             join m in Table on q.Id equals m.PartnerId
                             join u in Context.Users on q.Id equals u.PartnerId
                             where u.PartnerId == partnerId && u.Id == userId
                             select m).ToList();*/
                return Table.Where(p => p.UserId == userId).Where(b => b.Name.ToLower().Contains(value.ToLower())).Select(b => b.Name).ToList();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}