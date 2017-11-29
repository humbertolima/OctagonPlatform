namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TerminalDefaultBankAccount : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Terminals", "BankAccountId", "dbo.BankAccounts");
            DropIndex("dbo.Terminals", new[] { "BankAccountId" });
            AlterColumn("dbo.Terminals", "BankAccountId", c => c.Int(nullable: false));
            CreateIndex("dbo.Terminals", "BankAccountId");
            AddForeignKey("dbo.Terminals", "BankAccountId", "dbo.BankAccounts", "Id", cascadeDelete: false);
            DropColumn("dbo.Terminals", "SurchargeAmount");
            DropColumn("dbo.Terminals", "SurchargeByPercent");
            DropColumn("dbo.Terminals", "SurchargeType");
            DropColumn("dbo.Terminals", "FixSurcharge");
            DropColumn("dbo.Terminals", "SettledType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Terminals", "SettledType", c => c.Int(nullable: false));
            AddColumn("dbo.Terminals", "FixSurcharge", c => c.Double(nullable: false));
            AddColumn("dbo.Terminals", "SurchargeType", c => c.Int(nullable: false));
            AddColumn("dbo.Terminals", "SurchargeByPercent", c => c.Double(nullable: false));
            AddColumn("dbo.Terminals", "SurchargeAmount", c => c.Double(nullable: false));
            DropForeignKey("dbo.Terminals", "BankAccountId", "dbo.BankAccounts");
            DropIndex("dbo.Terminals", new[] { "BankAccountId" });
            AlterColumn("dbo.Terminals", "BankAccountId", c => c.Int());
            CreateIndex("dbo.Terminals", "BankAccountId");
            AddForeignKey("dbo.Terminals", "BankAccountId", "dbo.BankAccounts", "Id");
        }
    }
}
