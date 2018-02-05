namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usertimezone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "TimeZoneInfo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "TimeZoneInfo");
        }
    }
}
