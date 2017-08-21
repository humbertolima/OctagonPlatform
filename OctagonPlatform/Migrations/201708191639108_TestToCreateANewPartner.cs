using System.Data.Entity.Migrations;

namespace OctagonPlatform.Migrations
{
    public partial class TestToCreateANewPartner : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Partners", new[] { "ParentId" });
            AlterColumn("dbo.Partners", "ParentId", c => c.Int());
            CreateIndex("dbo.Partners", "ParentId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Partners", new[] { "ParentId" });
            AlterColumn("dbo.Partners", "ParentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Partners", "ParentId");
        }
    }
}
