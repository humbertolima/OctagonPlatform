namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addreportgroupid : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Terminals", name: "ReportGroupModel_Id", newName: "ReportGroupId");
            RenameIndex(table: "dbo.Terminals", name: "IX_ReportGroupModel_Id", newName: "IX_ReportGroupId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Terminals", name: "IX_ReportGroupId", newName: "IX_ReportGroupModel_Id");
            RenameColumn(table: "dbo.Terminals", name: "ReportGroupId", newName: "ReportGroupModel_Id");
        }
    }
}
