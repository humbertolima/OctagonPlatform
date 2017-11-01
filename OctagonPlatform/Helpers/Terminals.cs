using Newtonsoft.Json;
using OctagonPlatform.Models;
using System;
using System.Collections.Generic;
using System.Net;

namespace OctagonPlatform.Helpers
{
    public static class Terminals
    {
        public static List<TerminalMessage> GetTerminalMessages(string terminalId)
        {
            try
            {
                string url = "http://apiatm.azurewebsites.net/api/response/viewmessage/" + terminalId;
                var json = new WebClient().DownloadString(url);
                List<TerminalMessage> list = JsonConvert.DeserializeObject<List<TerminalMessage>>(json);
                return list;
            }
            catch (Exception ex)
            {
                
               throw new Exception(ex.Message + ", Messages not found. ");
            }
        }
    }
}