namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTypeFilter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Filters", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Filters", "Type");
        }
    }
}
