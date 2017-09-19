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

        [Display(Name = "Sorftware version")]
        public string SoftwareVersion { get; set; }

        [Display(Name = "Fimware version")]
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
        [Display(Name = "Date of installation")]
        public DateTime? InstalledDate { get; set; }

        [Display(Name = "The terminal was charged by?")]
        public string ChargedBy { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "The last terminal communication date")]
        public DateTime? LastCommunicationDate { get; set; }

        [Display(Name = "Terminal Balance")]
        public int? Balance { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Last terminal transaction Date")]
        public DateTime? LastTransactionDate { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Minimun ammount chash that must have the terminal")]
        public int? MinAmmountCash { get; set; }

        public bool Offline { get; set; }

        public string Key1 { get; set; }

        public string Key2 { get; set; }

        public string EncryptionType { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Keys bounded date")]
        public DateTime? DateKeyBounded { get; set; }

        [Display(Name = "Default bank account")]
        public int? BankAccountId { get; set; }

        public BankAccount DefaultBankAccount { get; set; }

        public ICollection<TerminalPicture> TerminalPictures { get; set; }

        public ICollection<Note> Notes { get; set; }

        public ICollection<Event> Events { get; set; }

        public ICollection<Document> Documents { get; set; }

        public ICollection<LoadCash> LoadCashs { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public ICollection<Contract> Contracts { get; set; }

        public ICollection<Cassette> Cassettes { get; set; }

        [Display(Name = "Who Initiates day closed")]
        public Initiate.Who WhoInitiates { get; set; }

        public VaultCash VaultCash { get; set; }

        [Required]
        public Helpers.Surcharge.SurchargeType SurchargeType { get; set; }

        [Required]
        [Display(Name = "Surcharge ammount by transaction")]
        public double SurchargeAmmount { get; set; }

        [Required]
        [Display(Name = "Percent by Surcharge Ammount")]
        public double SurchargeByPercent { get; set; }

        [Required]
        [Display(Name = "Take the greater or lesser between Surcharge Ammout or Surcharge by Percent")]
        public GreaterOrLessAmmount.GreaterOrLesser GreaterOrLesser { get; set; }

        [Required]
        [Display(Name = "Recolect type")]
        public Settled.SettledType SettledType { get; set; }

        public ICollection<Surcharge> Surcharges { get; set; }

        [Required]
        [Display(Name = "Inter changes ammount by sponsor bank")]
        public double InterChangeAmmount { get; set; }

        public ICollection<InterChange> InterChanges { get; set; }

        [Required]
        [Display(Name = "Chrage by Cryptocurrency Trnasactions")]
        public int CryptoChargeAmmount { get; set; }

        public ICollection<CryptoChargeAccount> CryptoChargeAccounts { get; set; }

        public ICollection<CryptoCurrencyTransaction> CryptoCurrencyTransactions { get; set; }

      

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
            Cassettes = new Collection<Cassette>();
            Surcharges = new Collection<Surcharge>();
            InterChanges = new Collection<InterChange>();
            CryptoChargeAccounts = new Collection<CryptoChargeAccount>();
            CryptoCurrencyTransactions = new Collection<CryptoCurrencyTransaction>();
        }
    }
}