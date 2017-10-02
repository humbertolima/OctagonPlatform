namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoFixAmmounEnTerminales : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Terminals", "FixSurcharge", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Terminals", "FixSurcharge");
        }
    }
}
