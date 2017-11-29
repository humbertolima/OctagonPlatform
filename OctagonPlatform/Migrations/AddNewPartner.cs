using System.Data.Entity.Migrations;

namespace OctagonPlatform.Migrations
{
    public partial class AddNewPartner : DbMigration
    {
        public override void Up()
        {

            Sql("SET IDENTITY_INSERT Partners ON");

            Sql("INSERT INTO Partners (Id, ParentId, [BusinessName], [Status], [Address1], [CountryId], [StateId], [CityId], [Email], [Mobile], [Deleted])" +
                " VALUES (1, 1,'Odyssey Group', 1, '753 Shotgum RD', 1, 1, 1, 'admin@xyncro.net', '7867921950', 'false')");

            Sql("SET IDENTITY_INSERT Partners OFF");
        }
        
        public override void Down()
        {
        }
    }
}
