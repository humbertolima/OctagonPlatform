namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EliminarRequiredCampoSurchargeByPercentInTerminal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Surcharges", "SplitAmmountPercent", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Surcharges", "SplitAmmountPercent");
        }
    }
}
