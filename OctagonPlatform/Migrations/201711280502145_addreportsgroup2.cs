namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addreportsgroup2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ReportGroups", "TerminalId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReportGroups", "TerminalId", c => c.Int(nullable: false));
        }
    }
}
