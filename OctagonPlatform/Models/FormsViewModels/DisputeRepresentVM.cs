using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class DisputeRepresentVM
    {
        public int Id { get; set; }

        public int disputeId { get; set; }
        public Dispute Dispute { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string CC { get; set; }

        public string Comments { get; set; }

        public byte[] AttachData { get; set; }
        
        public HttpPostedFileBase File { get; set; }
    }
}