using Newtonsoft.Json;
using OctagonPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace OctagonPlatform.Helpers
{
    public static class Terminals
    {
        public static List<TerminalMessage> GetTerminalMessages(string terminalId)
        {
            string url = "http://apiatm.azurewebsites.net/api/response/viewmessage/" + terminalId;
            var json = new WebClient().DownloadString(url);
            List<TerminalMessage> list = JsonConvert.DeserializeObject<List<TerminalMessage>>(json);
            return list;
        }
    }
}