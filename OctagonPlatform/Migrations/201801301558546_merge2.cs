namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class merge2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "TimeZoneInfo");
            DropColumn("dbo.Subscriptions", "Format");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subscriptions", "Format", c => c.String(maxLength: 10));
            AddColumn("dbo.Users", "TimeZoneInfo", c => c.String());
        }
    }
}
