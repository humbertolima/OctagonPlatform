namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateLocationTypes : DbMigration
    {
        public override void Up()
        {
            Sql("insert into LocationTypes ([Name]) values('Airport')");
            Sql("insert into LocationTypes ([Name]) values('Bank')");
            Sql("insert into LocationTypes ([Name]) values('Bar')");
            Sql("insert into LocationTypes ([Name]) values('Gas Station')");
            Sql("insert into LocationTypes ([Name]) values('Restaurant')");
            Sql("insert into LocationTypes ([Name]) values('Store')");
            Sql("insert into LocationTypes ([Name]) values('Cafeteria')");
            Sql("insert into LocationTypes ([Name]) values('Mall')");
            Sql("insert into LocationTypes ([Name]) values('Bus Station')");
            Sql("insert into LocationTypes ([Name]) values('Hotel')");
            Sql("insert into LocationTypes ([Name]) values('School')");
            Sql("insert into LocationTypes ([Name]) values('Hospital')");
        }
        
        public override void Down()
        {
        }
    }
}
