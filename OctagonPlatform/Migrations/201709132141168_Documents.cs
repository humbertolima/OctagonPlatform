namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Documents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Category = c.String(nullable: false),
                        Privacy = c.Boolean(nullable: false),
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
            DropForeignKey("dbo.Documents", "TerminalId", "dbo.Terminals");
            DropIndex("dbo.Documents", new[] { "TerminalId" });
            DropTable("dbo.Documents");
        }
    }
}
