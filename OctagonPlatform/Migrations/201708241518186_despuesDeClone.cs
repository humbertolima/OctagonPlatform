namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class despuesDeClone : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Partner_Id", "dbo.Partners");
            DropIndex("dbo.Users", new[] { "Partner_Id" });
            RenameColumn(table: "dbo.Users", name: "Partner_Id", newName: "PartnerId");
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        SetOfPermissionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SetOfPermissions", t => t.SetOfPermissionId, cascadeDelete: true)
                .Index(t => t.SetOfPermissionId);
            
            CreateTable(
                "dbo.SetOfPermissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PermissionUsers",
                c => new
                    {
                        Permission_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Permission_Id, t.User_Id })
                .ForeignKey("dbo.Permissions", t => t.Permission_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Permission_Id)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.Partners", "WorkPhone", c => c.Long());
            AddColumn("dbo.Partners", "Mobile", c => c.Long(nullable: false));
            AddColumn("dbo.Partners", "Fax", c => c.Long());
            AddColumn("dbo.Users", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Users", "LastName", c => c.String(nullable: false));
            AddColumn("dbo.Users", "Phone", c => c.String(nullable: false));
            AddColumn("dbo.Users", "Status", c => c.Int(nullable: false));
            AlterColumn("dbo.PartnerContacts", "Deleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Partners", "Deleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Logoes", "Deleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Users", "IsLocked", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Users", "Deleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Users", "PartnerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "PartnerId");
            AddForeignKey("dbo.Users", "PartnerId", "dbo.Partners", "Id", cascadeDelete: true);
            DropColumn("dbo.Cities", "Deleted");
            DropColumn("dbo.States", "Deleted");
            DropColumn("dbo.Countries", "Deleted");
            DropColumn("dbo.ContactTypes", "Deleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContactTypes", "Deleted", c => c.Boolean());
            AddColumn("dbo.Countries", "Deleted", c => c.Boolean());
            AddColumn("dbo.States", "Deleted", c => c.Boolean());
            AddColumn("dbo.Cities", "Deleted", c => c.Boolean());
            DropForeignKey("dbo.Users", "PartnerId", "dbo.Partners");
            DropForeignKey("dbo.Permissions", "SetOfPermissionId", "dbo.SetOfPermissions");
            DropForeignKey("dbo.PermissionUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.PermissionUsers", "Permission_Id", "dbo.Permissions");
            DropIndex("dbo.PermissionUsers", new[] { "User_Id" });
            DropIndex("dbo.PermissionUsers", new[] { "Permission_Id" });
            DropIndex("dbo.Permissions", new[] { "SetOfPermissionId" });
            DropIndex("dbo.Users", new[] { "PartnerId" });
            AlterColumn("dbo.Users", "PartnerId", c => c.Int());
            AlterColumn("dbo.Users", "Deleted", c => c.Boolean());
            AlterColumn("dbo.Users", "IsLocked", c => c.Boolean());
            AlterColumn("dbo.Logoes", "Deleted", c => c.Boolean());
            AlterColumn("dbo.Partners", "Deleted", c => c.Boolean());
            AlterColumn("dbo.PartnerContacts", "Deleted", c => c.Boolean());
            DropColumn("dbo.Users", "Status");
            DropColumn("dbo.Users", "Phone");
            DropColumn("dbo.Users", "LastName");
            DropColumn("dbo.Users", "Name");
            DropColumn("dbo.Partners", "Fax");
            DropColumn("dbo.Partners", "Mobile");
            DropColumn("dbo.Partners", "WorkPhone");
            DropTable("dbo.PermissionUsers");
            DropTable("dbo.SetOfPermissions");
            DropTable("dbo.Permissions");
            RenameColumn(table: "dbo.Users", name: "PartnerId", newName: "Partner_Id");
            CreateIndex("dbo.Users", "Partner_Id");
            AddForeignKey("dbo.Users", "Partner_Id", "dbo.Partners", "Id");
        }
    }
}
