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

        [Required(ErrorMessage = "Username is required")]
        [StringLength(16, ErrorMessage = "Must be between 3 and 16 characters", MinimumLength = 3)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Phone number is required")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public string Status { get; set; }

        [Required(ErrorMessage ="Partner is required")]
        [Display(Name="Partner")]
        public int PartnerId { get; set; }
        public Partner Partner { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        public string Password { get; set; }

        public bool? IsLocked { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(16, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 5)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        public ICollection<Permission> Permissions { get; set; }

        public bool? Deleted { get; set; }
        public DateTime? CreatedAt { get; set; }

        string IAuditEntity.CreatedBy
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public int? CreatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }

        string IAuditEntity.DeletedBy
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public int? DeletedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public User()
        {
            Permissions = new Collection<Permission>();
        }

        //Resto de la Implementacion de User
    }
}