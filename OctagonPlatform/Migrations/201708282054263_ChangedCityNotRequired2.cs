namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ChangedCityNotRequired2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PartnerContacts", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Partners", "CityId", "dbo.Cities");
            DropIndex("dbo.PartnerContacts", new[] { "CityId" });
            DropIndex("dbo.Partners", new[] { "CityId" });
            AlterColumn("dbo.PartnerContacts", "CityId", c => c.Int());
            AlterColumn("dbo.Partners", "CityId", c => c.Int());
            CreateIndex("dbo.PartnerContacts", "CityId");
            CreateIndex("dbo.Partners", "CityId");
            AddForeignKey("dbo.PartnerContacts", "CityId", "dbo.Cities", "Id");
            AddForeignKey("dbo.Partners", "CityId", "dbo.Cities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Partners", "CityId", "dbo.Cities");
            DropForeignKey("dbo.PartnerContacts", "CityId", "dbo.Cities");
            DropIndex("dbo.Partners", new[] { "CityId" });
            DropIndex("dbo.PartnerContacts", new[] { "CityId" });
            AlterColumn("dbo.Partners", "CityId", c => c.Int(nullable: false));
            AlterColumn("dbo.PartnerContacts", "CityId", c => c.Int(nullable: false));
            CreateIndex("dbo.Partners", "CityId");
            CreateIndex("dbo.PartnerContacts", "CityId");
            AddForeignKey("dbo.Partners", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PartnerContacts", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
    }
}
