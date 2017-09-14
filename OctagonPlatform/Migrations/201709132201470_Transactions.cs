namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Transactions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TerminalId = c.Int(nullable: false),
                        TransactionType = c.String(),
                        Normal = c.Boolean(nullable: false),
                        Reversal = c.Boolean(nullable: false),
                        Dcc = c.String(),
                        Pan = c.String(),
                        CardBrand = c.String(),
                        Input = c.String(),
                        CardSequence = c.String(),
                        Response = c.String(),
                        AmmountRequested = c.Int(nullable: false),
                        AmmountAproved = c.Int(nullable: false),
                        AmmountSurcharge = c.Double(nullable: false),
                        AmmountReversed = c.Double(nullable: false),
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
            DropForeignKey("dbo.Transactions", "TerminalId", "dbo.Terminals");
            DropIndex("dbo.Transactions", new[] { "TerminalId" });
            DropTable("dbo.Transactions");
        }
    }
}
