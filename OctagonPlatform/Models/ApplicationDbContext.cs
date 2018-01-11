using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace OctagonPlatform.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<PartnerContact> PartnerContacts { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<SetOfPermission> SetOfPermissions { get; set; }
        public DbSet<Terminal> Terminals { get; set; }
        public DbSet<TerminalContact> TerminalContacts { get; set; }
        public DbSet<LocationType> LocationTypes { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<TerminalPicture> TerminalPictures { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<LoadCash> LoadCashs { get; set; }
        public DbSet<TransactionStatistic> TransactionStatistics { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<VaultCash> VaultCashs { get; set; }
        public DbSet<InterChange> InterChanges { get; set; }
        public DbSet<Surcharge> Surcharges { get; set; }
        public DbSet<Cassette> Cassettes { get; set; }
        public DbSet<BindedKey> BindedKeys { get; set; }
        public DbSet<Dispute> Disputes { get; set; }
        public DbSet<DisputeRepresent> DisputeRepresents { get; set; }
        public DbSet<TerminalAlertConfig> TerminalAlertConfigs { get; set; }
        public DbSet<TerminalAlert> TerminalAlerts { get; set; }
        public DbSet<CryptoChargeAccount> CryptoChargeAccounts { get; set; }
        public DbSet<CryptoCurrencyTransaction> CryptoCurrencyTransactions { get; set; }
        public DbSet<ReportModel> Reports { get; set; }
        public DbSet<ReportGroupModel> ReportGroups { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ReportFilter> ReportFilters { get; set; }
        public DbSet<FilterModel> Filters { get; set; }
        public DbSet<SubscriptionModel> Subscriptions { get; set; }
        public ApplicationDbContext()
            : base("LocalConection")
        {
        }

        public static ApplicationDbContext Create()
        {

            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<Partner>().Property(c => c.ParentId).IsOptional();
            modelBuilder.Entity<Schedule>()
               .Map<ScheduleDaily>(m => m.Requires("Discriminator").HasValue("ScheduleDaily"))
               .Map<ScheduleMonthly>(m => m.Requires("Discriminator").HasValue("ScheduleMonthly"))
               .Map<ScheduleMonthlyRelative>(m => m.Requires("Discriminator").HasValue("ScheduleMonthlyRelative"))
               .Map<ScheduleOnce>(m => m.Requires("Discriminator").HasValue("ScheduleOnce"))
               .Map<ScheduleWeekly>(m => m.Requires("Discriminator").HasValue("ScheduleWeekly"));
            modelBuilder.Entity<ReportFilter>()
           .HasRequired(b => b.Subscription)
           .WithMany(b => b.ReportFilters)
           .WillCascadeOnDelete(true);
            //modelBuilder.Entity<>
        }


    }
}