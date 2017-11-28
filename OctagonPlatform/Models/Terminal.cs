using OctagonPlatform.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models
{
    public class Terminal: IAuditEntity, ISoftDeleted
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TerminalId { get; set; }


        [Required]
        [Display(Name = "Partner")]
        public int PartnerId { get; set; }

        public Partner Partner { get; set; }

        public ICollection<TerminalMessage> TerminalMessages { get; set; }

        public ICollection<TerminalAlert> TerminalAlerts { get; set; }

        public ICollection<TerminalContact> TerminalContacts { get; set; }

        public ICollection<TerminalWorkingHours> WorkingHours { get; set; }

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

        public ICollection<TerminalPicture> TerminalPictures { get; set; }

        public ICollection<Note> Notes { get; set; }

        public ICollection<Event> Events { get; set; }

        public ICollection<Document> Documents { get; set; }

        public ICollection<LoadCash> LoadCashs { get; set; }

        public ICollection<TransactionStatistic> TransactionStatistics { get; set; }

        public int? LastTransactionId { get; set; }  

        public TransactionStatistic LastTransaction { get; set; }

        public ICollection<Contract> Contracts { get; set; }

        public ICollection<Cassette> Cassettes { get; set; }


        [Display(Name = "Who Initiates Day Closed")]
        public WhoInitiateDayClsed.Who WhoInitiates { get; set; }

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

        public ICollection<Surcharge> Surcharges { get; set; }

        public ICollection<InterChange> InterChanges { get; set; }

        [Required]
        [Display(Name = "Percent Charge By Transaction")]
        public double CryptoPercentChargeAmount { get; set; }

        public ICollection<CryptoChargeAccount> CryptoChargeAccounts { get; set; }

        public ICollection<CryptoCurrencyTransaction> CryptoCurrencyTransactions { get; set; }

        public ICollection<Dispute> Disputes { get; set; }

        public TerminalAlertConfig TerminalAlertConfigs { get; set; }


        public Terminal()
        {
            TerminalContacts = new Collection<TerminalContact>();
            TerminalPictures = new Collection<TerminalPicture>();
            Users = new Collection<User>();
            Notes = new Collection<Note>();
            Events = new Collection<Event>();
            Documents = new Collection<Document>();
            LoadCashs = new Collection<LoadCash>();
            TransactionStatistics = new Collection<TransactionStatistic>();
            Contracts = new Collection<Contract>();
            Cassettes = new Collection<Cassette>();
            Surcharges = new Collection<Surcharge>();
            InterChanges = new Collection<InterChange>();
            CryptoChargeAccounts = new Collection<CryptoChargeAccount>();
            CryptoCurrencyTransactions = new Collection<CryptoCurrencyTransaction>();
            Disputes = new Collection<Dispute>();
            TerminalAlerts = new Collection<TerminalAlert>();
            TerminalMessages = new Collection<TerminalMessage>();
            WorkingHours = new Collection<TerminalWorkingHours>();
        }
    }
}