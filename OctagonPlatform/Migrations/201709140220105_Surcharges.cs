namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Surcharges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Surcharges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TerminalId = c.Int(nullable: false),
                        BankAccountId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        StopDate = c.DateTime(nullable: false),
                        SplitAmmount = c.Double(nullable: false),
                        SettledType = c.Int(nullable: false),
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
            
            AddColumn("dbo.Terminals", "SurchargeType", c => c.Int(nullable: false));
            AddColumn("dbo.Terminals", "SurchargeAmmount", c => c.Double(nullable: false));
            AddColumn("dbo.Terminals", "Percent", c => c.Double(nullable: false));
            AddColumn("dbo.Terminals", "GreaterOrLesser", c => c.Int(nullable: false));
            AddColumn("dbo.Terminals", "SettledType", c => c.Int(nullable: false));
            AddColumn("dbo.VaultCashes", "SettledType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Surcharges", "TerminalId", "dbo.Terminals");
            DropForeignKey("dbo.Surcharges", "BankAccountId", "dbo.BankAccounts");
            DropIndex("dbo.Surcharges", new[] { "BankAccountId" });
            DropIndex("dbo.Surcharges", new[] { "TerminalId" });
            DropColumn("dbo.VaultCashes", "SettledType");
            DropColumn("dbo.Terminals", "SettledType");
            DropColumn("dbo.Terminals", "GreaterOrLesser");
            DropColumn("dbo.Terminals", "Percent");
            DropColumn("dbo.Terminals", "SurchargeAmmount");
            DropColumn("dbo.Terminals", "SurchargeType");
            DropTable("dbo.Surcharges");
        }
    }
}
