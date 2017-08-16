using OctagonPlatform.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OctagonPlatform.Models
{
    public class ContactType:ISoftDeleted
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<PartnerContact> PartnerContacts { get; set; }

        //public ICollection<TerminalContact> TerminalContacts { get; set; }

        public ContactType()
        {
            PartnerContacts = new Collection<PartnerContact>();
            //TerminalContacts = new Collection<TerminalContact>();
        }

        public bool? Deleted { get; set; }
    }
}
