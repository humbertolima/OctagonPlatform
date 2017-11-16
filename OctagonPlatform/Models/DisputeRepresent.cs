using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models
{
    public class DisputeRepresent
    {
        [Key, ForeignKey("Dispute")]
        public int Id { get; set; }

        public Dispute Dispute { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string CC { get; set; }

        public string Comments { get; set; }

        public byte[] AttachData { get; set; }

        [NotMapped]
        public HttpPostedFileBase File { get; set; }
    }
}