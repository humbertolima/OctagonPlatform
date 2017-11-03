namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoadCashYAgregueAnterioresModelosALDbContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoadCashes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PreviousBalance = c.Int(nullable: false),
                        AmmountLoaded = c.Int(nullable: false),
                        CurrentBalance = c.Int(nullable: false),
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
            
            AddColumn("dbo.Terminals", "Key1", c => c.String());
            AddColumn("dbo.Terminals", "Key2", c => c.String());
            AddColumn("dbo.Terminals", "EncryptionType", c => c.String());
            AddColumn("dbo.Terminals", "DateKeyBounded", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoadCashes", "TerminalId", "dbo.Terminals");
            DropIndex("dbo.LoadCashes", new[] { "TerminalId" });
            DropColumn("dbo.Terminals", "DateKeyBounded");
            DropColumn("dbo.Terminals", "EncryptionType");
            DropColumn("dbo.Terminals", "Key2");
            DropColumn("dbo.Terminals", "Key1");
            DropTable("dbo.LoadCashes");
        }
    }
}
