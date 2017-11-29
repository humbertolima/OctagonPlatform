namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCassetteFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cassettes", "AutoRecord", c => c.Boolean(nullable: false));
            DropColumn("dbo.Cassettes", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cassettes", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Cassettes", "AutoRecord");
        }
    }
}
