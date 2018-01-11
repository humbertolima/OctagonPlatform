namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPermissionPArent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Permissions", "ParentID", c => c.Int());
            CreateIndex("dbo.Permissions", "ParentID");
            AddForeignKey("dbo.Permissions", "ParentID", "dbo.Permissions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Permissions", "ParentID", "dbo.Permissions");
            DropIndex("dbo.Permissions", new[] { "ParentID" });
            DropColumn("dbo.Permissions", "ParentID");
        }
    }
}
