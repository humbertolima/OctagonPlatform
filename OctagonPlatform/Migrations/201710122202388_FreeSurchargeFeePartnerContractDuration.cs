namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FreeSurchargeFeePartnerContractDuration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Partners", "FreeSurchargeFeeContractConclude", c => c.DateTime(nullable: false));
            AddColumn("dbo.Partners", "IsFreeSurchargeFeeContractFinished", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Partners", "IsFreeSurchargeFeeContractFinished");
            DropColumn("dbo.Partners", "FreeSurchargeFeeContractConclude");
        }
    }
}
