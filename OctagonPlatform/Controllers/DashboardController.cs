using System;
using System.Net;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [Authorize]
    public class DashboardController : BaseController
    {

        public ActionResult Index()
        {
            try
            {
                //ViewBag.priceBTC = GetBitcoinPrice(1);
                //Session["BTC"] = ViewBag.priceBTC;
                //ViewBag.priceETH = GetBitcoinPrice(2);
                //Session["ETH"] = ViewBag.priceETH;
                //ViewBag.priceLTC = GetBitcoinPrice(3);
                //Session["LTC"] = ViewBag.priceLTC;
                return View();
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }

        private static string GetBitcoinPrice(int i)
        {
            try
            {
                var price = " ";

                var url = "http://buywidget220170913030514.azurewebsites.net/api/ticker/" + i;

                var json = new WebClient().DownloadString(url);
                json = json.Replace("\"", "");
                price = price + json;

                return price;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}