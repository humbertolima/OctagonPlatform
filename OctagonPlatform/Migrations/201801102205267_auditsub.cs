namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class auditsub : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subscriptions", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.Subscriptions", "CreatedBy", c => c.Int());
            AddColumn("dbo.Subscriptions", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.Subscriptions", "DeletedBy", c => c.Int());
            AddColumn("dbo.Subscriptions", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.Subscriptions", "UpdatedBy", c => c.Int());
            AddColumn("dbo.Subscriptions", "UpdatedByName", c => c.String());
            AddColumn("dbo.Subscriptions", "CreatedByName", c => c.String());
            AddColumn("dbo.Subscriptions", "DeletedByName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subscriptions", "DeletedByName");
            DropColumn("dbo.Subscriptions", "CreatedByName");
            DropColumn("dbo.Subscriptions", "UpdatedByName");
            DropColumn("dbo.Subscriptions", "UpdatedBy");
            DropColumn("dbo.Subscriptions", "UpdatedAt");
            DropColumn("dbo.Subscriptions", "DeletedBy");
            DropColumn("dbo.Subscriptions", "DeletedAt");
            DropColumn("dbo.Subscriptions", "CreatedBy");
            DropColumn("dbo.Subscriptions", "CreatedAt");
        }
    }
}
