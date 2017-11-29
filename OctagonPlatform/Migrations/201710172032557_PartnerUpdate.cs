namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PartnerUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Partners", "Interchange", c => c.Double(nullable: false));
            DropColumn("dbo.Partners", "FreeSurchargeFeeContractConclude");
            DropColumn("dbo.Partners", "IsFreeSurchargeFeeContractFinished");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Partners", "IsFreeSurchargeFeeContractFinished", c => c.Boolean(nullable: false));
            AddColumn("dbo.Partners", "FreeSurchargeFeeContractConclude", c => c.DateTime(nullable: false));
            DropColumn("dbo.Partners", "Interchange");
        }
    }
}
