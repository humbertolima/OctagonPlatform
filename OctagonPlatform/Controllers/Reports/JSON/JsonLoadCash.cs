using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Controllers.Reports.JSON
{
   
    public class JsonLoadCash
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int AmountPrevius { get; set; }
        public int AmountLoad { get; set; }
        public int AmountCurrent { get; set; }
        public string TerminalId { get; set; }
    }
}