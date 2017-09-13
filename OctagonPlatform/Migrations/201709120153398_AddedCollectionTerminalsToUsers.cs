namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedCollectionTerminalsToUsers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Terminal_Id", "dbo.Terminals");
            DropIndex("dbo.Users", new[] { "Terminal_Id" });
            CreateTable(
                "dbo.UserTerminals",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Terminal_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Terminal_Id })
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Terminals", t => t.Terminal_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Terminal_Id);
            
            DropColumn("dbo.Users", "Terminal_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Terminal_Id", c => c.Int());
            DropForeignKey("dbo.UserTerminals", "Terminal_Id", "dbo.Terminals");
            DropForeignKey("dbo.UserTerminals", "User_Id", "dbo.Users");
            DropIndex("dbo.UserTerminals", new[] { "Terminal_Id" });
            DropIndex("dbo.UserTerminals", new[] { "User_Id" });
            DropTable("dbo.UserTerminals");
            CreateIndex("dbo.Users", "Terminal_Id");
            AddForeignKey("dbo.Users", "Terminal_Id", "dbo.Terminals", "Id");
        }
    }
}
