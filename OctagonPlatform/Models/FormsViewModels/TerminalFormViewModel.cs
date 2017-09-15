using OctagonPlatform.Helpers;
using System;
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

        public string SoftwareVersion { get; set; }

        public string FimwareVersion { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateKeyBounded { get; set; }

        [Required]
        public Helpers.Surcharge.SurchargeType SurchargeType { get; set; }

        [Required]
        public double SurchargeAmmount { get; set; }

        [Required]
        public double Percent { get; set; }

        [Required]
        public GreaterOrLessAmmount.GreaterOrLesser GreaterOrLesser { get; set; }

        [Required]
        public Settled.SettledType SettledType { get; set; }

        [Required]
        public double InterChangeAmmount { get; set; }

        [Display(Name = "Who Initiates day closed")]
        public Initiate.Who WhoInitiates { get; set; }

        public ICollection<Cassette> Cassettes { get; set; }

    }
}