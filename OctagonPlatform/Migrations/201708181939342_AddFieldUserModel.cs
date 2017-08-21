using System.Data.Entity.Migrations;

namespace OctagonPlatform.Migrations
{
    public partial class AddFieldUserModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Partner_Id", "dbo.Partners");
            DropIndex("dbo.Users", new[] { "Partner_Id" });
            RenameColumn(table: "dbo.Users", name: "Partner_Id", newName: "PartnerId");
            AddColumn("dbo.Users", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Users", "LastName", c => c.String(nullable: false));
            AddColumn("dbo.Users", "Phone", c => c.String(nullable: false));
            AddColumn("dbo.Users", "Status", c => c.String());
            AlterColumn("dbo.Users", "PartnerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "PartnerId");
            AddForeignKey("dbo.Users", "PartnerId", "dbo.Partners", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "PartnerId", "dbo.Partners");
            DropIndex("dbo.Users", new[] { "PartnerId" });
            AlterColumn("dbo.Users", "PartnerId", c => c.Int());
            DropColumn("dbo.Users", "Status");
            DropColumn("dbo.Users", "Phone");
            DropColumn("dbo.Users", "LastName");
            DropColumn("dbo.Users", "Name");
            RenameColumn(table: "dbo.Users", name: "PartnerId", newName: "Partner_Id");
            CreateIndex("dbo.Users", "Partner_Id");
            AddForeignKey("dbo.Users", "Partner_Id", "dbo.Partners", "Id");
        }
    }
}
