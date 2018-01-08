using OctagonPlatform.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models
{
    public class User:IAuditEntity, ISoftDeleted
    {
        [Key]
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

        [Required(ErrorMessage ="Phone number is required")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "The partner's status is required")]
        public StatusType.Status Status { get; set; }

        [Required(ErrorMessage ="Partner is required")]
        [Display(Name="Partner")]
        public int PartnerId { get; set; }
        public Partner Partner { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Key { get; set; }

        public bool IsLocked { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(50, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 5)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        public ICollection<Permission> Permissions { get; set; }

        public ICollection<Terminal> Terminals { get; set; }

        public ICollection<BankAccount> BankAccounts { get; set; }
        public ICollection<ReportModel> Reports { get; set; }



        public bool Deleted { get; set; }
        public DateTime? CreatedAt { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }

        public int? DeletedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }
        public string CreatedByName { get; set; }
        public string DeletedByName { get; set; }

        public User()
        {
            Permissions = new Collection<Permission>();
            Terminals = new Collection<Terminal>();

            BankAccounts = new Collection<BankAccount>();
        }

        
    }
}