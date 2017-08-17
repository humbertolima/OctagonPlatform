using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models.ManagmentViewModel
{
    public class PartnerContactManagmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The partner is required")]
        [Display(Name = "Partner")]
        public int PartnerId { get; set; }

        public IEnumerable<Partner> Partners { get; set; }

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

        public IEnumerable<ContactType> ContactTypes { get; set; }

        [Required(ErrorMessage = "The phone is required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Phone number is not valid")]
        public ulong Phone { get; set; }

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

        public IEnumerable<Country> Countries { get; set; }

        [Required(ErrorMessage = "The state is required")]
        [Display(Name = "State")]
        public int StateId { get; set; }

        public IEnumerable<State> States { get; set; }

        [Required(ErrorMessage = "The city is required")]
        [Display(Name = "City")]
        public int CityId { get; set; }

        public IEnumerable<City> Cities { get; set; }

    }
}