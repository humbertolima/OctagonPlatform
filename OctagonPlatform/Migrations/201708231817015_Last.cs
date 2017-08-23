namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Last : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Cities", "Deleted");
            DropColumn("dbo.States", "Deleted");
            DropColumn("dbo.Countries", "Deleted");
            DropColumn("dbo.ContactTypes", "Deleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContactTypes", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Countries", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.States", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cities", "Deleted", c => c.Boolean(nullable: false));
        }
    }
}
