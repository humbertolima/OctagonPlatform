namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UniqueCollumnsInPartnersEmail : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Partners", "Email", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Partners", new[] { "Email" });
        }
    }
}
