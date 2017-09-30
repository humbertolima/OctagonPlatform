namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ElimineCampoInterchangedeTerminals : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Terminals", "InterChangeAmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Terminals", "InterChangeAmount", c => c.Double(nullable: false));
        }
    }
}
