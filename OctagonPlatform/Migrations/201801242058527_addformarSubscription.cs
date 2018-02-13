namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addformarSubscription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subscriptions", "Format", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subscriptions", "Format");
        }
    }
}
