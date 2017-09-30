namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UserNameUniqueEliminado : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", new[] { "UserName" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Users", "UserName", unique: true);
        }
    }
}
