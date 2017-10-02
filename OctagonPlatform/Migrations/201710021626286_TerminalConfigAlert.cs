namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TerminalConfigAlert : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TerminalAlertConfigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TerminalId = c.Int(nullable: false),
                        LowCach1 = c.Double(nullable: false),
                        LowCash2 = c.Double(),
                        LowCash3 = c.Double(),
                        InactivePeriod = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Terminals", t => t.TerminalId, cascadeDelete: true)
                .Index(t => t.TerminalId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TerminalAlertConfigs", t => t.TerminalAlertConfigId, cascadeDelete: true)
                .Index(t => t.Id_8583, unique: true, name: "TerminalMessage_Id_8583_Index")
                .Index(t => t.TerminalAlertConfigId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TerminalAlertConfigs", "TerminalId", "dbo.Terminals");
            DropForeignKey("dbo.TerminalMessages", "TerminalAlertConfigId", "dbo.TerminalAlertConfigs");
            DropIndex("dbo.TerminalMessages", new[] { "TerminalAlertConfigId" });
            DropIndex("dbo.TerminalMessages", "TerminalMessage_Id_8583_Index");
            DropIndex("dbo.TerminalAlertConfigs", new[] { "TerminalId" });
            DropTable("dbo.TerminalMessages");
            DropTable("dbo.TerminalAlertConfigs");
        }
    }
}
