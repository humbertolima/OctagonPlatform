namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addintpartner : DbMigration
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
