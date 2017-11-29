namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateInitialv01 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Disputes", new[] { "Terminal_Id" });
            DropColumn("dbo.Disputes", "IndexId");
            RenameColumn(table: "dbo.Disputes", name: "Terminal_Id", newName: "IndexId");
            AlterColumn("dbo.Disputes", "IndexId", c => c.Int(nullable: false));
            CreateIndex("dbo.Disputes", "IndexId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Disputes", new[] { "IndexId" });
            AlterColumn("dbo.Disputes", "IndexId", c => c.Int());
            RenameColumn(table: "dbo.Disputes", name: "IndexId", newName: "Terminal_Id");
            AddColumn("dbo.Disputes", "IndexId", c => c.Int(nullable: false));
            CreateIndex("dbo.Disputes", "Terminal_Id");
        }
    }
}
