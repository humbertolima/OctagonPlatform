namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFieldToDispute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Disputes", "Viewed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Disputes", "MessageNumber", c => c.String());
            AddColumn("dbo.Disputes", "NetworkAdjustmentId", c => c.String());
            AddColumn("dbo.Disputes", "Network", c => c.String());
            AddColumn("dbo.Disputes", "DisputeType", c => c.Int(nullable: false));
            AddColumn("dbo.Disputes", "SecuenceNumber", c => c.String());
            AddColumn("dbo.Disputes", "AmountRequested", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Disputes", "DisputedAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Disputes", "LastDayToRepresent", c => c.DateTime(nullable: false));
            DropColumn("dbo.Disputes", "Message");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Disputes", "Message", c => c.String());
            DropColumn("dbo.Disputes", "LastDayToRepresent");
            DropColumn("dbo.Disputes", "DisputedAmount");
            DropColumn("dbo.Disputes", "AmountRequested");
            DropColumn("dbo.Disputes", "SecuenceNumber");
            DropColumn("dbo.Disputes", "DisputeType");
            DropColumn("dbo.Disputes", "Network");
            DropColumn("dbo.Disputes", "NetworkAdjustmentId");
            DropColumn("dbo.Disputes", "MessageNumber");
            DropColumn("dbo.Disputes", "Viewed");
        }
    }
}
