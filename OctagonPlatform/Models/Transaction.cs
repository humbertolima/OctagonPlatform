using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models
{

    public class Transaction
    {
        public string SequenceNumber { get; set; }
        public int Amount1 { get; set; }
        public double Amount2 { get; set; }
        public object Miscelanea1 { get; set; }
        public object Miscelanea2 { get; set; }
        public string Miscelaneax { get; set; }
        public string Pan { get; set; }
        public string DateExp { get; set; }
        public string ServiceCode { get; set; }
        public int Id { get; set; }
        public string CommIdentifier { get; set; }
        public string Terminalidentifier { get; set; }
        public string SoftwareVersion { get; set; }
        public string ModeFlag { get; set; }
        public string Infoheader { get; set; }
        public string TerminalId { get; set; }
        public string TransCode { get; set; }
        public DateTime Date { get; set; }
        public string MessageType { get; set; }
    }

}