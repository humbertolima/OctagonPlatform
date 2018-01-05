namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPermissionSubGroup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PermissionSubGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        SetOfPermissionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SetOfPermissions", t => t.SetOfPermissionId)
                .Index(t => t.SetOfPermissionId);
            
            AddColumn("dbo.Permissions", "PermissionGroupId", c => c.Int(nullable: false));
            CreateIndex("dbo.Permissions", "PermissionGroupId");
            AddForeignKey("dbo.Permissions", "PermissionGroupId", "dbo.PermissionSubGroups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Permissions", "PermissionGroupId", "dbo.PermissionSubGroups");
            DropForeignKey("dbo.PermissionSubGroups", "SetOfPermissionId", "dbo.SetOfPermissions");
            DropIndex("dbo.PermissionSubGroups", new[] { "SetOfPermissionId" });
            DropIndex("dbo.Permissions", new[] { "PermissionGroupId" });
            DropColumn("dbo.Permissions", "PermissionGroupId");
            DropTable("dbo.PermissionSubGroups");
        }
    }
}
