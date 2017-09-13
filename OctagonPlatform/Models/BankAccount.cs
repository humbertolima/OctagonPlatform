using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string RoutingNumber { get; set; }
        public string Status { get; set; }
        public string NameOfCheck { get; set; }
        public decimal FedTax { get; set; }
        public string SSN { get; set; }
        public string LastSettled { get; set; }
        public bool? Comercial { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int Zip { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Partner is required")]
        [Display(Name = "Partner")]
        public int PartnerId { get; set; }
        public Partner Partner { get; set; }

        [Required(ErrorMessage = "Type Account is Required")]
        [Display(Name = "Type")]
        public int AccountTypeId { get; set; }
        public AccountType AccountType { get; set; }


        public bool Deleted { get; set; }
        public DateTime? CreatedAt { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }

        public int? DeletedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
         
    }
    
}