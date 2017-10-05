namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambiarRelacionUnoAUnoTerminalAlertConfig : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TerminalAlertConfigs", "TerminalId", "dbo.Terminals");
            DropForeignKey("dbo.TerminalMessages", "TerminalAlertConfigId", "dbo.TerminalAlertConfigs");
            DropPrimaryKey("dbo.TerminalAlertConfigs");
            AddPrimaryKey("dbo.TerminalAlertConfigs", "TerminalId");
            AddForeignKey("dbo.TerminalAlertConfigs", "TerminalId", "dbo.Terminals", "Id");
            AddForeignKey("dbo.TerminalMessages", "TerminalAlertConfigId", "dbo.TerminalAlertConfigs", "TerminalId", cascadeDelete: true);
            DropColumn("dbo.TerminalAlertConfigs", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TerminalAlertConfigs", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.TerminalMessages", "TerminalAlertConfigId", "dbo.TerminalAlertConfigs");
            DropForeignKey("dbo.TerminalAlertConfigs", "TerminalId", "dbo.Terminals");
            DropPrimaryKey("dbo.TerminalAlertConfigs");
            AddPrimaryKey("dbo.TerminalAlertConfigs", "Id");
            AddForeignKey("dbo.TerminalMessages", "TerminalAlertConfigId", "dbo.TerminalAlertConfigs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TerminalAlertConfigs", "TerminalId", "dbo.Terminals", "Id", cascadeDelete: true);
        }
    }
}
