namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class TerminalAddedCryptoTransactions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CryptoChargeAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TerminalId = c.Int(nullable: false),
                        BankAccountId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        StopDate = c.DateTime(nullable: false),
                        SplitAmmount = c.Double(nullable: false),
                        SettledType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccountId)
                .ForeignKey("dbo.Terminals", t => t.TerminalId)
                .Index(t => t.TerminalId)
                .Index(t => t.BankAccountId);
            
            CreateTable(
                "dbo.CryptoCurrencyTransactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
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
                        Terminal_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Terminals", t => t.Terminal_Id)
                .Index(t => t.Terminal_Id);
            
            AddColumn("dbo.Terminals", "SurchargeByPercent", c => c.Double(nullable: false));
            AddColumn("dbo.Terminals", "CryptoChargeAmmount", c => c.Int(nullable: false));
            DropColumn("dbo.Terminals", "Percent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Terminals", "Percent", c => c.Double(nullable: false));
            DropForeignKey("dbo.CryptoCurrencyTransactions", "Terminal_Id", "dbo.Terminals");
            DropForeignKey("dbo.CryptoChargeAccounts", "TerminalId", "dbo.Terminals");
            DropForeignKey("dbo.CryptoChargeAccounts", "BankAccountId", "dbo.BankAccounts");
            DropIndex("dbo.CryptoCurrencyTransactions", new[] { "Terminal_Id" });
            DropIndex("dbo.CryptoChargeAccounts", new[] { "BankAccountId" });
            DropIndex("dbo.CryptoChargeAccounts", new[] { "TerminalId" });
            DropColumn("dbo.Terminals", "CryptoChargeAmmount");
            DropColumn("dbo.Terminals", "SurchargeByPercent");
            DropTable("dbo.CryptoCurrencyTransactions");
            DropTable("dbo.CryptoChargeAccounts");
        }
    }
}
