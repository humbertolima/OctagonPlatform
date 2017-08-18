using OctagonPlatform.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models
{
    public class PartnerContact:IAuditEntity,ISoftDeleted
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The partner is required")]
        [Display(Name = "Partner")]
        public int PartnerId { get; set; }

        public Partner Partner { get; set; }

        [Required(ErrorMessage = "The name is required")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Last name is required")]
        [StringLength(20)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(16, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 5)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The contact type is required")]
        [Display(Name = "Contact type")]
        public int ContactTypeId { get; set; }

        public ContactType ContactType { get; set; }

        [Required(ErrorMessage = "The phone is required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Phone number is not valid")]
        public ulong Phone  { get; set; }

        [Required(ErrorMessage = "The address1 is required")]
        [StringLength(50)]
        public string Address1 { get; set; }

        [StringLength(50)]
        public string Address2 { get; set; }

        [Required(ErrorMessage = "The zip code is required")]
        [Display(Name = "Zip Code")]
        public int Zip { get; set; }

        [Required(ErrorMessage = "The country is required")]
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        public Country Country { get; set; }

        [Required(ErrorMessage = "The state is required")]
        [Display(Name = "State")]
        public int StateId { get; set; }

        public State State { get; set; }

        [Required(ErrorMessage = "The city is required")]
        [Display(Name = "City")]
        public int CityId { get; set; }

        public City City { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string DeletedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }

        public bool? Deleted { get; set; }
    }
}