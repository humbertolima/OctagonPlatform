namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Contracts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TerminationDate = c.DateTime(),
                        AutoRenew = c.Boolean(nullable: false),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contracts", "TerminalId", "dbo.Terminals");
            DropIndex("dbo.Contracts", new[] { "TerminalId" });
            DropTable("dbo.Contracts");
        }
    }
}
