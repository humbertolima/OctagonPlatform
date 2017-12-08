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
        public IEnumerable<dynamic> GetAllGroup(string term)
        {
            try
            {
                return Table.Where(b => b.Name.Contains(term)).Select(b => new { label = b.Name, value = b.Id }).ToList();
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
    }
}