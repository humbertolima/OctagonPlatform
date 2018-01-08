namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationReportuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reports", "UserId");
            AddForeignKey("dbo.Reports", "UserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reports", "UserId", "dbo.Users");
            DropIndex("dbo.Reports", new[] { "UserId" });
            DropColumn("dbo.Reports", "UserId");
        }
    }
}
