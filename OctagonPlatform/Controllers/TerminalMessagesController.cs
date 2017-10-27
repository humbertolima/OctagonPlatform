using Newtonsoft.Json;
using OctagonPlatform.Models;
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
            var list = Helpers.Terminals.GetTerminalMessages(terminalId);
            return View(list);
        }
        
        public PartialViewResult MessagesDetail(string messagesId, string direction)
        {
            string url = "http://apiatm.azurewebsites.net/api/response/message/" + messagesId + "/" + direction;
            
            var json = new WebClient().DownloadString(url);

            var list = JsonConvert.DeserializeAnonymousType(json, new Dictionary<string, string>());
            
            return PartialView(list);
        }
    }
}