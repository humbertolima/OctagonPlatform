using OctagonPlatform.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class TerminalFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Partner")]
        public int PartnerId { get; set; }

        public Partner Partner { get; set; }

        public ICollection<Partner> Partners { get; set; }  

        [Required]
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        public Country Country { get; set; }

        public ICollection<Country> Countries { get; set; } 

        [Required]
        [Display(Name = "State")]
        public int StateId { get; set; }

        public State State { get; set; }

        public ICollection<State> States { get; set; }  

        [Display(Name = "City")]
        public int CityId { get; set; }

        public City City { get; set; }

        public ICollection<City> Cities { get; set; }   

        [Required]
        [Display(Name = "Location Type")]
        public int LocationTypeId { get; set; }

        public LocationType LocationType { get; set; }

        public ICollection<LocationType> LocationTypes { get; set; }

        [Required(ErrorMessage = "The phone is required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Phone number is not valid")]
        [StringValidation(ErrorMessage = "Phone must be only numbers, with no spaces. ")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Make")]
        public int MakeId { get; set; }

        public Make Make { get; set; }

        public ICollection<Make> Makes { get; set; }    

        [Required]
        [Display(Name = "Model")]
        public int ModelId { get; set; }

        public Model Model { get; set; }

        public ICollection<Model> Models { get; set; }  

        [Required(ErrorMessage = "The partner's status is required")]
        public StatusType.Status Status { get; set; }

        [Required]
        [Display(Name = "Communication Type")]
        public CommunicationType.Communication CommunicationType { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address1 { get; set; }

        [Display(Name = "Cont.")]
        public string Address2 { get; set; }

        [Required(ErrorMessage = "The zip code is required")]
        [Display(Name = "Zip")]
        public int Zip { get; set; }

        [Required]
        [Display(Name = "Emv")]
        public bool EmvReady { get; set; }

        [Required]
        [Display(Name = "Serial")]
        [Index(IsUnique = true)]
        [StringLength(50)]
        public string MachineSerialNumber { get; set; }

        [Display(Name = "Veep Serial")]
        public string VeppSerialNumber { get; set; }

        [Display(Name = "Software Version")]
        public string SoftwareVersion { get; set; }

        [Display(Name = "Fimware Version")]
        public string FimwareVersion { get; set; }

        [Required]
        [Display(Name = "Bank Account")]
        public int BankAccountId { get; set; }

        public BankAccount DefaultBankAccount { get; set; }

        public IEnumerable<BankAccount> BankAccounts { get; set; }

        [Required]
        [Display(Name = "Surcharge Amount")]
        public double SurchargeAmountFee { get; set; }

        [Required]
        [Display(Name = "Surcharge Percentage")]
        public double SurchargePercentageFee { get; set; }

        [Required]
        [Display(Name = "Surcharge Logic")]
        public SurchargedBy.SurchargeTypes SurchargeType { get; set; }


        [Display(Name = "Day Closed")]
        public WhoInitiateDayClsed.Who WhoInitiates { get; set; }

        [Required]
        [Display(Name = "Cryptocurrency Fee")]
        public double CryptoPercentChargeAmount { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d MMM yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Installation Date")]
        [DateTimeValidation(ErrorMessage = "Invalid Date Time")]
        public DateTime? InstalledDate { get; set; }

        [Display(Name = "Loaded By?")]
        public string LoadedBy { get; set; }

        [Display(Name = "Terminal Balance")]
        public double? Balance { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Min Cash")]
        public double? MinAmmountCash { get; set; }
    }
}