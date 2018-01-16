namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteSetAndSubgroups : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Permissions", "SetOfPermissionId", "dbo.SetOfPermissions");
            DropForeignKey("dbo.Permissions", "PermissionSubGroupId", "dbo.PermissionSubGroups");
            DropForeignKey("dbo.PermissionSubGroups", "SetOfPermissionId", "dbo.SetOfPermissions");
            DropIndex("dbo.Permissions", new[] { "SetOfPermissionId" });
            DropIndex("dbo.Permissions", new[] { "PermissionSubGroupId" });
            DropIndex("dbo.PermissionSubGroups", new[] { "SetOfPermissionId" });
            DropColumn("dbo.Permissions", "View");
            DropColumn("dbo.Permissions", "Create");
            DropColumn("dbo.Permissions", "Edit");
            DropColumn("dbo.Permissions", "Delete");
            DropColumn("dbo.Permissions", "SetOfPermissionId");
            DropColumn("dbo.Permissions", "PermissionSubGroupId");
         
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
            
            CreateTable(
                "dbo.PermissionSubGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        SetOfPermissionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Permissions", "PermissionSubGroupId", c => c.Int(nullable: false));
            AddColumn("dbo.Permissions", "SetOfPermissionId", c => c.Int(nullable: false));
            AddColumn("dbo.Permissions", "Delete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Permissions", "Edit", c => c.Boolean(nullable: false));
            AddColumn("dbo.Permissions", "Create", c => c.Boolean(nullable: false));
            AddColumn("dbo.Permissions", "View", c => c.Boolean(nullable: false));
            CreateIndex("dbo.PermissionSubGroups", "SetOfPermissionId");
            CreateIndex("dbo.Permissions", "PermissionSubGroupId");
            CreateIndex("dbo.Permissions", "SetOfPermissionId");
            AddForeignKey("dbo.Permissions", "SetOfPermissionId", "dbo.SetOfPermissions", "Id");
            AddForeignKey("dbo.Permissions", "PermissionSubGroupId", "dbo.PermissionSubGroups", "Id");
            AddForeignKey("dbo.PermissionSubGroups", "SetOfPermissionId", "dbo.SetOfPermissions", "Id");
        }
    }
}
