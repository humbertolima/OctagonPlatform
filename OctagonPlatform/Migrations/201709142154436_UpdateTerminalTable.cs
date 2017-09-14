namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTerminalTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BankAccounts", "Ssn", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BankAccounts", "Ssn", c => c.String(nullable: false));
        }
    }
}
