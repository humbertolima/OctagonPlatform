namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateModels : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Models ([Name], MakeId) values('Mini Atm', 1)");
            Sql("insert into Models ([Name], MakeId) values('SiriUs Atm', 1)");
        }
        
        public override void Down()
        {
        }
    }
}
