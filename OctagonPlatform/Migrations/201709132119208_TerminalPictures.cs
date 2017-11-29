namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TerminalPictures : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TerminalPictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(nullable: false),
                        TerminalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Terminals", t => t.TerminalId, cascadeDelete: false)
                .Index(t => t.TerminalId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TerminalPictures", "TerminalId", "dbo.Terminals");
            DropIndex("dbo.TerminalPictures", new[] { "TerminalId" });
            DropTable("dbo.TerminalPictures");
        }
    }
}
