using System;
using System.Net;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {

        public ActionResult Index()
        {
            try
            {
                ViewBag.priceBTC = "BTC " + GetBitcoinPrice(1);
                Session["BTC"] = ViewBag.priceBTC;
                ViewBag.priceETH = "ETH " + GetBitcoinPrice(2);
                Session["ETH"] = ViewBag.priceETH;
                ViewBag.priceLTC = "LTC " + GetBitcoinPrice(3);
                Session["LTC"] = ViewBag.priceLTC;
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

                var URL = "http://buywidget220170913030514.azurewebsites.net/api/ticker/" + i;

                var json = new WebClient().DownloadString(URL);
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