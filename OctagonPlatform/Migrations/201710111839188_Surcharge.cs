namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Surcharge : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Surcharges", "SplitAmount", c => c.Double(nullable: false));
            AddColumn("dbo.Surcharges", "SplitAmountPercent", c => c.Double(nullable: false));
            DropColumn("dbo.Surcharges", "SplitAmmount");
            DropColumn("dbo.Surcharges", "SplitAmmountPercent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Surcharges", "SplitAmmountPercent", c => c.Double(nullable: false));
            AddColumn("dbo.Surcharges", "SplitAmmount", c => c.Double(nullable: false));
            DropColumn("dbo.Surcharges", "SplitAmountPercent");
            DropColumn("dbo.Surcharges", "SplitAmount");
        }
    }
}
