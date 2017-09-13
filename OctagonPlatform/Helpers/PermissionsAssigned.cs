using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctagonPlatform.Helpers
{
    public class PermissionAssigned : SelectListItem
    {   
        public string Type { get; set; }
    }
}