namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterFieldDisputeRepresent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DisputeRepresents", "Image", c => c.Binary());
            DropColumn("dbo.DisputeRepresents", "AttachData");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DisputeRepresents", "AttachData", c => c.Binary());
            DropColumn("dbo.DisputeRepresents", "Image");
        }
    }
}
