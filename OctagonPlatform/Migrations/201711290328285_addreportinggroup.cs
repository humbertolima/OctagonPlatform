namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addreportinggroup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReportGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Terminals", "LocationName", c => c.String(nullable: false));
            AddColumn("dbo.Terminals", "ReportGroupModel_Id", c => c.Int());
            CreateIndex("dbo.Terminals", "ReportGroupModel_Id");
            AddForeignKey("dbo.Terminals", "ReportGroupModel_Id", "dbo.ReportGroups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Terminals", "ReportGroupModel_Id", "dbo.ReportGroups");
            DropIndex("dbo.Terminals", new[] { "ReportGroupModel_Id" });
            DropColumn("dbo.Terminals", "ReportGroupModel_Id");
            DropColumn("dbo.Terminals", "LocationName");
            DropTable("dbo.ReportGroups");
        }
    }
}
