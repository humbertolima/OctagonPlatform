using OctagonPlatform.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class TerminalStatusFormFilterVM
    {
        public string Partner { get; set; }
        [HiddenInput]
        public int PartnerId { get; set; }
        public string Group { get; set; }
        [HiddenInput]
        public int GroupId { get; set; }
       
        public StatusType.Status Status { get; set; }
        public string State { get; set; }
        [HiddenInput]
        public int StateId { get; set; }
        public string City { get; set; }
        [HiddenInput]
        public int CityId { get; set; }
        public string ZipCode { get; set; }
        public TerminalStatusFormFilterVM()
        {            
            PartnerId = -1;
            GroupId = -1;
        }
    }
}