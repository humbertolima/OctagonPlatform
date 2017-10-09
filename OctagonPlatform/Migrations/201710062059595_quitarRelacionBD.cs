namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quitarRelacionBD : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TerminalMessages", "TerminalAlertConfigId", "dbo.TerminalAlertConfigs");
            DropIndex("dbo.TerminalMessages", "TerminalMessage_Id_8583_Index");
            DropIndex("dbo.TerminalMessages", new[] { "TerminalAlertConfigId" });
            AddColumn("dbo.TerminalAlertConfigs", "IgnoreChestDoorOpen", c => c.Boolean(nullable: false));
            AddColumn("dbo.TerminalAlertConfigs", "IgnoreTopDoorOpen", c => c.Boolean(nullable: false));
            AddColumn("dbo.TerminalAlertConfigs", "IgnoreReceiptPaper", c => c.Boolean(nullable: false));
            AddColumn("dbo.TerminalAlertConfigs", "IgnoreReceiptRibbon", c => c.Boolean(nullable: false));
            AddColumn("dbo.TerminalAlertConfigs", "IgnoreJournalPaper", c => c.Boolean(nullable: false));
            AddColumn("dbo.TerminalAlertConfigs", "IgnoreJournalRibbon", c => c.Boolean(nullable: false));
            AddColumn("dbo.TerminalAlertConfigs", "IgnoreCassetteNotes", c => c.Boolean(nullable: false));
            AddColumn("dbo.TerminalAlertConfigs", "IgnoreReceiptNeedAttention", c => c.Boolean(nullable: false));
            AddColumn("dbo.TerminalAlertConfigs", "IgnoreJournalNeedAttention", c => c.Boolean(nullable: false));
            AddColumn("dbo.TerminalAlertConfigs", "IgnoreBillDispenserNeedAttention", c => c.Boolean(nullable: false));
            AddColumn("dbo.TerminalAlertConfigs", "IgnoreCommNeedAttention", c => c.Boolean(nullable: false));
            AddColumn("dbo.TerminalAlertConfigs", "IgnoreCardReaderNeedAttention", c => c.Boolean(nullable: false));
            DropTable("dbo.TerminalMessages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TerminalMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Id_8583 = c.String(nullable: false, maxLength: 50),
                        TerminalAlertConfigId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.Int(),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.Int(),
                        UpdatedByName = c.String(),
                        CreatedByName = c.String(),
                        DeletedByName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.TerminalAlertConfigs", "IgnoreCardReaderNeedAttention");
            DropColumn("dbo.TerminalAlertConfigs", "IgnoreCommNeedAttention");
            DropColumn("dbo.TerminalAlertConfigs", "IgnoreBillDispenserNeedAttention");
            DropColumn("dbo.TerminalAlertConfigs", "IgnoreJournalNeedAttention");
            DropColumn("dbo.TerminalAlertConfigs", "IgnoreReceiptNeedAttention");
            DropColumn("dbo.TerminalAlertConfigs", "IgnoreCassetteNotes");
            DropColumn("dbo.TerminalAlertConfigs", "IgnoreJournalRibbon");
            DropColumn("dbo.TerminalAlertConfigs", "IgnoreJournalPaper");
            DropColumn("dbo.TerminalAlertConfigs", "IgnoreReceiptRibbon");
            DropColumn("dbo.TerminalAlertConfigs", "IgnoreReceiptPaper");
            DropColumn("dbo.TerminalAlertConfigs", "IgnoreTopDoorOpen");
            DropColumn("dbo.TerminalAlertConfigs", "IgnoreChestDoorOpen");
            CreateIndex("dbo.TerminalMessages", "TerminalAlertConfigId");
            CreateIndex("dbo.TerminalMessages", "Id_8583", unique: true, name: "TerminalMessage_Id_8583_Index");
            AddForeignKey("dbo.TerminalMessages", "TerminalAlertConfigId", "dbo.TerminalAlertConfigs", "TerminalId", cascadeDelete: true);
        }
    }
}
