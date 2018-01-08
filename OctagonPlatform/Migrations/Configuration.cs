using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

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
                ParentId = null,
                BusinessName = "Odyssey Group",
                Status = Helpers.StatusType.Status.Active,
                Address1 = "753 Shotgum RD",
                CountryId = 1,
                StateId = 1,
                CityId = 1,
                Email = "admin@xyncro.net",
                Mobile = "7867921950",
                Deleted = false,
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
            }, new Models.User
            {
                UserName = "admin03",
                Password = "0C-CD-1F-A5-42-CC-6C-67-CB-7C-3C-B8-CC-86-51-7E",
                PartnerId = 1,
                IsLocked = false,
                Email = "admin@xyncro.net",
                Name = "Administrator3",
                LastName = "Admin3",
                Phone = "7867921520",
                Status = Helpers.StatusType.Status.Active,
                Deleted = false,
                Key = "t4RcY6PQUBjqO3R24jEYK8d7ZCNS9fuU4QooX1nDSBFJPuKTkNUdiRVv2Uoxu7SPhAw8QDgc7bgiDFsE34JxxqLo54wdO1jVV1Bp",
            }

            );

            context.LocationTypes.AddOrUpdate(l => l.Name,
                new Models.LocationType { Name = "Restaurant" },
                new Models.LocationType { Name = "Barber Shop" }
                );

            context.Makes.AddOrUpdate(m => m.Name,
                new Models.Make { Name = "Puloon" },
                new Models.Make { Name = "N. Hyosung" }
                );

            context.Models.AddOrUpdate(m => m.Name,
                new Models.Model { Name = "Mini ATM", MakeId = 1 },
                new Models.Model { Name = "Siri Atm", MakeId = 1 }
                );

            context.Filters.AddOrUpdate(l => l.Name,
                new Models.FilterModel { Name = "TerminalId" },
                new Models.FilterModel { Name = "ReportingGroup" },
                new Models.FilterModel { Name = "Status" },
                new Models.FilterModel { Name = "StartDate" },
                new Models.FilterModel { Name = "StopDate" },
                new Models.FilterModel { Name = "Account" },
                new Models.FilterModel { Name = "SurchargeOverrideAmount" },
                new Models.FilterModel { Name = "SurchargeOverride" },
                new Models.FilterModel { Name = "PartnerId" }
                );

            #region SetOfPermissions

            string terminalsName = "Terminals";
            context.SetOfPermissions.AddOrUpdate(p => p.Name,
                new Models.SetOfPermission { Name = terminalsName });

            string partnerName = "Partners";
            context.SetOfPermissions.AddOrUpdate(p => p.Name,
                new Models.SetOfPermission { Name = partnerName });

            context.SaveChanges();  //importante. para poder tomar el Id de DB y setearlo en los Id de los campos de la tabla permissions
            #endregion

            #region Terminal Permissions

            //segun documentacion si se usa el Find en vez de FirtOrDefault, no hay que darle SaveChanges().

            context.Permissions.AddOrUpdate(p => p.Name, new Models.Permission
            {
                Name = "TerminalView",
                SetOfPermissionId = context.SetOfPermissions.FirstOrDefault(f => f.Name == terminalsName).Id

            });

            context.Permissions.AddOrUpdate(p => p.Name, new Models.Permission
            {
                Name = "TerminalCreate",
                SetOfPermissionId = context.SetOfPermissions.FirstOrDefault(f => f.Name == terminalsName).Id

            });

            context.Permissions.AddOrUpdate(p => p.Name, new Models.Permission
            {
                Name = "TerminalEdit",
                SetOfPermissionId = context.SetOfPermissions.FirstOrDefault(f => f.Name == terminalsName).Id

            });

            context.Permissions.AddOrUpdate(p => p.Name, new Models.Permission
            {
                Name = "TerminalDelete",
                SetOfPermissionId = context.SetOfPermissions.FirstOrDefault(f => f.Name == terminalsName).Id

            });
            #endregion

            #region Partner Permissions


            context.Permissions.AddOrUpdate(p => p.Name, new Models.Permission
            {
                Name = "PartnerView",
                SetOfPermissionId = context.SetOfPermissions.FirstOrDefault(f => f.Name == partnerName).Id

            });

            context.Permissions.AddOrUpdate(p => p.Name, new Models.Permission
            {
                Name = "PartnerCreate",
                SetOfPermissionId = context.SetOfPermissions.FirstOrDefault(f => f.Name == partnerName).Id

            });

            context.Permissions.AddOrUpdate(p => p.Name, new Models.Permission
            {
                Name = "PartnerEdit",
                SetOfPermissionId = context.SetOfPermissions.FirstOrDefault(f => f.Name == partnerName).Id

            });

            context.Permissions.AddOrUpdate(p => p.Name, new Models.Permission
            {
                Name = "PartnerDelete",
                SetOfPermissionId = context.SetOfPermissions.FirstOrDefault(f => f.Name == partnerName).Id

            });
            #endregion

        }
    }
}
