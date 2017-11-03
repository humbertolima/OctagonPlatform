namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTerminalsAlerts2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TerminalAlerts", "TerminalId", "dbo.Terminals");
            DropIndex("dbo.TerminalAlerts", new[] { "TerminalId" });
            AddColumn("dbo.TerminalAlerts", "Terminal_Id", c => c.Int());
            AlterColumn("dbo.TerminalAlerts", "TerminalId", c => c.String(nullable: false));
            CreateIndex("dbo.TerminalAlerts", "Terminal_Id");
            AddForeignKey("dbo.TerminalAlerts", "Terminal_Id", "dbo.Terminals", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TerminalAlerts", "Terminal_Id", "dbo.Terminals");
            DropIndex("dbo.TerminalAlerts", new[] { "Terminal_Id" });
            AlterColumn("dbo.TerminalAlerts", "TerminalId", c => c.Int(nullable: false));
            DropColumn("dbo.TerminalAlerts", "Terminal_Id");
            CreateIndex("dbo.TerminalAlerts", "TerminalId");
            AddForeignKey("dbo.TerminalAlerts", "TerminalId", "dbo.Terminals", "Id", cascadeDelete: false);
        }
    }
}
