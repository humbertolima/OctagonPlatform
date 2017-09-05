namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UniqueCollumnsInPartners : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Partners", "BusinessName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Partners", new[] { "BusinessName" });
        }
    }
}
