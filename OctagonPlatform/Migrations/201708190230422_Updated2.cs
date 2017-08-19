namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Updated2 : DbMigration
    {
        public override void Up()
        {
            Sql("insert into ContactTypes (Name) values ('Accounting')");
            Sql("insert into ContactTypes (Name) values ('Location')");
            Sql("insert into ContactTypes (Name) values ('Other')");
            Sql("insert into ContactTypes (Name) values ('Owner')");
            Sql("insert into ContactTypes (Name) values ('Technical')");
        }
        
        public override void Down()
        {
        }
    }
}
