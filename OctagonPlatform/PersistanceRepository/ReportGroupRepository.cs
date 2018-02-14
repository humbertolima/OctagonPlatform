using OctagonPlatform.Models;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OctagonPlatform.PersistanceRepository
{
    public class ReportGroupRepository : GenericRepository<ReportGroupModel>, IReportGroup
    {
        
        public void DeleteRange(string[] ids)
        {
          Table.RemoveRange( Table.Where(c => ids.Contains(c.Id.ToString())).ToList());
            Context.SaveChanges();
        }

        public object FindByName(string name)
        {
            return Table.Where(c => c.Name.Equals( name,System.StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }
        public IEnumerable<dynamic> GetAllGroup(string term,int partnerId)
        {
            try
            {
                IEnumerable<Partner> listpartner = GetPartnerByParentId(partnerId);
                var list4 = (from q in listpartner join m in Table on q.Id equals m.PartnerId select m).ToList();

                return list4.Where(b => b.Name.ToLower().Contains(term.ToLower())).Select(b => new { label = b.Name, value = b.Id }).ToList();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public ReportGroupModel GetGroupById(int id)
        {             
            return Table.Where(c => c.Id == id).Include(c => c.Terminals).FirstOrDefault();
        }

        public Terminal GetTerminal(int v)
        {
            return Context.Terminals.Where(p => p.Id == v).SingleOrDefault();
        }
    }
}