using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class ReportingGroupVM
    {
        public IEnumerable<ReportGroupModel> ListGroup { get; set; }
        public List<Terminal> ListUnassociatedTerminals { get; set; }
        public List<Terminal> ListAssociatedTerminals  { get; set; }
        public string Partner { get; set; }
        [HiddenInput]
        public int PartId { get; set; }
        public string State { get; set; }
        [HiddenInput]
        public int StateId { get; set; }
        public string City { get; set; }
        [HiddenInput]
        public int CityId { get; set; }
        public string ZipCode { get; set; }
       
        public ReportingGroupVM(IEnumerable<ReportGroupModel> groups, List<Terminal> unassociatedTn = null, List<Terminal> associatedTn = null)
        {
            ListGroup = groups;
            if (unassociatedTn != null)
                ListUnassociatedTerminals = unassociatedTn;
            else
                ListUnassociatedTerminals = new List<Terminal>();
            if (associatedTn != null)
                ListAssociatedTerminals = associatedTn;
            else
                ListAssociatedTerminals = new List<Terminal>();
        }
       
    }
}