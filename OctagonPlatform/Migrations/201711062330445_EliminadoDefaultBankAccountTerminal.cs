namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EliminadoDefaultBankAccountTerminal : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Terminals", "BankAccountId", "dbo.BankAccounts");
            DropIndex("dbo.Terminals", new[] { "BankAccountId" });
            RenameColumn(table: "dbo.Terminals", name: "BankAccountId", newName: "BankAccount_Id");
            AlterColumn("dbo.Terminals", "BankAccount_Id", c => c.Int());
            CreateIndex("dbo.Terminals", "BankAccount_Id");
            AddForeignKey("dbo.Terminals", "BankAccount_Id", "dbo.BankAccounts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Terminals", "BankAccount_Id", "dbo.BankAccounts");
            DropIndex("dbo.Terminals", new[] { "BankAccount_Id" });
            AlterColumn("dbo.Terminals", "BankAccount_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Terminals", name: "BankAccount_Id", newName: "BankAccountId");
            CreateIndex("dbo.Terminals", "BankAccountId");
            AddForeignKey("dbo.Terminals", "BankAccountId", "dbo.BankAccounts", "Id", cascadeDelete: true);
        }
    }
}
