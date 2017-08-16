using OctagonPlatform.Helpers;
using System;

namespace OctagonPlatform.Models
{
    public class PartnerContact:IAuditEntity,ISoftDeleted
    {
        public int Id { get; set; }

        public int PartnerId { get; set; }

        public Partner Partner { get; set; }

        public string BusinessName { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int ContactTypeId { get; set; }

        public ContactType ContactType { get; set; }

        public ulong Phone  { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public int Zip { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public int StateId { get; set; }

        public State State { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public DateTime? CreatedAt { get; set; }

        public User CreatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public User DeletedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public User UpdatedBy { get; set; }

        public bool? Deleted { get; set; }
    }
}