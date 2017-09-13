namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateLocationTypes : DbMigration
    {
        public override void Up()
        {
            
            Sql("insert into LocationTypes ([Name]) values ('Airport')");
            Sql("insert into LocationTypes ([Name]) values ('Apartment Complex')");
            Sql("insert into LocationTypes ([Name]) values ('Bank')");
            Sql("insert into LocationTypes ([Name]) values ('Bar')");
            Sql("insert into LocationTypes ([Name]) values ('Bus Station')");
            Sql("insert into LocationTypes ([Name]) values ('Casino')");
            Sql("insert into LocationTypes ([Name]) values ('Collage Campus')");
            Sql("insert into LocationTypes ([Name]) values ('Deli')");
            Sql("insert into LocationTypes ([Name]) values ('Drug Store')");
            Sql("insert into LocationTypes ([Name]) values ('Gas Station')");
            Sql("insert into LocationTypes ([Name]) values ('Hospital')");
            Sql("insert into LocationTypes ([Name]) values ('Hotel')");
            Sql("insert into LocationTypes ([Name]) values ('Mall')");
            Sql("insert into LocationTypes ([Name]) values ('Mobil')");
            Sql("insert into LocationTypes ([Name]) values ('Night Club')");
            Sql("insert into LocationTypes ([Name]) values ('Pharmacy')");
            Sql("insert into LocationTypes ([Name]) values ('Resort')");
            Sql("insert into LocationTypes ([Name]) values ('Restaurant')");
            Sql("insert into LocationTypes ([Name]) values ('Cafeteria')");
            Sql("insert into LocationTypes ([Name]) values ('Drive Up ATM')");
            Sql("insert into LocationTypes ([Name]) values ('Money Exchange')");
            
        }
        
        public override void Down()
        {
        }
    }
}
