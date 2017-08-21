﻿using OctagonPlatform.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class PartnerFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The parent partner is required")]
        [Display(Name = "Parent")]
        public int ParentId { get; set; }

        public IEnumerable<Partner> Parents { get; set; }

        [Required(ErrorMessage = "The partner business's name is required")]
        [StringLength(50)]
        [Display(Name = "Business's name")]
        public string BusinessName { get; set; }

        [Required(ErrorMessage = "The partner's status is required")]
        public StatusType.Status Status { get; set; }

        [Required(ErrorMessage = "The address1 is required")]
        [StringLength(50)]
        public string Address1 { get; set; }

        [StringLength(50)]
        public string Address2 { get; set; }

        [Required(ErrorMessage = "The partner's country is required")]
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        public IEnumerable<Country> Countries { get; set; }

        [Required(ErrorMessage = "The partner's state is required")]
        [Display(Name = "State")]
        public int StateId { get; set; }

        public IEnumerable<State> States { get; set; }

        [Required(ErrorMessage = "The partner's city is required")]
        [Display(Name = "City")]
        public int CityId { get; set; }

        public IEnumerable<City> Cities { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(16, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 5)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Display(Name = "Work Phone")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Work phone number is not valid")]
        public long? WorkPhone { get; set; }

        [Display(Name = "Mobile Phone")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Mobile phone number is not valid")]
        public long Mobile { get; set; }

        [Display(Name = "Fax Number")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Fax number is not valid")]
        public long? Fax { get; set; }

        public string WebSite { get; set; }

    }
}