namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterFieldTerminalIdByIndexIdDispute : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Disputes", "TerminalId", "dbo.Terminals");
            DropIndex("dbo.Disputes", new[] { "TerminalId" });
            RenameColumn(table: "dbo.Disputes", name: "TerminalId", newName: "Terminal_Id");
            AddColumn("dbo.Disputes", "IndexId", c => c.Int(nullable: false));
            AlterColumn("dbo.Disputes", "Terminal_Id", c => c.Int());
            CreateIndex("dbo.Disputes", "Terminal_Id");
            AddForeignKey("dbo.Disputes", "Terminal_Id", "dbo.Terminals", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Disputes", "Terminal_Id", "dbo.Terminals");
            DropIndex("dbo.Disputes", new[] { "Terminal_Id" });
            AlterColumn("dbo.Disputes", "Terminal_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Disputes", "IndexId");
            RenameColumn(table: "dbo.Disputes", name: "Terminal_Id", newName: "TerminalId");
            CreateIndex("dbo.Disputes", "TerminalId");
            AddForeignKey("dbo.Disputes", "TerminalId", "dbo.Terminals", "Id", cascadeDelete: true);
        }
    }
}
