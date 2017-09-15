namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualizarTerminal : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Terminals", "BankAccountId", "dbo.BankAccounts");
            DropIndex("dbo.Terminals", new[] { "BankAccountId" });
            AlterColumn("dbo.Terminals", "BankAccountId", c => c.Int());
            CreateIndex("dbo.Terminals", "BankAccountId");
            AddForeignKey("dbo.Terminals", "BankAccountId", "dbo.BankAccounts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Terminals", "BankAccountId", "dbo.BankAccounts");
            DropIndex("dbo.Terminals", new[] { "BankAccountId" });
            AlterColumn("dbo.Terminals", "BankAccountId", c => c.Int(nullable: false));
            CreateIndex("dbo.Terminals", "BankAccountId");
            AddForeignKey("dbo.Terminals", "BankAccountId", "dbo.BankAccounts", "Id", cascadeDelete: true);
        }
    }
}
