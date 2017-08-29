namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InsertUserAdmin : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Users ON");

            Sql("INSERT INTO Users (Id, [UserName], [Password], [PartnerId], [IsLocked], [Email], [Name], [LastName], [Phone], [Status], [Deleted])" +
                " VALUES (1, 'admin01', 'admin01', 1, 'false', 'admin@xyncro.net', 'Administrator', 'Admin','7867921520', 1, 'false')");

            Sql("SET IDENTITY_INSERT Users OFF");
        }
        
        public override void Down()
        {

        }
    }
}
