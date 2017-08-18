namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPermissions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        SetOfPermissionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PartnerContactManagmentViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PartnerId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 16),
                        ContactTypeId = c.Int(nullable: false),
                        Address1 = c.String(nullable: false, maxLength: 50),
                        Address2 = c.String(maxLength: 50),
                        Zip = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PermissionUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.PermissionUsers", "Permission_Id", "dbo.Permissions");
            DropIndex("dbo.PermissionUsers", new[] { "User_Id" });
            DropIndex("dbo.PermissionUsers", new[] { "Permission_Id" });
            DropTable("dbo.PermissionUsers");
            DropTable("dbo.PartnerContactManagmentViewModels");
            DropTable("dbo.Permissions");
        }
    }
}
