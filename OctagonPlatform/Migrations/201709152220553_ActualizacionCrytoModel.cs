namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualizacionCrytoModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CryptoCurrencyTransactions", "Terminal_Id", "dbo.Terminals");
            DropIndex("dbo.CryptoCurrencyTransactions", new[] { "Terminal_Id" });
            RenameColumn(table: "dbo.CryptoCurrencyTransactions", name: "Terminal_Id", newName: "TerminalId");
            AddColumn("dbo.CryptoChargeAccounts", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.CryptoChargeAccounts", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.CryptoChargeAccounts", "CreatedBy", c => c.Int());
            AddColumn("dbo.CryptoChargeAccounts", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.CryptoChargeAccounts", "DeletedBy", c => c.Int());
            AddColumn("dbo.CryptoChargeAccounts", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.CryptoChargeAccounts", "UpdatedBy", c => c.Int());
            AddColumn("dbo.CryptoChargeAccounts", "UpdatedByName", c => c.String());
            AddColumn("dbo.CryptoChargeAccounts", "CreatedByName", c => c.String());
            AddColumn("dbo.CryptoChargeAccounts", "DeletedByName", c => c.String());
            AlterColumn("dbo.CryptoCurrencyTransactions", "TerminalId", c => c.Int(nullable: false));
            CreateIndex("dbo.CryptoCurrencyTransactions", "TerminalId");
            AddForeignKey("dbo.CryptoCurrencyTransactions", "TerminalId", "dbo.Terminals", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CryptoCurrencyTransactions", "TerminalId", "dbo.Terminals");
            DropIndex("dbo.CryptoCurrencyTransactions", new[] { "TerminalId" });
            AlterColumn("dbo.CryptoCurrencyTransactions", "TerminalId", c => c.Int());
            DropColumn("dbo.CryptoChargeAccounts", "DeletedByName");
            DropColumn("dbo.CryptoChargeAccounts", "CreatedByName");
            DropColumn("dbo.CryptoChargeAccounts", "UpdatedByName");
            DropColumn("dbo.CryptoChargeAccounts", "UpdatedBy");
            DropColumn("dbo.CryptoChargeAccounts", "UpdatedAt");
            DropColumn("dbo.CryptoChargeAccounts", "DeletedBy");
            DropColumn("dbo.CryptoChargeAccounts", "DeletedAt");
            DropColumn("dbo.CryptoChargeAccounts", "CreatedBy");
            DropColumn("dbo.CryptoChargeAccounts", "CreatedAt");
            DropColumn("dbo.CryptoChargeAccounts", "Deleted");
            RenameColumn(table: "dbo.CryptoCurrencyTransactions", name: "TerminalId", newName: "Terminal_Id");
            CreateIndex("dbo.CryptoCurrencyTransactions", "Terminal_Id");
            AddForeignKey("dbo.CryptoCurrencyTransactions", "Terminal_Id", "dbo.Terminals", "Id");
        }
    }
}
