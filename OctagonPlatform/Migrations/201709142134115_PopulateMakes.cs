namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateMakes : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Makes ([Name]) values ('Puloon')");
            Sql("insert into Makes ([Name]) values ('N. Hyosung')");
        }
        
        public override void Down()
        {
        }
    }
}
