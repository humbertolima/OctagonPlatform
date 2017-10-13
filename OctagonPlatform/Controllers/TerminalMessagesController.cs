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
            string url = "http://apiatm.azurewebsites.net/api/response/viewmessage/" + terminalId;
            var json = new WebClient().DownloadString(url);
            List<TerminalMessage> list = JsonConvert.DeserializeObject<List<TerminalMessage>>(json);

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