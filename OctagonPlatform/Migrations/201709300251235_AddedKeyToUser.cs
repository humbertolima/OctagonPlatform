namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedKeyToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Key", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Key");
        }
    }
}
