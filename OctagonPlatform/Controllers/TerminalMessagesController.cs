using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    public class TerminalMessagesController : Controller
    {
        // GET: TerminalMessages
        public ActionResult Index(string terminalId)
        {
            try
            {
                var list = Helpers.Terminals.GetTerminalMessages(terminalId);
                return View(list);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }
        
        public PartialViewResult MessagesDetail(string messagesId, string direction)
        {
            try
            {
                var url = "http://apiatm.azurewebsites.net/api/response/message/" + messagesId + "/" + direction;

                var json = new WebClient().DownloadString(url);

                var list = JsonConvert.DeserializeAnonymousType(json, new Dictionary<string, string>());

                return PartialView(list);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return PartialView(new Dictionary<string, string>());
            }
        }
    }
}