namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTerminalContact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Terminal_Id", c => c.Int());
            CreateIndex("dbo.Users", "Terminal_Id");
            AddForeignKey("dbo.Users", "Terminal_Id", "dbo.Terminals", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Terminal_Id", "dbo.Terminals");
            DropIndex("dbo.Users", new[] { "Terminal_Id" });
            DropColumn("dbo.Users", "Terminal_Id");
        }
    }
}
