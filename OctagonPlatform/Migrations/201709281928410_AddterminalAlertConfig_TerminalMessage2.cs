namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddterminalAlertConfig_TerminalMessage2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TerminalMessages", "TerminalAlertConfig_Id", "dbo.TerminalAlertConfigs");
            DropIndex("dbo.TerminalMessages", new[] { "TerminalAlertConfig_Id" });
            RenameColumn(table: "dbo.TerminalMessages", name: "TerminalAlertConfig_Id", newName: "TerminalAlertConfigId");
            AlterColumn("dbo.TerminalMessages", "TerminalAlertConfigId", c => c.Int(nullable: false));
            CreateIndex("dbo.TerminalMessages", "TerminalAlertConfigId");
            AddForeignKey("dbo.TerminalMessages", "TerminalAlertConfigId", "dbo.TerminalAlertConfigs", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TerminalMessages", "TerminalAlertConfigId", "dbo.TerminalAlertConfigs");
            DropIndex("dbo.TerminalMessages", new[] { "TerminalAlertConfigId" });
            AlterColumn("dbo.TerminalMessages", "TerminalAlertConfigId", c => c.Int());
            RenameColumn(table: "dbo.TerminalMessages", name: "TerminalAlertConfigId", newName: "TerminalAlertConfig_Id");
            CreateIndex("dbo.TerminalMessages", "TerminalAlertConfig_Id");
            AddForeignKey("dbo.TerminalMessages", "TerminalAlertConfig_Id", "dbo.TerminalAlertConfigs", "Id");
        }
    }
}
