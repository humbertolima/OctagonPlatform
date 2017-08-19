using System.Data.Entity;

namespace OctagonPlatform.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Logo> Logos { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<PartnerContact> PartnerContacts { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<SetOfPermission> SetOfPermissions { get; set; }

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
            
        }

    }
}