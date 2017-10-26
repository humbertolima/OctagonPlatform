namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableTerminalUpdatedDeletedSomeFields : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Terminals", "InactiveHours");
            DropColumn("dbo.Terminals", "LastCommunicationDate");
            DropColumn("dbo.Terminals", "LastTransactionDate");
            DropColumn("dbo.Terminals", "MinAmmountCash");
            DropColumn("dbo.Terminals", "Offline");
            DropColumn("dbo.Terminals", "RemainingSurchargeAmountFee");
            DropColumn("dbo.Terminals", "RemainingSurchargePercentFee");
            DropColumn("dbo.Terminals", "RemainingInterchange");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Terminals", "RemainingInterchange", c => c.Double(nullable: false));
            AddColumn("dbo.Terminals", "RemainingSurchargePercentFee", c => c.Double(nullable: false));
            AddColumn("dbo.Terminals", "RemainingSurchargeAmountFee", c => c.Double(nullable: false));
            AddColumn("dbo.Terminals", "Offline", c => c.Boolean(nullable: false));
            AddColumn("dbo.Terminals", "MinAmmountCash", c => c.Double());
            AddColumn("dbo.Terminals", "LastTransactionDate", c => c.DateTime());
            AddColumn("dbo.Terminals", "LastCommunicationDate", c => c.DateTime());
            AddColumn("dbo.Terminals", "InactiveHours", c => c.Int(nullable: false));
        }
    }
}
