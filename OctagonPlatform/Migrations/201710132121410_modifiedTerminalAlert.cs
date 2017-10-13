namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifiedTerminalAlert : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TerminalAlerts", "CashAvailable", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TerminalAlerts", "CashAvailable", c => c.Int(nullable: false));
        }
    }
}
