namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BankAccountBankName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankAccounts", "BankName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BankAccounts", "BankName");
        }
    }
}
