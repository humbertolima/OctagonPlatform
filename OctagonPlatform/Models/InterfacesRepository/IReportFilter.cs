﻿

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface IReportFilter : IGenericRepository<ReportFilter>
    {
        void DeleteReportFilter(ReportFilter rfilter);
    }
}