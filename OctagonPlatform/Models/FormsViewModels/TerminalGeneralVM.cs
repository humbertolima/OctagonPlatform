using OctagonPlatform.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class TerminalGeneralVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Terminal Id")]
        public string TerminalId { get; set; }


        [Required]
        [Display(Name = "Partner")]
        public int PartnerId { get; set; }

        public Partner Partner { get; set; }

        [Required(ErrorMessage = "The phone is required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Phone number is not valid")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        public Country Country { get; set; }

        [Required]
        [Display(Name = "State")]
        public int StateId { get; set; }

        public Models.State State { get; set; }

        [Display(Name = "City")]
        public int CityId { get; set; }

        public City City { get; set; }

        [Required]
        [Display(Name = "Location Type")]
        public int LocationTypeId { get; set; }

        public LocationType LocationType { get; set; }

        [Required]
        [Display(Name = "Make")]
        public int MakeId { get; set; }

        public Make Make { get; set; }

        [Required]
        [Display(Name = "Model")]
        public int ModelId { get; set; }

        public Model Model { get; set; }

        [Required(ErrorMessage = "The partner's status is required")]
        public StatusType.Status Status { get; set; }

        [Required]
        public CommunicationType.Communication CommunicationType { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required(ErrorMessage = "The zip code is required")]
        [Display(Name = "Zip Code")]
        public int Zip { get; set; }
        
        [Required]
        [Display(Name = "Machine Serial Number")]
        [StringLength(50)]
        public string MachineSerialNumber { get; set; }

        [Display(Name = "Veep Serial Number")]
        public string VeppSerialNumber { get; set; }

        [Display(Name = "Sorftware Version")]
        public string SoftwareVersion { get; set; }

        [Display(Name = "Fimware Version")]
        public string FimwareVersion { get; set; }

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

        [Display(Name = "Terminal Balance")]
        public double? Balance { get; set; }

        public BindedKey BindedKey { get; set; }
        
        public VaultCash VaultCash { get; set; }

        [Required]
        [Display(Name = "Surcharge Fee")]
        [DataType(DataType.Currency)]
        public double SurchargeAmountFee { get; set; }

        [Required]
        [Display(Name = "Percent Surcharge Fee")]
        public double SurchargePercentageFee { get; set; }

        [Required]
        [Display(Name = "Surcharged Type")]
        public SurchargedBy.SurchargeTypes SurchargeType { get; set; }

        [Required]
        [Display(Name = "Percent Charge By Transaction")]
        public double CryptoPercentChargeAmount { get; set; }


        [Required]
        public string LocationName { get; set; }

        public int? ReportGroupId { get; set; }

        public DateTime? DateCreated { get; set; }
    }
}