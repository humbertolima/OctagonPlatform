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
            public const string GetCashManagement = "Get CashManagement Terminal";
            public const string GetInterchanges = "Get Intershanges Terminal";
            public const string GetSurcharges = "Get Surcharges Terminal";
            public const string GetVaultCash = "Get VaultCash Terminal";
            public const string GetContacts = "Get Contacts Terminal";

            public const string GetConfiguration  = "Get Configuration Terminal";
            public const string SetConfiguration  = "Set Configuration Terminal";

            
            public const string  SetWorkingHours  = "Set WorkingHours Terminal";
            public const string  AddWorkingHours  = "Add WorkingHours Terminal";
            public const string  DeleteWorkingHours   = "Delete WorkingHours Terminal";

            
            public const string GetPictures = "Get Pictures Terminal";
            public const string SetPictures = "Set Pictures Terminal";
            public const string PictureDelete  = "Delete Pictures Terminal";

            public const string GetDocuments = "Get Documents Terminal";
            public const string SetDocuments = "Set Documents Terminal";
            public const string DocumentDelete = "Delete Documents Terminal";

            public const string GetGeneralInfo = "Get GeneralInfo Terminal";

            public const string GetNotes = "Get Notes Terminal";
            public const string SetNotes = "Set Notes Terminal";
            public const string DeleteNotes = "Delete Notes Terminal";

            public const string GetCassettes = "Get Cassettes Terminal";
            public const string SetCassettes = "Set Cassettes Terminal";
            public const string CassetteDelete  = "Delete Cassettes Terminal";



            

            public const string TerminalFull = AddTerminal + "," + EditTerminal + "," + DeleteTerminal + "," + DetailTerminal + "," + ListAllTerminal;
        }
    }
}