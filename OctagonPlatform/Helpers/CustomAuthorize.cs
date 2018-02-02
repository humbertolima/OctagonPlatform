using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OctagonPlatform.Helpers
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // The user is not authenticated
                base.HandleUnauthorizedRequest(filterContext);
            }
            else if (!this.Roles.Split(',').Any(filterContext.HttpContext.User.IsInRole))
            {
                var temp = new TempDataDictionary
                {
                    { "Error", "Access Denied" }
                };

                // The user is not in any of the listed roles => 
                // show the unauthorized view
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Shared/Error.cshtml",
                    TempData = temp
                };
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
        public void Dispose()
        {
            this.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}