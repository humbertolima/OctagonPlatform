namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualizarTerminal2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Terminals", "WhoInitiates", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Terminals", "WhoInitiates");
        }
    }
}
