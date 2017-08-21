using System.Data.Entity.Migrations;

namespace OctagonPlatform.Migrations
{
    public partial class AddedSetOfPermissions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SetOfPermissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Permissions", "SetOfPermissionId");
            AddForeignKey("dbo.Permissions", "SetOfPermissionId", "dbo.SetOfPermissions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Permissions", "SetOfPermissionId", "dbo.SetOfPermissions");
            DropIndex("dbo.Permissions", new[] { "SetOfPermissionId" });
            DropTable("dbo.SetOfPermissions");
        }
    }
}
