using OctagonPlatform.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OctagonPlatform.Models
{
    public sealed class Partner: IAuditEntity, ISoftDeleted
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The parent partner is required")]
        [Display(Name = "Parent")]
        public int ParentId { get; set; }

        public Partner Parent { get; set; }

        public ICollection<Partner> Partners { get; set; }

        [Required(ErrorMessage = "The partner business's name is required")]
        [StringLength(50)]
        [Display(Name = "Business's name")]
        [Index(IsUnique = true)]
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

        public Country Country { get; set; }

        [Required(ErrorMessage = "The partner's state is required")]
        [Display(Name = "State")]
        public int StateId { get; set; }

        public State State { get; set; }


        [Display(Name = "City")]
        public int? CityId { get; set; }

        public City City { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(50, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 5)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        [Display(Name = "Work Phone")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Work phone number is not valid")]
        public string WorkPhone { get; set; }

        [Display(Name = "Mobile Phone")]
        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Mobile phone number is not valid")]
        public string Mobile { get; set; }

        [Display(Name = "Fax Number")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Fax number is not valid")]
        public string Fax { get; set; }

        public string WebSite { get; set; }
        
        [DataType(DataType.ImageUrl)]
        public string  Logo { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<PartnerContact> PartnerContacts { get; set; }


        public ICollection<Terminal> Terminals { get; set; }

        public ICollection<BankAccount> BankAccounts { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FreeSurchargeFeeContractConclude { get; set; }

        public bool IsFreeSurchargeFeeContractFinished { get; set; }
             
        public Partner()
        {
            Users = new Collection<User>();

            PartnerContacts = new Collection<PartnerContact>();

            Partners = new Collection<Partner>();

            Terminals = new Collection<Terminal>();

            BankAccounts = new Collection<BankAccount>();
            
        }

        
        public DateTime? CreatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }
        public string CreatedByName { get; set; }
        public string DeletedByName { get; set; }

        public bool Deleted { get; set; }

        public void FreeSurchargeFeeContractCalculation()
        {
            if (DateTime.UtcNow.CompareTo(FreeSurchargeFeeContractConclude) >= 0)
                IsFreeSurchargeFeeContractFinished = true;
        }
    }
}
