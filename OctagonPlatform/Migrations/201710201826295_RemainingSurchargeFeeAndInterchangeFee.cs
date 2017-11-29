namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemainingSurchargeFeeAndInterchangeFee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Terminals", "RemainingSurchargeAmountFee", c => c.Double(nullable: false));
            AddColumn("dbo.Terminals", "RemainingSurchargePercentFee", c => c.Double(nullable: false));
            AddColumn("dbo.Terminals", "RemainingInterchange", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Terminals", "RemainingInterchange");
            DropColumn("dbo.Terminals", "RemainingSurchargePercentFee");
            DropColumn("dbo.Terminals", "RemainingSurchargeAmountFee");
        }
    }
}
