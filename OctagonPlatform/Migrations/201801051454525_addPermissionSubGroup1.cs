namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPermissionSubGroup1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Permissions", name: "PermissionGroupId", newName: "PermissionSubGroupId");
            RenameIndex(table: "dbo.Permissions", name: "IX_PermissionGroupId", newName: "IX_PermissionSubGroupId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Permissions", name: "IX_PermissionSubGroupId", newName: "IX_PermissionGroupId");
            RenameColumn(table: "dbo.Permissions", name: "PermissionSubGroupId", newName: "PermissionGroupId");
        }
    }
}
