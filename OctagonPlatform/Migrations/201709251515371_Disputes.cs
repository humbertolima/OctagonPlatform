namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Disputes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Disputes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TerminalId = c.Int(nullable: false),
                        TransactionId = c.Int(nullable: false),
                        Message = c.String(),
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
            
            AlterColumn("dbo.Terminals", "Balance", c => c.Double());
            AlterColumn("dbo.Terminals", "MinAmmountCash", c => c.Double());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Disputes", "TerminalId", "dbo.Terminals");
            DropIndex("dbo.Disputes", new[] { "TerminalId" });
            AlterColumn("dbo.Terminals", "MinAmmountCash", c => c.Int());
            AlterColumn("dbo.Terminals", "Balance", c => c.Int());
            DropTable("dbo.Disputes");
        }
    }
}
