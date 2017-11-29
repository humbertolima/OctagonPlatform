namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdatedUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Status", c => c.String());
        }
    }
}
