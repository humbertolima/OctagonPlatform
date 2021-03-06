using OctagonPlatform.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace OctagonPlatform.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Models.ApplicationDbContext>
    {
        private List<Permission> allPermissions;
        private User admin02;
        private User admin03;

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            allPermissions = new List<Permission>();
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.


            Models.Partner odyssey = new Partner() { ParentId = null, BusinessName = "Odyssey Group", Status = Helpers.StatusType.Status.Active, Address1 = "753 Shotgum RD", CountryId = 1, StateId = 1, CityId = 1, Email = "admin@xyncro.net", Mobile = "7867921950", Interchange = 0, Deleted = false, };

            admin02 = new User() { UserName = "admin02", Password = "0C-CD-1F-A5-42-CC-6C-67-CB-7C-3C-B8-CC-86-51-7E", IsLocked = false, Email = "admin@xyncro.net", Name = "Administrator", LastName = "Admin", Phone = "7867921520", Status = Helpers.StatusType.Status.Active, Deleted = false, Key = "t4RcY6PQUBjqO3R24jEYK8d7ZCNS9fuU4QooX1nDSBFJPuKTkNUdiRVv2Uoxu7SPhAw8QDgc7bgiDFsE34JxxqLo54wdO1jVV1Bp", };
            admin03 = new User() { UserName = "admin03", Password = "0C-CD-1F-A5-42-CC-6C-67-CB-7C-3C-B8-CC-86-51-7E", IsLocked = false, Email = "admin@xyncro.net", Name = "Administrator3", LastName = "Admin3", Phone = "7867921520", Status = Helpers.StatusType.Status.Active, Deleted = false, Key = "t4RcY6PQUBjqO3R24jEYK8d7ZCNS9fuU4QooX1nDSBFJPuKTkNUdiRVv2Uoxu7SPhAw8QDgc7bgiDFsE34JxxqLo54wdO1jVV1Bp", };


            context.Partners.AddOrUpdate(m => m.BusinessName, odyssey);     //agrego a oddysey como partner.

            admin02.PartnerId = odyssey.Id;
            admin03.PartnerId = odyssey.Id;

            context.Users.AddOrUpdate(m => m.UserName, admin02);
            context.SaveChanges();



            context.LocationTypes.AddOrUpdate(l => l.Name,
                new LocationType { Name = "Restaurant" },
                new LocationType { Name = "Barber Shop" }
                );

            Model miniATM = new Model { Name = "Mini ATM", Make = new Make() };
            Model SiriATM = new Model { Name = "Siri ATM", Make = new Make() };

            Make Puloon = new Make() { Name = "Puloon" };
            Make Hysosung = new Make() { Name = "N. Hyosung" };

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

            if (!System.Diagnostics.Debugger.IsAttached)
                System.Diagnostics.Debugger.Launch();

            List<string> Level0 = new List<string> { "prueba6", "prueba5", "prueba4", "prueba2", "Prueba", "Alerts", "Mobile TMS", "My Profile", "Partners", "Reports", "Terminals", };
            foreach (var item in Level0)
            {
                Permission perm = new Permission() { Name = item };
                SavePermissions(context, perm);
            }

            List<string> Level01 = new List<string>() { "View Alerts", "OffLine Terminals", "Inactive Terminals", "Incomplete Terminals", "Low Cash Balance", "Unsettled Funds", "Settlement Changes", "ACH Returns", "Pending Disputes", "Represent Pending Disputes", };
            foreach (var item in Level01)
            {

                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Alerts").Id };
                SavePermissions(context, perm);
            }


            List<string> Level02 = new List<string>() { "MobileTMS Access", "Phone Book", "ATM Error Code Lookup", "Terminal  Locator", };
            foreach (var item in Level02)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Mobile TMS").Id };
                SavePermissions(context, perm);
            }


            List<string> Level03 = new List<string>() { "View Shortcuts", "View Switch Talk", "View Error Code Search", "View Phone Book", "Voice TMS Access", "My profile Admin", };
            foreach (var item in Level03)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "My Profile").Id };
                SavePermissions(context, perm);
            }

            List<string> Level031 = new List<string>() { "Upload Partner Logo", "Save Profile", };
            foreach (var item in Level031)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "My profile Admin").Id };
                SavePermissions(context, perm);
            }

            List<string> Level04 = new List<string>() { "Partner Admin", "BIN Suspension Groups", "Buy Rate Management", "Compliance Document", "General Info Partner", "Partner Contacts", "Partner Funding Sources", "Partner Settlement Accounts", "Partner User Management", };
            foreach (var item in Level04)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Partners").Id };
                SavePermissions(context, perm);
            }

            List<string> Level032 = new List<string>() { "Add New Partner", "Delete Partner", };
            foreach (var item in Level032)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Partner Admin").Id };
                SavePermissions(context, perm);
            }

            List<string> Level033 = new List<string>() { "View BIN Groups", "Edit BIN Groups", "Add BIN Groups", "Delete BIN Groups", };
            foreach (var item in Level033)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "BIN Suspension Groups").Id };
                SavePermissions(context, perm);
            }

            List<string> Level034 = new List<string>() { "View Buy Rates", "Edit Buy Rates", "Buy Rates", };
            foreach (var item in Level034)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Buy Rate Management").Id };
                SavePermissions(context, perm);
            }

            List<string> Level035 = new List<string>() { "View Restricted Documents", "Manage Compliance Documents Categories", };
            foreach (var item in Level035)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Compliance Document").Id };
                SavePermissions(context, perm);
            }

            List<string> Level036 = new List<string>() { "View General Info Partner", "Edit General Info Partner", };
            foreach (var item in Level036)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "General Info Partner").Id };
                SavePermissions(context, perm);
            }

            List<string> Level037 = new List<string>() { "View Partner Contacts", "Edit Partner Contacts", "Add Partner Contacts", "Delete Partner Contacts", };
            foreach (var item in Level037)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Partner Contacts").Id };
                SavePermissions(context, perm);
            }

            List<string> Level038 = new List<string>() { "View Funding", "Edit Funding", "Delete Funding", };
            foreach (var item in Level038)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Partner Funding Sources").Id };
                SavePermissions(context, perm);
            }

            List<string> Level039 = new List<string>() { "View Settlement", "Edit Settlement", "Add Settlement", "Delete Settlement", };
            foreach (var item in Level039)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Partner Settlement Accounts").Id };
                SavePermissions(context, perm);
            }

            List<string> Level3110 = new List<string>() { "View Users", "Edit Users", "Add Users", "Delete Users", };
            foreach (var item in Level3110)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Partner User Management").Id };
                SavePermissions(context, perm);
            }

            List<string> Level05 = new List<string>() { "View Reports", "Reports Admin", "Terminal Reports", "Transaction Reports", "Settlement Reports", "Merchant Reports", "Alerts Reports", };
            foreach (var item in Level05)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Reports").Id };
                SavePermissions(context, perm);
            }

            List<string> Level06 = new List<string> { "View Terminal Management", "View Terminal Statistics", "Cash Management", "Terminal Admin", "Alert Settings", "Compliance Documents", "General Info Terminals", "Interchange Split", "Surcharge Split", "Terminal Contacts", "Terminal Notes", "Terminal Pictures", "Transactions", "Vault Cash Info", };
            foreach (var item in Level06)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Terminals").Id };
                SavePermissions(context, perm);
            }

            List<string> Level041 = new List<string>() { "Add Terminal", "Clone Terminal", "Delete Terminal", "Bind Master Key", "Change Terminal Partner", };
            foreach (var item in Level041)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Terminal Admin").Id };
                SavePermissions(context, perm);
            }

            List<string> Level042 = new List<string>() { "Edit Terminals Alert Settings", };
            foreach (var item in Level042)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Alert Settings").Id };
                SavePermissions(context, perm);
            }


            List<string> Level043 = new List<string>() { "View Compliance", "Edit Compliance", };
            foreach (var item in Level043)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Compliance Documents").Id };
                SavePermissions(context, perm);
            }

            List<string> Level044 = new List<string>() { "View General Info Terminals", "Edit General Info", "Edit Terminal Status", "Manage Terminal Address", "View Terminal Contract Info", "View Terminal Sponsor Info", };
            foreach (var item in Level044)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "General Info Terminals").Id };
                SavePermissions(context, perm);
            }

            List<string> Level045 = new List<string>() { "View Interchange Split", "Add Interchange Split", "Edit Interchange Split", "Delete Interchange Split", };
            foreach (var item in Level045)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Interchange Split").Id };
                SavePermissions(context, perm);
            }

            List<string> Level046 = new List<string>() { "View Surcharge Split", "Add Surcharge Split", "Edit Surcharge Split", "Delete Surcharge Split", };
            foreach (var item in Level046)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Surcharge Split").Id };
                SavePermissions(context, perm);
            }

            List<string> Level047 = new List<string>() { "View Terminal Contacts", "Edit Terminal Contacts", "Delete Terminal Contacts", };
            foreach (var item in Level047)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Terminal Contacts").Id };
                SavePermissions(context, perm);
            }

            List<string> Level048 = new List<string>() { "View Terminal Notes", "Edit Terminal Notes", };
            foreach (var item in Level048)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Terminal Notes").Id };
                SavePermissions(context, perm);
            }

            List<string> Level049 = new List<string>() { "View Terminal Pictures", "Edit Terminal Pictures", "Delete Terminal Pictures", };
            foreach (var item in Level049)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Terminal Pictures").Id };
                SavePermissions(context, perm);
            }

            List<string> Level051 = new List<string>() { "View Switch Messages", "Current Days Transaction", };
            foreach (var item in Level051)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Transactions").Id };
                SavePermissions(context, perm);
            }

            List<string> Level052 = new List<string>() { "View Vault Cash Info", "Edit Vault Cash Info", "Add Vault Cash Info", "Delete Vault Cash Info", };
            foreach (var item in Level052)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Vault Cash Info").Id };
                SavePermissions(context, perm);
            }

            #endregion

            #region Permission Level 2

            List<string> Level121 = new List<string> { "Edit Reporting Groups", "Manage Report Subscriptions", "Display Surcharge Column on Reports", };
            foreach (var item in Level121)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Reports Admin").Id };
                SavePermissions(context, perm);
            }

            List<string> Level122 = new List<string> { "Cash Balance at Close Report", "Encryption Levels Report", "Terminal Notes Report", "Terminal Configuration Sheet", "Terminal List", "Terminal List By Partner", "Terminal Settlement Setup", "Terminal Status", "Vault Cash Projection Report", };
            foreach (var item in Level122)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Terminal Reports").Id };
                SavePermissions(context, perm);
            }

            List<string> Level123 = new List<string> { "Daily Transaction Summary", "Transaction Analysis", "Transaction Averages Report", "Transaction Detail", "Transaction Summary", "Transactions By Network Summary", "Transactions by Network Summary Filtered", "Transactions Per Hour Graph", "Monthly Transaction Summary", "Transaction Monthly Summary", };
            foreach (var item in Level123)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Transaction Reports").Id };
                SavePermissions(context, perm);
            }


            List<string> Level124 = new List<string> { "ACH Detail", "Cash Projection Summary Report", "Consolidated Settlement Report", "Disbursement Setup History", "eInvoice Authorized Accounts", "Funds Reconciliation Report", "Settlement Split List", "Settlement Split", "Monthly EBT", };
            foreach (var item in Level124)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Settlement Reports").Id };
                SavePermissions(context, perm);
            }

            List<string> Level125 = new List<string> { "Monthly Statement", "Monthly Statement by Bank Account", "Monthly Statement with Interchange", "Monthly Statement By Terminal Profile", "Profile Transaction Summary", "User Report", "User Permissions Report", };
            foreach (var item in Level125)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Merchant Reports").Id };
                SavePermissions(context, perm);
            }

            List<string> Level126 = new List<string> { "Dispute Email Reprints", "Dispute Status Report", "Dispute History", };
            foreach (var item in Level126)
            {
                Permission perm = new Permission() { Name = item, ParentID = allPermissions.Single(m => m.Name == "Alerts Reports").Id };
                SavePermissions(context, perm);
            }

            #endregion


            User userAdmin = new User()
            {
                Permissions = new List<Permission>()
            };

            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                //traigo de BD el usuario admin con sus permisos. Hago el using par aque no de problemas con el otro context.
                userAdmin = context.Users.Include(m => m.Permissions).Where(m => m.UserName == admin02.UserName).FirstOrDefault();
                dbContext.Dispose();
            }

            //foreach (Permission permission in allPermissions)
            //{
            //    if (!userAdmin.Permissions.Contains(permission) || (userAdmin.Permissions == null))             //valido si el permiso no esta en el usaurio.
            //    {
            //        permission.Users.Add(admin02);
            //    }
            //}
        }


        private void SavePermissions(ApplicationDbContext context, Permission perm)
        {
            context.Permissions.AddOrUpdate(m => m.Name, perm);                                         //comprobar si el permissions esta para guardarlo o updated
                                                                                                        //ideal que se le pueda hacer Include al AddOrUpdate para que ademas de que me devuelva el ID, tambiem me devuelva el USER que tiene este permiso.
            context.SaveChanges();                                                                      //para que me devuelva el ID que asigno el insertar. automaticamente perm toma el id.
            perm = context.Permissions.Include(u => u.Users).FirstOrDefault(p => p.Name == perm.Name);  //para poder ponerle el include de User porque perm no trae el user que lo tiene y el Contains me decia que no lo tenia.

            var user = context.Users.Include(m => m.Permissions.Select(u => u.Users)).FirstOrDefault(a => a.Id == admin02.Id);
            
            if (user.Permissions.Contains(perm) == false)
            {
                context.Users.FirstOrDefault(u => u.Id == user.Id).Permissions.Add(perm);
                //perm.Users.Add(admin02);                                                              //adicionar el usaurio admin a todos los permisos.
            }


            context.SaveChanges();                                                                      //salvar los cambios en DB para que me traiga el ID del permiso en DB
            allPermissions.Add(perm);                                                                   //guardo listado de permissos para ponerlo como Parent en el Arbol de permisos.
        }
    }
}
