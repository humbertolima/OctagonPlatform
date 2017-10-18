namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ElimineTodosLOsCamposUnique : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.BankAccounts", new[] { "NickName" });
            DropIndex("dbo.BankAccounts", new[] { "RoutingNumber" });
            DropIndex("dbo.BankAccounts", new[] { "AccountNumber" });
            DropIndex("dbo.Partners", new[] { "BusinessName" });
            DropIndex("dbo.Partners", new[] { "Email" });
            DropIndex("dbo.PartnerContacts", new[] { "Email" });
            DropIndex("dbo.TerminalContacts", new[] { "Email" });
            DropIndex("dbo.TerminalContacts", new[] { "Phone" });
            DropIndex("dbo.Terminals", new[] { "MachineSerialNumber" });
            DropIndex("dbo.Users", new[] { "Email" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Users", "Email", unique: true);
            CreateIndex("dbo.Terminals", "MachineSerialNumber", unique: true);
            CreateIndex("dbo.TerminalContacts", "Phone", unique: true);
            CreateIndex("dbo.TerminalContacts", "Email", unique: true);
            CreateIndex("dbo.PartnerContacts", "Email", unique: true);
            CreateIndex("dbo.Partners", "Email", unique: true);
            CreateIndex("dbo.Partners", "BusinessName", unique: true);
            CreateIndex("dbo.BankAccounts", "AccountNumber", unique: true);
            CreateIndex("dbo.BankAccounts", "RoutingNumber", unique: true);
            CreateIndex("dbo.BankAccounts", "NickName", unique: true);
        }
    }
}
