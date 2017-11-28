﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models.InterfacesRepository
{
   
    public interface IReportGroup : IGenericRepository<ReportGroupModel>
    {
        void DeleteRange(string[] ids);
    }
}