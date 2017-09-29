namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddterminalAlertConfig_TerminalMessage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TerminalAlertConfigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LowCach1 = c.Double(nullable: false),
                        LowCash2 = c.Double(),
                        LowCash3 = c.Double(),
                        InactivePeriod = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TerminalMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Id_8583 = c.String(nullable: false, maxLength: 50),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.Int(),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.Int(),
                        UpdatedByName = c.String(),
                        CreatedByName = c.String(),
                        DeletedByName = c.String(),
                        TerminalAlertConfig_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TerminalAlertConfigs", t => t.TerminalAlertConfig_Id)
                .Index(t => t.Id_8583, unique: true, name: "TerminalMessage_Id_8583_Index")
                .Index(t => t.TerminalAlertConfig_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TerminalMessages", "TerminalAlertConfig_Id", "dbo.TerminalAlertConfigs");
            DropIndex("dbo.TerminalMessages", new[] { "TerminalAlertConfig_Id" });
            DropIndex("dbo.TerminalMessages", "TerminalMessage_Id_8583_Index");
            DropTable("dbo.TerminalMessages");
            DropTable("dbo.TerminalAlertConfigs");
        }
    }
}
