namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SurchageTypeArreglarNombre : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Terminals", "GreaterOrLesser");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Terminals", "GreaterOrLesser", c => c.Int(nullable: false));
        }
    }
}
