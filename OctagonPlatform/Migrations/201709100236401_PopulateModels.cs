namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateModels : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Models ([Name], MakeId) values ('Mini ATM', 1)");
            Sql("insert into Models ([Name], MakeId) values ('Siri Us ATM', 1)");
        }
        
        public override void Down()
        {
        }
    }
}
