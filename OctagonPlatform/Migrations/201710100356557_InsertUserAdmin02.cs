namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InsertUserAdmin02 : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Users ON");

            Sql("INSERT INTO Users (Id, [UserName], [Password], [PartnerId], [IsLocked], [Email], [Name], [LastName], [Phone], [Status], [Deleted],[Key])" +
                " VALUES (1, 'admin02', '0C-CD-1F-A5-42-CC-6C-67-CB-7C-3C-B8-CC-86-51-7E', 1, 'false', 'admin@xyncro.net', 'Administrator', 'Admin','7867921520', 1, 'false','t4RcY6PQUBjqO3R24jEYK8d7ZCNS9fuU4QooX1nDSBFJPuKTkNUdiRVv2Uoxu7SPhAw8QDgc7bgiDFsE34JxxqLo54wdO1jVV1Bp')");

            Sql("SET IDENTITY_INSERT Users OFF");
        }
        
        public override void Down()
        {
        }
    }
}
