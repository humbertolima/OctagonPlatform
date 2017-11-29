namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Event : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TerminalId = c.Int(nullable: false),
                        MessageNumber = c.String(),
                        MessageType = c.String(),
                        AtmType = c.String(),
                        Deleted = c.Boolean(nullable: false),
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
                .ForeignKey("dbo.Terminals", t => t.TerminalId, cascadeDelete: false)
                .Index(t => t.TerminalId);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nota = c.String(nullable: false),
                        TerminalId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
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
                .ForeignKey("dbo.Terminals", t => t.TerminalId, cascadeDelete: false)
                .Index(t => t.TerminalId);
            
            AddColumn("dbo.Terminals", "BankAccountId", c => c.Int(nullable: false));
            CreateIndex("dbo.Terminals", "BankAccountId");
            AddForeignKey("dbo.Terminals", "BankAccountId", "dbo.BankAccounts", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notes", "TerminalId", "dbo.Terminals");
            DropForeignKey("dbo.Events", "TerminalId", "dbo.Terminals");
            DropForeignKey("dbo.Terminals", "BankAccountId", "dbo.BankAccounts");
            DropIndex("dbo.Notes", new[] { "TerminalId" });
            DropIndex("dbo.Events", new[] { "TerminalId" });
            DropIndex("dbo.Terminals", new[] { "BankAccountId" });
            DropColumn("dbo.Terminals", "BankAccountId");
            DropTable("dbo.Notes");
            DropTable("dbo.Events");
        }
    }
}
