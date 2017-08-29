namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ChangedPhoneFromLongToString2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Partners", "Mobile", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Partners", "Mobile", c => c.String());
        }
    }
}
