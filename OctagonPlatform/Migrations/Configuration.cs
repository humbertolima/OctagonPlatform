using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using OctagonPlatform.Models;

namespace OctagonPlatform.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;

        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            Partner odyssey = new Partner() { ParentId = null, BusinessName = "Odyssey Group", Status = Helpers.StatusType.Status.Active, Address1 = "753 Shotgum RD", CountryId = 1, StateId = 1, CityId = 1, Email = "admin@xyncro.net", Mobile = "7867921950", Interchange = 0, Deleted = false, };

            List<User> allUsers = new List<User>
            {
              new User() { UserName = "admin02", Password = "0C-CD-1F-A5-42-CC-6C-67-CB-7C-3C-B8-CC-86-51-7E", IsLocked = false, Email = "admin@xyncro.net", Name = "Administrator", LastName = "Admin", Phone = "7867921520", Status = Helpers.StatusType.Status.Active, Deleted = false, Key = "t4RcY6PQUBjqO3R24jEYK8d7ZCNS9fuU4QooX1nDSBFJPuKTkNUdiRVv2Uoxu7SPhAw8QDgc7bgiDFsE34JxxqLo54wdO1jVV1Bp", },
              new User() { UserName = "admin03", Password = "0C-CD-1F-A5-42-CC-6C-67-CB-7C-3C-B8-CC-86-51-7E", IsLocked = false, Email = "admin@xyncro.net", Name = "Administrator3", LastName = "Admin3", Phone = "7867921520", Status = Helpers.StatusType.Status.Active, Deleted = false, Key = "t4RcY6PQUBjqO3R24jEYK8d7ZCNS9fuU4QooX1nDSBFJPuKTkNUdiRVv2Uoxu7SPhAw8QDgc7bgiDFsE34JxxqLo54wdO1jVV1Bp", }
            };

            foreach (var user in allUsers)
            {
                odyssey.Users.Add(user);
            }

            context.Partners.AddOrUpdate(m => m.BusinessName, odyssey);     //agrego a oddysey como partner.

            context.LocationTypes.AddOrUpdate(l => l.Name,
                new LocationType { Name = "Restaurant" },
                new LocationType { Name = "Barber Shop" }
                );

            Model miniATM = new Model { Name = "Mini ATM" };
            Model SiriATM = new Model { Name = "Siri ATM" };

            Make Puloon = new Make() { Name = "Puloon" };
            Make Hysosung = new Make() { Name = "N. Hyosung" };

            Puloon.Models.Add(miniATM);
            Hysosung.Models.Add(SiriATM);

            context.Makes.AddOrUpdate(m => m.Name, Puloon);
            context.Makes.AddOrUpdate(m => m.Name, Hysosung);

            #region despues del merge
            List<Permission> allPermissions = new List<Permission>();
            //Level0 es el nivel raiz Alert
            Dictionary<int, string> Level0 = new Dictionary<int, string>() { { 1, "Alerts" }, { 2, "Mobile TMS" }, { 3, "My Profile" }, { 4, "Partners" }, { 5, "Reports" }, { 6, "Terminals" }, };

            foreach (var item in Level0)
            {
                allPermissions.Add(new Permission { Name = item.Value });
            }

            
            Dictionary<int, string> Level01 = new Dictionary<int, string>() { { 7, "View Alerts" }, { 8, "OffLine Terminals" }, { 9, "Inactive Terminals" }, { 10, "Incomplete Terminals" }, { 11, "Low Cash Balance" }, { 12, "Unsettled Changes" }, { 13, "Settlement Changes" }, { 14, "ACH Returns" }, { 15, "Pending Disputes" }, { 16, "Represent Pending Disputes" }, };
            foreach (var item in Level01)
            {
                allPermissions.Add(new Permission { Name = item.Value, Parent = allPermissions.Single(m => m.Name == "Alerts") }); //el parent es el de ALERT level0, la raiz.
            }

            Dictionary<int, string> Level02 = new Dictionary<int, string>() { { 17, "MobileTMS Access" }, { 18, "Phone Book" }, { 19, "ATM Error Code Lookup" }, { 20, "Terminal  Locator" }, };
            foreach (var item in Level02)
            {
                allPermissions.Add(new Permission { Name = item.Value, Parent = allPermissions.Single(m => m.Name == "Mobile TMS") });
            }

            List<string> Level03 = new List<string>() { "View Shortcuts", "View Switch Talk", "View Error Code Search", "View Phone Book", "Voice TMS Access", "My profile Admin", };
            foreach (var item in Level03)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "My Profile") });
            }

            List<string> Level04 = new List<string>() { "Partner Admin", "BIN Suspension Groups", "Buy Rate Management", "Compilance Document", "General Info Partner", "Partner Contacts", "Partner Funding Sources", "Partner Settlement Accounts", "Partner User Management", };
            foreach (var item in Level04)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Partners") });
            }

            List<string> Level05 = new List<string>() { "View Reports", "Reports Admin", "Terminal Reports", "Transaction Reports", "Settlement Reports", "Merchant Reports", "Alerts Reports", };
            foreach (var item in Level05)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Reports") });
            }

            List<string> Level06 = new List<string> { "View Terminal Management", "View Terminal Statistics", "Cash Management", "Terminal Admin", "Alert Settings", "Compilance Documents", "General Info", "Interchange Split", "Surcharge Split", "Terminal Contacts", "Terminal Notes", "Terminal Pictures", "Transactions", "Vault Cash Info", };
            foreach (var item in Level06)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Terminals") });
            }


            #endregion

            #region Permission Level 2

            List<string> Level11 = new List<string> { "Upload Partner Logo", "Save Profile", };
            foreach (var item in Level11)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "My profile Admin") });
            }

            List<string> Level12 = new List<string> { "Add New Partner", "Delete Partner", };
            foreach (var item in Level12)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Partner Admin") });
            }

            List<string> Level13 = new List<string> { "View BIN Group", "Edit BIN Group", "Add BIN Group", "Delete BIN Group", };
            foreach (var item in Level13)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "BIN Suspension Groups") });
            }

            List<string> Level14 = new List<string> { "View Buy Rates", "Edit Buy Rates", "Add Buy Rates", };
            foreach (var item in Level14)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Buy Rate Management") });
            }

            List<string> Level15 = new List<string> { "View Restricted Documents", "Manage Compliance Document Categories", };
            foreach (var item in Level15)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Compilance Document") });
            }

            List<string> Level16 = new List<string> { "View General Info", "Edit General Info", };
            foreach (var item in Level16)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "General Info Partner") });
            }

            List<string> Level17 = new List<string> { "View Contacts", "Edit Contacts", "Add Contacts", "Delete Contacts", };
            foreach (var item in Level17)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Partner Contacts") });
            }

            List<string> Level18 = new List<string> { "View Funding Source", "Edit Funding Source", "Delete Funding Source", };
            foreach (var item in Level18)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Partner Funding Sources") });
            }

            List<string> Level19 = new List<string> { "View Settlement Account", "Add Settlement Account", "Edit Settlement Account", "Delete Settlement Account", };
            foreach (var item in Level19)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Partner Settlement Accounts") });
            }

            List<string> Level120 = new List<string> { "View User", "Edit User", "Add New User", "Delete User", };
            foreach (var item in Level120)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Partner User Management") });
            }

            List<string> Level121 = new List<string> { "Edit Reporting Groups", "Manage Report Subscriptions", "Display Surcharge Column on Reports", };
            foreach (var item in Level121)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Reports Admin") });
            }

            List<string> Level122 = new List<string> { "Cash Balance at Close Report", "Encryption Levels Report", "Terminal Notes Reports", "Terminal Configuration Sheet", "Terminal List", "Terminal List By Partner", "Terminal Settlement Setup", "Terminal Status", "Vault Cash Projection Report", };
            foreach (var item in Level122)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Terminal Reports") });
            }

            List<string> Level123 = new List<string> { "Daily Transaction Summary", "Transaction Analysis", "Transaction Averages Report", "Transaction Detail", "Transaction Summary", "Transactions By Network Summary", "Transactions by Network Summary Filtered", "Transactions Per Hour Graph", "Monthly Transaction Summary", "Transaction Monthly Summary", };
            foreach (var item in Level123)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Transaction Reports") });
            }

            List<string> Level124 = new List<string> { "ACH Detail", "Cash Projection Summary Report", "Consolidated Settlement Report", "Disbursement Setup History", "eInvoice Authorized Accounts", "Funds Reconciliation Report", "Settlement Split List", "Settlement Split", "Monthly EBT", };
            foreach (var item in Level124)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Settlement Reports") });
            }

            List<string> Level125 = new List<string> { "Monthly Statement", "Monthly Statement by Bank Account", "Monthly Statement with Interchange", "Monthly Statement By Terminal Profile", "Profile Transaction Summary", "User Report", "User Permissions Report", };
            foreach (var item in Level125)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Merchant Reports") });
            }

            List<string> Level126 = new List<string> { "Dispute Email Reprints", "Dispute Status Report", "Dispute History", };
            foreach (var item in Level126)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Alerts Reports") });
            }
            #endregion
            //if (!System.Diagnostics.Debugger.IsAttached)
            //    System.Diagnostics.Debugger.Launch();
            foreach (var permission in allPermissions)
            {
                context.Permissions.AddOrUpdate(m => m.Name, permission);
            }
        }
    }
}



//new Permission { Name = terminal, Parent = context.Permissions.SingleOrDefault(perm => perm.Name == root) },
//                new Permission { Name = partner, Parent = context.Permissions.SingleOrDefault(perm => perm.Name == root) },
//                new Permission { Name = "General Info", Parent = context.Permissions.SingleOrDefault(perm => perm.Name == terminal) },
//                new Permission { Name = "Terminal Admin", Parent = context.Permissions.SingleOrDefault(perm => perm.Name == terminal) }
