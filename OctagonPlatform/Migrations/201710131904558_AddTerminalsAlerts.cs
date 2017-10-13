namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTerminalsAlerts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TerminalAlerts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TerminalId = c.Int(nullable: false),
                        Notificated = c.Boolean(nullable: false),
                        CashAvailable = c.Int(nullable: false),
                        AlarmChestdooropen = c.String(),
                        AlarmTopdooropen = c.String(),
                        AlarmSupervisoractive = c.String(),
                        Receiptprinterpaperstatus = c.String(),
                        ReceiptPrinterRibbonStatus = c.String(),
                        JournalPrinterPaperStatus = c.String(),
                        JournalPrinterRibbonStatus = c.String(),
                        NoteStatusDispenser = c.String(),
                        ReceiptPrinter = c.String(),
                        JournalPrinter = c.String(),
                        Dispenser = c.String(),
                        CommunicationsSystem = c.String(),
                        CardReader = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Terminals", t => t.TerminalId, cascadeDelete: true)
                .Index(t => t.TerminalId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TerminalAlerts", "TerminalId", "dbo.Terminals");
            DropIndex("dbo.TerminalAlerts", new[] { "TerminalId" });
            DropTable("dbo.TerminalAlerts");
        }
    }
}
