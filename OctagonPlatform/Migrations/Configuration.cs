using System.Data.Entity.Migrations;

namespace OctagonPlatform.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.Partners.AddOrUpdate(p => p.BusinessName, new Models.Partner
            {
                ParentId = 1,
                BusinessName = "Odyssey Group",
                Status = Helpers.StatusType.Status.Active,
                Address1= "753 Shotgum RD",
                CountryId=1,
                StateId=1,
                CityId =1,
                Email = "admin@xyncro.net",
                Mobile = "7867921950",
                Deleted =false,
            });

            context.Users.AddOrUpdate(u => u.Name, new Models.User
            {
                UserName = "admin02",
                Password = "0C-CD-1F-A5-42-CC-6C-67-CB-7C-3C-B8-CC-86-51-7E",
                PartnerId = 1,
                IsLocked = false,
                Email = "admin@xyncro.net",
                Name = "Administrator",
                LastName = "Admin",
                Phone = "7867921520",
                Status = Helpers.StatusType.Status.Active,
                Deleted = false,
                Key = "t4RcY6PQUBjqO3R24jEYK8d7ZCNS9fuU4QooX1nDSBFJPuKTkNUdiRVv2Uoxu7SPhAw8QDgc7bgiDFsE34JxxqLo54wdO1jVV1Bp",
            });
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
