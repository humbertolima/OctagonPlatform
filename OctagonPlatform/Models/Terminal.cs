using OctagonPlatform.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OctagonPlatform.Models
{
    public class Terminal: IAuditEntity, ISoftDeleted
    {
        [Key]
        public int Id { get; set; }
   
        [Required]
        [Display(Name = "Partner")]
        public int PartnerId { get; set; }

        public Partner Partner { get; set; }

        public ICollection<TerminalContact> TerminalContacts { get; set; }

        [Required]
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        public Country Country { get; set; }

        [Required]
        [Display(Name = "State")]
        public int StateId { get; set; }

        public State State { get; set; }

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

        public ICollection<User> Users { get; set; }

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

        [DataType(DataType.DateTime)]
        public DateTime? InstalledDate { get; set; }

        public string ChargedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? LastCommunicationDate { get; set; }

        public int? Balance { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? LastTransactionDate { get; set; }

        [DataType(DataType.Currency)]
        public int? MinAmmountCash { get; set; }

        public bool Offline { get; set; }

        public string Key1 { get; set; }

        public string Key2 { get; set; }

        public string EncryptionType { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateKeyBounded { get; set; }

        [Required]
        [Display(Name = "Default bank account")]
        public int BankAccountId { get; set; }

        public BankAccount DefaultBankAccount { get; set; }

        public ICollection<TerminalPicture> TerminalPictures { get; set; }

        public ICollection<Note> Notes { get; set; }

        public ICollection<Event> Events { get; set; }

        public ICollection<Document> Documents { get; set; }

        public ICollection<LoadCash> LoadCashs { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public ICollection<Contract> Contracts { get; set; }

        public ICollection<Cassette.Denomination> Cassettes { get; set; }

        public VaultCash VaultCash { get; set; }

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

        public ICollection<Surcharge> Surcharges { get; set; }

        [Required]
        public double InterChangeAmmount { get; set; }

        public ICollection<InterChange> InterChanges { get; set; }

        public Terminal()
        {
            TerminalContacts = new Collection<TerminalContact>();
            TerminalPictures = new Collection<TerminalPicture>();
            Users = new Collection<User>();
            Notes = new Collection<Note>();
            Events = new Collection<Event>();
            Documents = new Collection<Document>();
            LoadCashs = new Collection<LoadCash>();
            Transactions = new Collection<Transaction>();
            Contracts = new Collection<Contract>();
            Cassettes = new Collection<Cassette.Denomination>();
            Surcharges = new Collection<Surcharge>();
            InterChanges = new Collection<InterChange>();
        }
    }
}