namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prueba : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Permissions", "SetOfPermissionId", "dbo.SetOfPermissions");
            DropIndex("dbo.Permissions", new[] { "SetOfPermissionId" });
            AddColumn("dbo.Permissions", "ParentID", c => c.Int());
            CreateIndex("dbo.Permissions", "ParentID");
            AddForeignKey("dbo.Permissions", "ParentID", "dbo.Permissions", "Id");
            DropColumn("dbo.Permissions", "View");
            DropColumn("dbo.Permissions", "Create");
            DropColumn("dbo.Permissions", "Edit");
            DropColumn("dbo.Permissions", "Delete");
            DropColumn("dbo.Permissions", "SetOfPermissionId");
            DropTable("dbo.SetOfPermissions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SetOfPermissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Permissions", "SetOfPermissionId", c => c.Int(nullable: false));
            AddColumn("dbo.Permissions", "Delete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Permissions", "Edit", c => c.Boolean(nullable: false));
            AddColumn("dbo.Permissions", "Create", c => c.Boolean(nullable: false));
            AddColumn("dbo.Permissions", "View", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Permissions", "ParentID", "dbo.Permissions");
            DropIndex("dbo.Permissions", new[] { "ParentID" });
            DropColumn("dbo.Permissions", "ParentID");
            CreateIndex("dbo.Permissions", "SetOfPermissionId");
            AddForeignKey("dbo.Permissions", "SetOfPermissionId", "dbo.SetOfPermissions", "Id");
        }
    }
}
