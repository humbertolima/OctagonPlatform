namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ChangeIsLocked : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "IsLocked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "IsLocked", c => c.Boolean());
        }
    }
}
