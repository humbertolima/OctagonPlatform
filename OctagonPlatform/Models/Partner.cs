using OctagonPlatform.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OctagonPlatform.Models
{
    public sealed class Partner:IAuditEntity, ISoftDeleted
    {

        public int Id { get; set; }

        public int ParentId { get; set; }

        public Partner Parent { get; set; }

        public string BusinessName { get; set; }

        public StatusType.Status Status { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public int StateId { get; set; }

        public State State { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public string Email { get; set; }

        public ulong WorkPhone { get; set; }

        public string Mobile { get; set; }

        public ulong Fax { get; set; }

        public string WebSite { get; set; }

        public int LogoId { get; set; }

        public Logo Logo { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<PartnerContact> PartnerContacts { get; set; }

        /*
         public ICollection<Terminal> Terminals { get; set; }

         public ICollection<BankAccount> BankAccounts { get; set; }
             */

        public Partner()
        {
            Users = new Collection<User>();
            PartnerContacts = new Collection<PartnerContact>();
            /*
            Terminals = new Collection<Terminals>();
            BankAccounts = new Collection<BankAccounts>();
            */
        }

        public DateTime? CreatedAt { get; set; }

        public User CreatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public User DeletedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public User UpdatedBy { get; set; }

        public bool? Deleted { get; set; }
    }
}
