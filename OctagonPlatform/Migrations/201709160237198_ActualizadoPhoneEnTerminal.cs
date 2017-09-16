namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualizadoPhoneEnTerminal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Terminals", "Phone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Terminals", "Phone");
        }
    }
}
