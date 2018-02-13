namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CultureInfo2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CultureInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 10),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .Index(t => t.CountryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CultureInfo", "CountryId", "dbo.Countries");
            DropIndex("dbo.CultureInfo", new[] { "CountryId" });
            DropTable("dbo.CultureInfo");
        }
    }
}
