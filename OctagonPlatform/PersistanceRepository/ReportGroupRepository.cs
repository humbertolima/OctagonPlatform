using OctagonPlatform.Models;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
                return Table.Where(b => b.Name.Contains(term)).Select(b => b.Name).ToList();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}