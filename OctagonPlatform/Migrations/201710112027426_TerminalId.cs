namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TerminalId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Terminals", "TerminalId", c => c.String(nullable: false));
            DropColumn("dbo.Terminals", "BindedKeys");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Terminals", "BindedKeys", c => c.Boolean(nullable: false));
            DropColumn("dbo.Terminals", "TerminalId");
        }
    }
}
