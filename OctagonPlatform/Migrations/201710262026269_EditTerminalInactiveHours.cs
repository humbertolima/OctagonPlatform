namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditTerminalInactiveHours : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Terminals", "InactiveHours", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Terminals", "InactiveHours");
        }
    }
}
