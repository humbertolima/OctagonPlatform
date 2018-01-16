using System.Collections.Generic;
using System.Data.Entity;
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
                Id = 1,
                ParentId = null,
                BusinessName = "Odyssey Group",
                Status = Helpers.StatusType.Status.Active,
                Address1 = "753 Shotgum RD",
                CountryId = 1,
                StateId = 1,
                CityId = 1,
                Email = "admin@xyncro.net",
                Mobile = "7867921950",
                Interchange = 0,
                Deleted = false,
            });

            context.Users.AddOrUpdate(u => u.Name, new Models.User
            {
                Id=1,
                UserName = "admin02",
                Password = "0C-CD-1F-A5-42-CC-6C-67-CB-7C-3C-B8-CC-86-51-7E",
                PartnerId = 3,
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
                Id= 2,
                UserName = "admin03",
                Password = "0C-CD-1F-A5-42-CC-6C-67-CB-7C-3C-B8-CC-86-51-7E",
                PartnerId = 3,
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
                new Models.Make {Name = "Puloon" },
                new Models.Make { Name = "N. Hyosung" }
                );

            context.Models.AddOrUpdate(m => m.Name,
                new Models.Model { Name = "Mini ATM", MakeId = 9 },
                new Models.Model { Name = "Siri Atm", MakeId = 10 }
                );




            List<string> Level01 = new List<string>() { "View Alerts", "OffLine Terminals", "Inactive Terminals", "Incomplete Terminals", "Low Cash Balance", "Unsettled Changes", "Settlement Changes", "ACH Returns", "Pending Disputes", "Represent Pending Disputes", };
            foreach (var item in Level01)
            {
                context.Permissions.AddOrUpdate(m => m.Name, new Models.Permission
                {
                    Name = item,
                    ParentID = context.Permissions.SingleOrDefault(perm => perm.Name == "Alerts").Id
                });
            }
            context.SaveChanges();

            List<string> Level02 = new List<string>() { "MobileTMS Access", "Phone Book", "ATM Error Code Lookup", "Terminal  Locator", };
            foreach (var item in Level02)
            {
                context.Permissions.AddOrUpdate(m => m.Name, new Models.Permission
                {
                    Name = item,
                    ParentID = context.Permissions.SingleOrDefault(perm => perm.Name == "Mobile TMS").Id
                });
            }
            context.SaveChanges();

            List<string> Level03 = new List<string>() { "View Shortcuts", "View Switch Talk", "View Error Code Search", "View Phone Book", "Voice TMS Access", "My profile Admin", };
            foreach (var item in Level03)
            {
                context.Permissions.AddOrUpdate(m => m.Name, new Models.Permission
                {
                    Name = item,
                    ParentID = context.Permissions.SingleOrDefault(perm => perm.Name == "My Profile").Id
                });
            }
            context.SaveChanges();

            List<string> Level04 = new List<string>() { "Partner Admin", "BIN Suspension Groups", "Buy Rate Management", "Compilance Document", "General Info", "Partner Contacts", "Partner Funding Sources", "Partner Settlement Accounts", "Partner User Management", };
            foreach (var item in Level04)
            {
                context.Permissions.AddOrUpdate(m => m.Name, new Models.Permission
                {
                    Name = item,
                    ParentID = context.Permissions.SingleOrDefault(perm => perm.Name == "Partners").Id
                });
            }
            context.SaveChanges();

            List<string> Level05 = new List<string>() { "View Reports", "Reports Admin", "Terminal Reports", "Transaction Reports", "Settlement Reports", "Merchant Reports", "Alerts Reports", };
            foreach (var item in Level05)
            {
                context.Permissions.AddOrUpdate(m => m.Name, new Models.Permission
                {
                    Name = item,
                    ParentID = context.Permissions.SingleOrDefault(perm => perm.Name == "Reports").Id
                });
            }
            context.SaveChanges();

            List<string> Level06 = new List<string> { "View Terminal Management", "View Terminal Statistics", "Cash Management", "Terminal Admin", "Alert Settings", "Compilance Documents", "General Info", "Interchange Split", "Surcharge Split", "Terminal Contacts", "Terminal Notes", "Terminal Pictures", "Transactions", "Vault Cash Info", };
            foreach (var item in Level06)
            {
                context.Permissions.AddOrUpdate(m => m.Name, new Models.Permission
                {
                    Name = item,
                    ParentID = context.Permissions.SingleOrDefault(perm => perm.Name == "Terminals").Id
                });
            }
            context.SaveChanges();
             #endregion

            #region Permission Level 2

            List<string> Level11 = new List<string> { "Upload Partner Logo", "Save Profile", };
            foreach (var item in Level11)
            {
                context.Permissions.AddOrUpdate(m => m.Name, new Models.Permission
                {
                    Name = item,
                    ParentID = context.Permissions.SingleOrDefault(perm => perm.Name == "My profile Admin").Id
                });
            }

            List<string> Level12 = new List<string> { "Add New Partner", "Delete Partner", };
            foreach (var item in Level12)
            {
                context.Permissions.AddOrUpdate(m => m.Name, new Models.Permission
                {
                    Name = item,
                    ParentID = context.Permissions.SingleOrDefault(perm => perm.Name == "Partner Admin").Id
                });
            }

            List<string> Level13 = new List<string> { "View BIN Group", "Edit BIN Group", "Add BIN Group", "Delete BIN Group", };
            foreach (var item in Level13)
            {
                context.Permissions.AddOrUpdate(m => m.Name, new Models.Permission
                {
                    Name = item,
                    ParentID = context.Permissions.SingleOrDefault(perm => perm.Name == "BIN Suspension Groups").Id
                });
            }

            List<string> Level14 = new List<string> { "View Buy Rates", "Edit Buy Rates", "Add Buy Rates", };
            foreach (var item in Level14)
            {
                context.Permissions.AddOrUpdate(m => m.Name, new Models.Permission
                {
                    Name = item,
                    ParentID = context.Permissions.SingleOrDefault(perm => perm.Name == "Buy Rate Management").Id
                });
            }

            List<string> Level15 = new List<string> { "View Restricted Documents", "Manage Compliance Document Categories", };
            foreach (var item in Level15)
            {
                context.Permissions.AddOrUpdate(m => m.Name, new Models.Permission
                {
                    Name = item,
                    ParentID = context.Permissions.SingleOrDefault(perm => perm.Name == "Compilance Document").Id
                });
            }

            List<string> Level16 = new List<string> { "View General Info", "Edit General Info", };
            foreach (var item in Level16)
            {
                context.Permissions.AddOrUpdate(m => m.Name, new Models.Permission
                {
                    Name = item,
                    ParentID = context.Permissions.SingleOrDefault(perm => perm.Name == "General Info").Id
                });
            }

            List<string> Level17 = new List<string> { "View Contacts", "Edit Contacts", "Add Contacts", "Delete Contacts", };
            foreach (var item in Level17)
            {
                context.Permissions.AddOrUpdate(m => m.Name, new Models.Permission
                {
                    Name = item,
                    ParentID = context.Permissions.SingleOrDefault(perm => perm.Name == "Partner Contacts").Id
                });
            }

            List<string> Level18 = new List<string> { "View Funding Source", "Edit Funding Source", "Delete Funding Source", };
            foreach (var item in Level18)
            {
                context.Permissions.AddOrUpdate(m => m.Name, new Models.Permission
                {
                    Name = item,
                    ParentID = context.Permissions.SingleOrDefault(perm => perm.Name == "Partner Funding Sources").Id
                });
            }

            List<string> Level19 = new List<string> { "View Settlement Account", "Edit Settlement Account", "Add Settlement Account", "Edit Settlement Account", "Delete Settlement Account", };
            foreach (var item in Level19)
            {
                context.Permissions.AddOrUpdate(m => m.Name, new Models.Permission
                {
                    Name = item,
                    ParentID = context.Permissions.SingleOrDefault(perm => perm.Name == "Partner Settlement Accounts").Id
                });
            }

            List<string> Level120 = new List<string> { "View User", "Edit User", "Add New User", "Delete User", };
            foreach (var item in Level120)
            {
                context.Permissions.AddOrUpdate(m => m.Name, new Models.Permission
                {
                    Name = item,
                    ParentID = context.Permissions.SingleOrDefault(perm => perm.Name == "Partner User Management").Id
                });
            }

            List<string> Level121 = new List<string> { "Edit Reporting Groups", "Manage Report Subscriptions", "Display Surcharge Column on Reports", };
            foreach (var item in Level121)
            {
                context.Permissions.AddOrUpdate(m => m.Name, new Models.Permission
                {
                    Name = item,
                    ParentID = context.Permissions.SingleOrDefault(perm => perm.Name == "Reports Admin").Id
                });
            }

            List<string> Level122 = new List<string> { "Cash Balance at Close Report", "Encryption Levels Report", "Terminal Notes", "Terminal Configuration Sheet", "Terminal List", "Terminal List By Partner", "Terminal Settlement Setup", "Terminal Status", "Vault Cash Projection Report", };
            foreach (var item in Level122)
            {
                context.Permissions.AddOrUpdate(m => m.Name, new Models.Permission
                {
                    Name = item,
                    ParentID = context.Permissions.SingleOrDefault(perm => perm.Name == "Terminal Reports").Id
                });
            }

            List<string> Level123 = new List<string> { "Daily Transaction Summary", "Transaction Analysis", "Transaction Averages Report", "Transaction Detail", "Transaction Summary", "Transactions By Network Summary", "Transactions by Network Summary Filtered", "Transactions Per Hour Graph", "Monthly Transaction Summary", "Transaction Monthly Summary", };
            foreach (var item in Level123)
            {
                context.Permissions.AddOrUpdate(m => m.Name, new Models.Permission
                {
                    Name = item,
                    ParentID = context.Permissions.SingleOrDefault(perm => perm.Name == "Transaction Reports").Id
                });
            }

            List<string> Level124 = new List<string> { "ACH Detail", "Cash Projection Summary Report", "Consolidated Settlement Report", "Disbursement Setup History", "eInvoice Authorized Accounts", "Funds Reconciliation Report", "Settlement Split List", "Settlement Split", "Monthly EBT", };
            foreach (var item in Level124)
            {
                context.Permissions.AddOrUpdate(m => m.Name, new Models.Permission
                {
                    Name = item,
                    ParentID = context.Permissions.SingleOrDefault(perm => perm.Name == "Settlement Reports").Id
                });
            }

            List<string> Level125 = new List<string> { "Monthly Statement", "Monthly Statement by Bank Account", "Monthly Statement with Interchange", "Monthly Statement By Terminal Profile", "Profile Transaction Summary", "User Report", "User Permissions Report", };
            foreach (var item in Level125)
            {
                context.Permissions.AddOrUpdate(m => m.Name, new Models.Permission
                {
                    Name = item,
                    ParentID = context.Permissions.SingleOrDefault(perm => perm.Name == "Merchant Reports").Id
                });
            }

            List<string> Level126 = new List<string> { "Dispute Email Reprints", "Dispute Status Report", "Dispute History", };
            foreach (var item in Level126)
            {
                context.Permissions.AddOrUpdate(m => m.Name, new Models.Permission
                {
                    Name = item,
                    ParentID = context.Permissions.SingleOrDefault(perm => perm.Name == "Alerts Reports").Id
                });
            }
            #endregion
        }
    }
}



//new Models.Permission { Name = terminal, Parent = context.Permissions.SingleOrDefault(perm => perm.Name == root) },
//                new Models.Permission { Name = partner, Parent = context.Permissions.SingleOrDefault(perm => perm.Name == root) },
//                new Models.Permission { Name = "General Info", Parent = context.Permissions.SingleOrDefault(perm => perm.Name == terminal) },
//                new Models.Permission { Name = "Terminal Admin", Parent = context.Permissions.SingleOrDefault(perm => perm.Name == terminal) }
