
using OctagonPlatform.Models;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OctagonPlatform.PersistanceRepository
{
    public class CultureInfoRepository : GenericRepository<CultureInfoModel>, ICultureInfo
    {
        public IEnumerable<CultureInfoModel> AllIncludeCountry()
        {          

            return Table.Include(c => c.Country).ToList();
        
        }
        public IEnumerable<Country> GetCountries()
        {
            return Context.Countries;
        }
    }
}