using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OctagonPlatform.Controllers
{

	
public abstract class BaseController : Controller
    {
        private string CultureInfoName { get; set; }
        public CultureInfo CultureInfoSession { get; set; }
        protected override void Initialize(RequestContext requestContext)
        {
            if (requestContext.HttpContext.Session != null)
            {
                CultureInfoName = "en-US";
                if (!(requestContext.HttpContext.Session["CultureInfo"] == null))
                {
                    CultureInfoName = requestContext.HttpContext.Session["CultureInfo"].ToString();
                }
                if (!string.IsNullOrEmpty(CultureInfoName))
                {
                    try
                    {
                        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(CultureInfoName);
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo(CultureInfoName);
                        CultureInfoSession = new CultureInfo(CultureInfoName);
                        HttpCookie cultureCookie = new HttpCookie("_culture");
                        cultureCookie.Value = CultureInfoName.Substring(0,2);
                        cultureCookie.Expires = DateTime.UtcNow.AddYears(1);

                        HttpCookie cookie = requestContext.HttpContext.Request.Cookies["_culture"];
                        if (cookie != null)
                            cookie.Value = CultureInfoName.Substring(0, 2);   // update cookie value
                        else
                        {
                            cookie = new HttpCookie("_culture");
                            cookie.Value = CultureInfoName.Substring(0, 2);
                            cookie.Expires = DateTime.Now.AddYears(1);
                        }
                        requestContext.HttpContext.Response.Cookies.Add(cookie);
                      
                    }
                    catch (Exception)
                    {
                        throw new NotSupportedException($"Invalid language code '{CultureInfoName}'.");
                    }
                }
            }

            
            base.Initialize(requestContext);
        }
    }
}