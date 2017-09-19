using OctagonPlatform.Helpers;
using System.Collections.Generic;
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

        public string Address2 { get; set; }

        [Required(ErrorMessage = "The zip code is required")]
        [Display(Name = "Zip Code")]
        public int Zip { get; set; }

        [Required]
        [Display(Name = "Emv is Ready")]
        public bool EmvReady { get; set; }

        [Required]
        [Display(Name = "Machine serial number")]
        [Index(IsUnique = true)]
        [StringLength(50)]
        public string MachineSerialNumber { get; set; }

        [Display(Name = "Veep Serial number")]
        public string VeppSerailNumber { get; set; }

        [Display(Name = "Software Version")]
        public string SoftwareVersion { get; set; }

        [Display(Name = "Fimware Version")]
        public string FimwareVersion { get; set; }


        [Required]
        [Display(Name = "Surcharged type, chraged by total ammount or percent")]
        public Helpers.Surcharge.SurchargeType SurchargeType { get; set; }

        [Required]
        [Display(Name = "Surcharge ammount per transaction")]
        public double SurchargeAmmount { get; set; }

        [Required]
        [Display(Name = "Percent by Surcharge Ammount")]
        public double SurchargeByPercent { get; set; }

        [Required]
        [Display(Name = "Surcharge by ammount or percent")]
        public GreaterOrLessAmmount.GreaterOrLesser GreaterOrLesser { get; set; }

        [Required]
        [Display(Name = "Settled the recolection by?")]
        public Settled.SettledType SettledType { get; set; }

        [Required]
        [Display(Name = "Interchange ammount, aproved by sponsor bank")]
        public double InterChangeAmmount { get; set; }

        [Display(Name = "Who Initiates day closed")]
        public Initiate.Who WhoInitiates { get; set; }

        [Required]
        [Display(Name = "Chrage by Cryptocurrency Trnasactions")]
        public int CryptoChargeAmmount { get; set; }

    }
}