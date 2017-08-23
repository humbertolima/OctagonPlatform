namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InsertUserAdmin : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Users (PartnerId, [UserName],  [Password], [Email]) VALUES (8,'admin','admin','admin@xyncro.net')");
        }
        
        public override void Down()
        {

        }
    }
}
