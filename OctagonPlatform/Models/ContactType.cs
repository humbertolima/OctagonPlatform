using OctagonPlatform.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models
{
    public class ContactType:ISoftDeleted
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Contact type's name is required")]
        [StringLength(20)]
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
