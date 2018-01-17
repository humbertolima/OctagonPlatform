namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReportFilters", "SubscriptionID", "dbo.Subscriptions");
            AddForeignKey("dbo.ReportFilters", "SubscriptionID", "dbo.Subscriptions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReportFilters", "SubscriptionID", "dbo.Subscriptions");
            AddForeignKey("dbo.ReportFilters", "SubscriptionID", "dbo.Subscriptions", "Id");
        }
    }
}
