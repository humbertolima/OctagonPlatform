namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relationmanytoman5567ppoopouy : DbMigration
    {
        public override void Up()
        {
            
           
            CreateTable(
                "dbo.ReportGroupModelTerminals",
                c => new
                    {
                        ReportGroupModel_Id = c.Int(nullable: false),
                        Terminal_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ReportGroupModel_Id, t.Terminal_Id })
                .ForeignKey("dbo.ReportGroups", t => t.ReportGroupModel_Id)
                .ForeignKey("dbo.Terminals", t => t.Terminal_Id)
                .Index(t => t.ReportGroupModel_Id)
                .Index(t => t.Terminal_Id);
            
           
        }
        
        public override void Down()
        {
           
            DropForeignKey("dbo.ReportGroupModelTerminals", "Terminal_Id", "dbo.Terminals");
            DropForeignKey("dbo.ReportGroupModelTerminals", "ReportGroupModel_Id", "dbo.ReportGroups");
            DropIndex("dbo.ReportGroupModelTerminals", new[] { "Terminal_Id" });
            DropIndex("dbo.ReportGroupModelTerminals", new[] { "ReportGroupModel_Id" });
            DropTable("dbo.ReportGroupModelTerminals");
          
           
        }
    }
}
