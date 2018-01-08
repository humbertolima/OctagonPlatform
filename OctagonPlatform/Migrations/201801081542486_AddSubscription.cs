namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubscription : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Description = c.String(),
                        EmailComment = c.String(),
                        ScheduledId = c.Int(nullable: false),
                        PartnerId = c.Int(nullable: false),
                        Schedule_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Partners", t => t.PartnerId)
                .ForeignKey("dbo.Schedules", t => t.Schedule_ID)
                .Index(t => t.PartnerId)
                .Index(t => t.Schedule_ID);
            
            CreateTable(
                "dbo.ReportFilters",
                c => new
                    {
                        ReportID = c.Int(nullable: false),
                        FilterID = c.Int(nullable: false),
                        SubscriptionID = c.Int(nullable: false),
                        Value = c.String(),
                    })
                .PrimaryKey(t => new { t.ReportID, t.FilterID })
                .ForeignKey("dbo.Filters", t => t.FilterID)
                .ForeignKey("dbo.Reports", t => t.ReportID)
                .ForeignKey("dbo.Subscriptions", t => t.SubscriptionID)
                .Index(t => t.ReportID)
                .Index(t => t.FilterID)
                .Index(t => t.SubscriptionID);
            
            CreateTable(
                "dbo.Filters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subscriptions", "Schedule_ID", "dbo.Schedules");
            DropForeignKey("dbo.ReportFilters", "SubscriptionID", "dbo.Subscriptions");
            DropForeignKey("dbo.ReportFilters", "ReportID", "dbo.Reports");
            DropForeignKey("dbo.ReportFilters", "FilterID", "dbo.Filters");
            DropForeignKey("dbo.Subscriptions", "PartnerId", "dbo.Partners");
            DropIndex("dbo.ReportFilters", new[] { "SubscriptionID" });
            DropIndex("dbo.ReportFilters", new[] { "FilterID" });
            DropIndex("dbo.ReportFilters", new[] { "ReportID" });
            DropIndex("dbo.Subscriptions", new[] { "Schedule_ID" });
            DropIndex("dbo.Subscriptions", new[] { "PartnerId" });
            DropTable("dbo.Filters");
            DropTable("dbo.ReportFilters");
            DropTable("dbo.Subscriptions");
        }
    }
}
