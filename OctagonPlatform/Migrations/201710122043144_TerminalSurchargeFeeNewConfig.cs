namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class TerminalSurchargeFeeNewConfig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Terminals", "SurchargeAmountFee", c => c.Double(nullable: false));
            AddColumn("dbo.Terminals", "SurchargePercentageFee", c => c.Double(nullable: false));
            AddColumn("dbo.Terminals", "SurchargeType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Terminals", "SurchargeType");
            DropColumn("dbo.Terminals", "SurchargePercentageFee");
            DropColumn("dbo.Terminals", "SurchargeAmountFee");
        }
    }
}
