namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCassette : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cassettes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Denomination = c.Int(nullable: false),
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
                .ForeignKey("dbo.Terminals", t => t.TerminalId, cascadeDelete: true)
                .Index(t => t.TerminalId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cassettes", "TerminalId", "dbo.Terminals");
            DropIndex("dbo.Cassettes", new[] { "TerminalId" });
            DropTable("dbo.Cassettes");
        }
    }
}
