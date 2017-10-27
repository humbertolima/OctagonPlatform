namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateFieldInactivePeriodTerminal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TerminalAlertConfigs", "IgnoreHoursInactive", c => c.Int(nullable: false));
            DropColumn("dbo.TerminalAlertConfigs", "InactivePeriod");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TerminalAlertConfigs", "InactivePeriod", c => c.DateTime());
            DropColumn("dbo.TerminalAlertConfigs", "IgnoreHoursInactive");
        }
    }
}
