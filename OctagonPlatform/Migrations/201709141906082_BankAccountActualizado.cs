namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BankAccountActualizado : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.BankAccounts", new[] { "FedTax" });
            DropIndex("dbo.BankAccounts", new[] { "Ssn" });
            DropIndex("dbo.BankAccounts", new[] { "Phone" });
            DropIndex("dbo.BankAccounts", new[] { "Email" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.BankAccounts", "Email", unique: true);
            CreateIndex("dbo.BankAccounts", "Phone", unique: true);
            CreateIndex("dbo.BankAccounts", "Ssn", unique: true);
            CreateIndex("dbo.BankAccounts", "FedTax", unique: true);
        }
    }
}
