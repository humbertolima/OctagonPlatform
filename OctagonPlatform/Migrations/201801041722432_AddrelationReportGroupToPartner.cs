namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddrelationReportGroupToPartner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReportGroups", "PartnerId", c => c.Int(nullable: false));
            CreateIndex("dbo.ReportGroups", "PartnerId");
            AddForeignKey("dbo.ReportGroups", "PartnerId", "dbo.Partners", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReportGroups", "PartnerId", "dbo.Partners");
            DropIndex("dbo.ReportGroups", new[] { "PartnerId" });
            DropColumn("dbo.ReportGroups", "PartnerId");
        }
    }
}
