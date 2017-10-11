namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TerminalIdTerminalAlertConfig : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TerminalAlertConfigs", name: "TerminalId", newName: "Id");
            RenameIndex(table: "dbo.TerminalAlertConfigs", name: "IX_TerminalId", newName: "IX_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TerminalAlertConfigs", name: "IX_Id", newName: "IX_TerminalId");
            RenameColumn(table: "dbo.TerminalAlertConfigs", name: "Id", newName: "TerminalId");
        }
    }
}
