namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLocationNameTerminal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Terminals", "LocationName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Terminals", "LocationName");
        }
    }
}
