namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InterChange : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InterChanges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TerminalId = c.Int(nullable: false),
                        BankAccountId = c.Int(nullable: false),
                        Ammount = c.Double(nullable: false),
                        CalculationMethod = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        StopDate = c.DateTime(nullable: false),
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
                .ForeignKey("dbo.BankAccounts", t => t.BankAccountId)
                .ForeignKey("dbo.Terminals", t => t.TerminalId, cascadeDelete: false)
                .Index(t => t.TerminalId)
                .Index(t => t.BankAccountId);
            
            AddColumn("dbo.Terminals", "InterChangeAmmount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InterChanges", "TerminalId", "dbo.Terminals");
            DropForeignKey("dbo.InterChanges", "BankAccountId", "dbo.BankAccounts");
            DropIndex("dbo.InterChanges", new[] { "BankAccountId" });
            DropIndex("dbo.InterChanges", new[] { "TerminalId" });
            DropColumn("dbo.Terminals", "InterChangeAmmount");
            DropTable("dbo.InterChanges");
        }
    }
}
