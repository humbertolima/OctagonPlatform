namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class format : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Subscriptions", "Format", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subscriptions", "Format", c => c.String(maxLength: 1));
        }
    }
}
