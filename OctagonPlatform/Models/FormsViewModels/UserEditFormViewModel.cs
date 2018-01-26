﻿using OctagonPlatform.Helpers;
using OctagonPlatform.Models.InterfacesRepository;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OctagonPlatform.Models.FormsViewModels

{
    public class UserEditFormViewModel : IError
    {
        public UserEditFormViewModel()
        {
          

        }

        public int Id { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User name is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [DataType(DataType.PhoneNumber)]
        [StringValidation(ErrorMessage = "Phone must be only numbers, with no spaces. ")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "The partner's status is required")]
        public StatusType.Status Status { get; set; }

        [Required(ErrorMessage = "Partner is required")]
        [Display(Name = "Partner")]
        public int PartnerId { get; set; }

        public Partner Partner { get; set; }

        public IEnumerable<Partner> Partners { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }

        public bool IsLocked { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        
        public ICollection<Permission> Permissions { get; set; }
        
        public string Error { get; set; }
        public List<System.Web.Mvc.SelectListItem> TimeZoneList { get; set; }
        public string TimeZoneInfo { get; set; }
    }
}
