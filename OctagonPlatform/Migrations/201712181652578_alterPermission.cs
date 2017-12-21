namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterPermission : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Permissions", "View", c => c.Boolean(nullable: false));
            AddColumn("dbo.Permissions", "Create", c => c.Boolean(nullable: false));
            AddColumn("dbo.Permissions", "Edit", c => c.Boolean(nullable: false));
            AddColumn("dbo.Permissions", "Delete", c => c.Boolean(nullable: false));
            DropColumn("dbo.Permissions", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Permissions", "Type", c => c.String(nullable: false));
            DropColumn("dbo.Permissions", "Delete");
            DropColumn("dbo.Permissions", "Edit");
            DropColumn("dbo.Permissions", "Create");
            DropColumn("dbo.Permissions", "View");
        }
    }
}
