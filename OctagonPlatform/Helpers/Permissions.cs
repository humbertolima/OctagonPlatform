using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Helpers
{
    public static class Permissions
    {
        public static class Terminals
        {
            public const string AddTerminal = "Add Terminal";
            public const string EditTerminal = "Edit Terminal";
            public const string DeleteTerminal = "Delete Terminal";
            public const string DetailTerminal = "Details Terminal";
            public const string ListAllTerminal = "List All Terminals";
            public const string Search = "Search Terminals";

            public const string GetKeyTerminal = "Get Key Terminal";
            public const string SetBindKey = "Set BindKey Terminal";

            public const string GetCashManagement = "Cash Management";

            public const string GetInterchanges = "View Interchange Split";
            public const string AddInterchanges = "Add Interchange Split";
            public const string EditInterchanges = "Edit Interchange Split";
            public const string DeleteInterchanges = "Delete Interchange Split";

            public const string GetSurcharges = "View Surcharge Split";
            public const string AddSurcharges = "Add Surcharge Split";
            public const string EditSurcharges = "Edit Surcharge Split";
            public const string DeleteSurcharges = "Delete Surcharge Split";

            public const string GetVaultCash = "View Vault Cash Info";
            public const string EditVaultCash = "Edit Vault Cash Info";
            public const string AddVaultCash = "Add Vault Cash Info";
            public const string DeleteVaultCash = "Delete Vault Cash Info";

            public const string GetContacts = "View Terminal Contacts";
            public const string EditContacts = "Edit Terminal Contacts";
            public const string AddContacts = "Add Terminal Contacts";
            public const string DeleteContacts = "Delete Terminal Contacts";

            public const string GetConfiguration = "Get Configuration Terminal";
            public const string SetConfiguration = "Set Configuration Terminal";


            public const string SetWorkingHours = "Set WorkingHours Terminal";
            public const string AddWorkingHours = "Add WorkingHours Terminal";
            public const string DeleteWorkingHours = "Delete WorkingHours Terminal";


            public const string GetPictures = "View Terminal Pictures";
            public const string SetPictures = "Edit Terminal Pictures";
            public const string PictureDelete = "Delete Terminal Pictures";

            public const string GetDocuments = "View Compliance";
            public const string SetDocuments = "Edit Compliance";
            public const string DocumentDelete = "Delete Compliance";

            public const string GetGeneralInfo = "View General Info Terminals";

            public const string GetNotes = "View Terminal Notes";
            public const string AddNotes = "Add Notes Terminal";
            public const string SetNotes = "Edit Notes Terminal";
            public const string DeleteNotes = "Delete Notes Terminal";

            public const string GetCassettes = "Get Cassettes Terminal";
            public const string AddCassettes = "Add Cassettes Terminal";
            public const string SetCassettes = "Set Cassettes Terminal";
            public const string CassetteDelete = "Delete Cassettes Terminal";





            public const string TerminalFull = AddTerminal + "," + EditTerminal + "," + DeleteTerminal + "," + DetailTerminal + "," + ListAllTerminal;
        }

        public static class Partner
        {
            public const string GetContacts = "View Partner Contacts";
            public const string EditContacts = "Edit Partner Contacts";
            public const string DeleteContacts = "Delete Partner Contacts";
            public const string AddContacts = "Add Partner Contacts";
            public const string SearchContacts = "Search Partner Contacts";
            

            public const string GetPartners = "View Partner";
            public const string GetAllPartners = "List All Partner";
            public const string AddPartners = "Add Partner";
            public const string EditPartners = "Edit Partner";
            public const string DeletePartners = "Delete Partner";
            public const string SearchPartners = "Search Partner";

            

            public const string GetBankAccounts = "Get Bank Accounts";
            public const string AddtBankAccounts = "Add Bank Accounts";
            public const string EditBankAccounts = "Edit Bank Accounts";
            public const string DeleteBankAccounts = "Delete Bank Accounts";
            public const string SearchBankAccount = "Search Bank Accounts";
            public const string AutoAccountBankAccount = "AutoAccount Bank Accounts";

            
            public const string GetTerminals = "Get Terminals Partners";
            public const string AddTerminals = "Add Terminals Partners";
            public const string EditTerminals = "Edit Terminals Partners";
            public const string DeleteTerminals = "Delete Terminals Partners";

            public const string GetUsers = "Get Users";
            public const string AddUsers = "Add Users";
            public const string EditUsers = "Edit Users";
            public const string DeleteUsers = "Delete Users";
            public const string SearchUsers = "Search Users";
            

            //public const string  = "";
            //public const string  = "";

        }
    }
}