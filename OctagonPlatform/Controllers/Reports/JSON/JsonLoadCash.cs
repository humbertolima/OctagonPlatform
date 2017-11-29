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
    public class JsonLoadCashReport
    {
       
        public string Locationname { get; set; }
        public DateTime Date { get; set; }
        public int AmountPrevius { get; set; }
        public int AmountLoad { get; set; }
        public int AmountCurrent { get; set; }
        public string TerminalId { get; set; }
        public JsonLoadCashReport( string locationname, DateTime date, int amountPrevius, int amountLoad, int amountCurrent, string terminalId)
        {
           
            Locationname = locationname;
            Date = date;
            AmountPrevius = amountPrevius;
            AmountLoad = amountLoad;
            AmountCurrent = amountCurrent;
            TerminalId = terminalId;
        }
    }
    public class JsonLoadCashChart
    {
        public string Date { get; set; }
        public int AmountPrevius { get; set; }
        public int AmountLoad { get; set; }
        
        public JsonLoadCashChart(string date, int amountPrevius, int amountLoad)
        {           
            AmountPrevius = amountPrevius;
            AmountLoad = amountLoad;          
            Date = date;
        }
    }
}