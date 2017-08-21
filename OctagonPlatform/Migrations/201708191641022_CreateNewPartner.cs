using System.Data.Entity.Migrations;

namespace OctagonPlatform.Migrations
{
    public partial class CreateNewPartner : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Partners (ParentId, BusinessName, Status, Address1, CountryId, StateId, CityId, Email, Mobile) values (null, 'Odyssey US', 1, '753 Shotgun Road', 231, 3930, 43879, 'test@test.com', 7862274197)");
        }
        
        public override void Down()
        {
        }
    }
}
