using OctagonPlatform.Models;
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

            Models.Partner odyssey = new Models.Partner() { ParentId = null, BusinessName = "Odyssey Group", Status = Helpers.StatusType.Status.Active, Address1 = "753 Shotgum RD", CountryId = 1, StateId = 1, CityId = 1, Email = "admin@xyncro.net", Mobile = "7867921950", Interchange = 0, Deleted = false, };

            Models.User admin02 = new Models.User() { UserName = "admin02", Password = "0C-CD-1F-A5-42-CC-6C-67-CB-7C-3C-B8-CC-86-51-7E", IsLocked = false, Email = "admin@xyncro.net", Name = "Administrator", LastName = "Admin", Phone = "7867921520", Status = Helpers.StatusType.Status.Active, Deleted = false, Key = "t4RcY6PQUBjqO3R24jEYK8d7ZCNS9fuU4QooX1nDSBFJPuKTkNUdiRVv2Uoxu7SPhAw8QDgc7bgiDFsE34JxxqLo54wdO1jVV1Bp", };
            Models.User admin03 = new Models.User() { UserName = "admin03", Password = "0C-CD-1F-A5-42-CC-6C-67-CB-7C-3C-B8-CC-86-51-7E", IsLocked = false, Email = "admin@xyncro.net", Name = "Administrator3", LastName = "Admin3", Phone = "7867921520", Status = Helpers.StatusType.Status.Active, Deleted = false, Key = "t4RcY6PQUBjqO3R24jEYK8d7ZCNS9fuU4QooX1nDSBFJPuKTkNUdiRVv2Uoxu7SPhAw8QDgc7bgiDFsE34JxxqLo54wdO1jVV1Bp", };

            odyssey.Users.Add(admin02);                                     //notese que a oddysey le agrego los usuarios. 
            odyssey.Users.Add(admin03);

            context.Partners.AddOrUpdate(m => m.BusinessName, odyssey);     //agrego a oddysey como partner.

            context.LocationTypes.AddOrUpdate(l => l.Name,
                new Models.LocationType { Name = "Restaurant" },
                new Models.LocationType { Name = "Barber Shop" }
                );

            Models.Model miniATM = new Models.Model { Name = "Mini ATM", Make = new Models.Make() };
            Models.Model SiriATM = new Models.Model { Name = "Siri ATM", Make = new Models.Make() };

            Models.Make Puloon = new Models.Make() { Name = "Puloon" };
            Models.Make Hysosung = new Models.Make() { Name = "N. Hyosung" };

            Puloon.Models.Add(miniATM);
            Hysosung.Models.Add(SiriATM);

            #region Insert Report
            context.Reports.AddOrUpdate(l => l.Name,
               new Models.ReportModel { Name = "Cash Balance at Close" },
               new Models.ReportModel { Name = "Cash Management" },
               new Models.ReportModel { Name = "Terminal List" },
                new Models.ReportModel { Name = "Daily Transaction Summary" },
                new Models.ReportModel { Name = "Monthly Transaction Summary" },
               new Models.ReportModel { Name = "Terminal Status" },
               new Models.ReportModel { Name = "Cash Load" }
               );
            #endregion

            #region despues del merge

            List<Permission> allPermissions = new List<Permission>();

            List<string> Level0 = new List<string>() { "Alerts", "Mobile TMS", "My Profile", "Partners", "Reports", "Terminals", };
            foreach (var item in Level0)
            {
                allPermissions.Add(new Permission { Name = item });
            }

            List<string> Level01 = new List<string>() { "View Alerts", "OffLine Terminals", "Inactive Terminals", "Incomplete Terminals", "Low Cash Balance", "Unsettled Funds", "Settlement Changes", "ACH Returns", "Pending Disputes", "Represent Pending Disputes", };
            foreach (var item in Level01)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Alerts") });
            }

            List<string> Level02 = new List<string>() { "MobileTMS Access", "Phone Book", "ATM Error Code Lookup", "Terminal  Locator", };
            foreach (var item in Level02)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Mobile TMS") });
            }

            List<string> Level03 = new List<string>() { "View Shortcuts", "View Switch Talk", "View Error Code Search", "View Phone Book", "Voice TMS Access", "My profile Admin", };
            foreach (var item in Level03)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "My Profile") });
            }

            List<string> Level031 = new List<string>() { "Upload Partner Logo","Save Profile", };
            foreach (var item in Level031)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "My profile Admin") });
            }

            List<string> Level04 = new List<string>() { "Partner Admin", "BIN Suspension Groups", "Buy Rate Management", "Compliance Document", "General Info Partner", "Partner Contacts", "Partner Funding Sources", "Partner Settlement Accounts", "Partner User Management", };
            foreach (var item in Level04)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Partners") });
            }

            List<string> Level032 = new List<string>() { "Add New Partner", "Delete Partner", };
            foreach (var item in Level032)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Partner Admin") });
            }

            List<string> Level033 = new List<string>() { "View BIN Groups", "Edit BIN Groups", "Add BIN Groups", "Delete BIN Groups", };
            foreach (var item in Level033)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "BIN Suspension Groups") });
            }

            List<string> Level034 = new List<string>() { "View Buy Rates", "Edit Buy Rates", "Buy Rates", };
            foreach (var item in Level034)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Buy Rate Management") });
            }

            List<string> Level035 = new List<string>() { "View Restricted Documents", "Manage Compliance Documents Categories", };
            foreach (var item in Level035)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Compliance Document") });
            }

            List<string> Level036 = new List<string>() { "View General Info Partner", "Edit General Info Partner", };
            foreach (var item in Level036)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "General Info Partner") });
            }

            List<string> Level037 = new List<string>() { "View Partner Contacts", "Edit Partner Contacts", "Add Partner Contacts", "Delete Partner Contacts", };
            foreach (var item in Level037)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Partner Contacts") });
            }

            List<string> Level038 = new List<string>() { "View Funding", "Edit Funding", "Delete Funding", };
            foreach (var item in Level038)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Partner Funding Sources") });
            }

            List<string> Level039 = new List<string>() { "View Settlement", "Edit Settlement", "Add Settlement", "Delete Settlement", };
            foreach (var item in Level039)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Partner Settlement Accounts") });
            }

            List<string> Level3110 = new List<string>() { "View Users", "Edit Users", "Add Users", "Delete Users", };
            foreach (var item in Level3110)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Partner User Management") });
            }

            List<string> Level05 = new List<string>() { "View Reports", "Reports Admin", "Terminal Reports", "Transaction Reports", "Settlement Reports", "Merchant Reports", "Alerts Reports", };
            foreach (var item in Level05)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Reports") });
            }

            List<string> Level06 = new List<string> { "View Terminal Management", "View Terminal Statistics", "Cash Management", "Terminal Admin", "Alert Settings", "Compliance Documents", "General Info Terminals", "Interchange Split", "Surcharge Split", "Terminal Contacts", "Terminal Notes", "Terminal Pictures", "Transactions", "Vault Cash Info", };
            foreach (var item in Level06)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Terminals") });
            }

            List<string> Level041 = new List<string>() { "Add Terminal", "Clone Terminal", "Delete Terminal", "Bind Master Key","Change Terminal Partner",};
            foreach (var item in Level041)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Terminal Admin") });
            }

            List<string> Level042 = new List<string>() { "Edit Terminals Alert Settings", };
            foreach (var item in Level042)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Alert Settings") });
            }

            List<string> Level043 = new List<string>() { "View Compliance", "Edit Compliance", };
            foreach (var item in Level043)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Compliance Documents") });
            }

            List<string> Level044 = new List<string>() { "View General Info Terminals", "Edit General Info","Edit Terminal Status","Manage Terminal Address","View Terminal Contract Info", "View Terminal Sponsor Info", };
            foreach (var item in Level044)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "General Info Terminals") });
            }

            List<string> Level045 = new List<string>() { "View Interchange Split", "Add Interchange Split", "Edit Interchange Split", "Delete Interchange Split", };
            foreach (var item in Level045)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Interchange Split") });
            }

            List<string> Level046 = new List<string>() { "View Surcharge Split", "Add Surcharge Split", "Edit Surcharge Split", "Delete Surcharge Split", };
            foreach (var item in Level046)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Surcharge Split") });
            }

            List<string> Level047 = new List<string>() { "View Terminal Contacts", "Edit Terminal Contacts", "Delete Terminal Contacts", };
            foreach (var item in Level047)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Terminal Contacts") });
            }

            List<string> Level048 = new List<string>() { "View Terminal Notes", "Edit Terminal Notes", };
            foreach (var item in Level048)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Terminal Notes") });
            }
            List<string> Level049 = new List<string>() { "View Terminal Pictures", "Edit Terminal Pictures", "Delete Terminal Pictures", };
            foreach (var item in Level049)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Terminal Pictures") });
            }

            List<string> Level051 = new List<string>() { "View Switch Messages", "Current Days Transaction", };
            foreach (var item in Level051)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Transactions") });
            }

            List<string> Level052 = new List<string>() { "View Vault Cash Info", "Edit Vault Cash Info", "Add Vault Cash Info", "Delete Vault Cash Info", };
            foreach (var item in Level052)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Vault Cash Info") });
            }

            #endregion

            #region Permission Level 2

            List<string> Level121 = new List<string> { "Edit Reporting Groups", "Manage Report Subscriptions", "Display Surcharge Column on Reports", };
            foreach (var item in Level121)
            {
                allPermissions.Add(new Permission { Name = item, Parent = allPermissions.Single(m => m.Name == "Reports Admin") });
            }

            List<string> Level122 = new List<string> { "Cash Balance at Close Report", "Encryption Levels Report", "Terminal Notes Report", "Terminal Configuration Sheet", "Terminal List", "Terminal List By Partner", "Terminal Settlement Setup", "Terminal Status", "Vault Cash Projection Report", };
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

            foreach (var permission in allPermissions)
            {
                context.Permissions.AddOrUpdate(m=>m.Name, permission);
            }
        }
    }
}



//new Models.Permission { Name = terminal, Parent = context.Permissions.SingleOrDefault(perm => perm.Name == root) },
//                new Models.Permission { Name = partner, Parent = context.Permissions.SingleOrDefault(perm => perm.Name == root) },
//                new Models.Permission { Name = "General Info", Parent = context.Permissions.SingleOrDefault(perm => perm.Name == terminal) },
//                new Models.Permission { Name = "Terminal Admin", Parent = context.Permissions.SingleOrDefault(perm => perm.Name == terminal) }
