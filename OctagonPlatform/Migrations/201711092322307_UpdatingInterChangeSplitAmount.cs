namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingInterChangeSplitAmount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InterChanges", "SplitAmount", c => c.Double(nullable: false));
            DropColumn("dbo.InterChanges", "Ammount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InterChanges", "Ammount", c => c.Double(nullable: false));
            DropColumn("dbo.InterChanges", "SplitAmount");
        }
    }
}
