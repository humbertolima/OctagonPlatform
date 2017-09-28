namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArregladoAmmountPorAmountErrorGramatical : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Terminals", "SurchargeAmount", c => c.Double(nullable: false));
            AddColumn("dbo.Terminals", "InterChangeAmount", c => c.Double(nullable: false));
            AddColumn("dbo.Terminals", "CryptoPercentChargeAmount", c => c.Double(nullable: false));
            DropColumn("dbo.Terminals", "SurchargeAmmount");
            DropColumn("dbo.Terminals", "InterChangeAmmount");
            DropColumn("dbo.Terminals", "CryptoPercentChargeAmmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Terminals", "CryptoPercentChargeAmmount", c => c.Double(nullable: false));
            AddColumn("dbo.Terminals", "InterChangeAmmount", c => c.Double(nullable: false));
            AddColumn("dbo.Terminals", "SurchargeAmmount", c => c.Double(nullable: false));
            DropColumn("dbo.Terminals", "CryptoPercentChargeAmount");
            DropColumn("dbo.Terminals", "InterChangeAmount");
            DropColumn("dbo.Terminals", "SurchargeAmount");
        }
    }
}
