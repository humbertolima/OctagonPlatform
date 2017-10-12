using Newtonsoft.Json;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    public class TerminalMessagesController : Controller
    {
        private readonly ITerminalRepository _repository;

        public TerminalMessagesController(ITerminalRepository repository)
        {
            _repository = repository;
        }

        // GET: TerminalMessages
        public ActionResult Index(string terminalId)
        {
                string url = "http://apiatm.azurewebsites.net/api/response/viewmessage/" + terminalId;
                var json = new WebClient().DownloadString(url);
                List<TerminalMessage> list = JsonConvert.DeserializeObject<List<TerminalMessage>>(json);

            return View(list);
        }
    }
}