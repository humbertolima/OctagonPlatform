﻿

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models.InterfacesRepository
{
   public interface ICultureInfo : IGenericRepository<CultureInfoModel>
    {

         IEnumerable<CultureInfoModel> AllIncludeCountry();
        IEnumerable<Country> GetCountries();

    }
}