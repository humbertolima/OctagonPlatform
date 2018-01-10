namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addkeyreportfilter : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ReportFilters");
            AddPrimaryKey("dbo.ReportFilters", new[] { "ReportID", "FilterID", "SubscriptionID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ReportFilters");
            AddPrimaryKey("dbo.ReportFilters", new[] { "ReportID", "FilterID" });
        }
    }
}
