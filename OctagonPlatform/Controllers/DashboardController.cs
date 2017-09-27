using System;
using System.Net;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [AllowAnonymous]
    public class DashboardController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.priceBTC = "BTC " + GetBitcoinPrice(1);
            Session["BTC"] = ViewBag.priceBTC;
            ViewBag.priceETH = "ETH " + GetBitcoinPrice(2);
            Session["ETH"] = ViewBag.priceETH;
            ViewBag.priceLTC = "LTC " + GetBitcoinPrice(3);
            Session["LTC"] = ViewBag.priceLTC;
            return View();
        }

        private string GetBitcoinPrice(int i)
        {
            var price = " ";

            var URL = "http://buywidget220170913030514.azurewebsites.net/api/ticker/"+i;

            var json = new WebClient().DownloadString(URL);
            json = json.Replace("\"", "");
            price = price + json;

            return price;
        }
    }
}