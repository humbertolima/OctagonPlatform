namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedTerminalCrypto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Terminals", "CryptoPercentChargeAmmount", c => c.Double(nullable: false));
            DropColumn("dbo.Terminals", "CryptoChargeAmmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Terminals", "CryptoChargeAmmount", c => c.Int(nullable: false));
            DropColumn("dbo.Terminals", "CryptoPercentChargeAmmount");
        }
    }
}
