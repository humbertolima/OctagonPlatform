using System.Data.Entity.Migrations;

namespace OctagonPlatform.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PartnerContacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PartnerId = c.Int(nullable: false),
                        BusinessName = c.String(),
                        Name = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        ContactTypeId = c.Int(nullable: false),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        Zip = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy_Id = c.Int(),
                        UpdatedBy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .ForeignKey("dbo.ContactTypes", t => t.ContactTypeId)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Partners", t => t.PartnerId, cascadeDelete: true)
                .ForeignKey("dbo.States", t => t.StateId)
                .ForeignKey("dbo.Users", t => t.UpdatedBy_Id)
                .Index(t => t.PartnerId)
                .Index(t => t.ContactTypeId)
                .Index(t => t.CountryId)
                .Index(t => t.StateId)
                .Index(t => t.CityId)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.UpdatedBy_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Partner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Partners", t => t.Partner_Id)
                .Index(t => t.Partner_Id);
            
            CreateTable(
                "dbo.Partners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(nullable: false),
                        BusinessName = c.String(),
                        Status = c.Int(nullable: false),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        CountryId = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        Email = c.String(),
                        Mobile = c.String(),
                        WebSite = c.String(),
                        LogoId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy_Id = c.Int(),
                        UpdatedBy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Logoes", t => t.LogoId)
                .ForeignKey("dbo.Partners", t => t.ParentId)
                .ForeignKey("dbo.States", t => t.StateId)
                .ForeignKey("dbo.Users", t => t.UpdatedBy_Id)
                .Index(t => t.ParentId)
                .Index(t => t.CountryId)
                .Index(t => t.StateId)
                .Index(t => t.CityId)
                .Index(t => t.LogoId)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.UpdatedBy_Id);
            
            CreateTable(
                "dbo.Logoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Picture = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PartnerContacts", "UpdatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PartnerContacts", "StateId", "dbo.States");
            DropForeignKey("dbo.Users", "Partner_Id", "dbo.Partners");
            DropForeignKey("dbo.Partners", "UpdatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Partners", "StateId", "dbo.States");
            DropForeignKey("dbo.PartnerContacts", "PartnerId", "dbo.Partners");
            DropForeignKey("dbo.Partners", "ParentId", "dbo.Partners");
            DropForeignKey("dbo.Partners", "LogoId", "dbo.Logoes");
            DropForeignKey("dbo.Partners", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Partners", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Partners", "CityId", "dbo.Cities");
            DropForeignKey("dbo.PartnerContacts", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PartnerContacts", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.PartnerContacts", "ContactTypeId", "dbo.ContactTypes");
            DropForeignKey("dbo.PartnerContacts", "CityId", "dbo.Cities");
            DropForeignKey("dbo.States", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Cities", "StateId", "dbo.States");
            DropIndex("dbo.Partners", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.Partners", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Partners", new[] { "LogoId" });
            DropIndex("dbo.Partners", new[] { "CityId" });
            DropIndex("dbo.Partners", new[] { "StateId" });
            DropIndex("dbo.Partners", new[] { "CountryId" });
            DropIndex("dbo.Partners", new[] { "ParentId" });
            DropIndex("dbo.Users", new[] { "Partner_Id" });
            DropIndex("dbo.PartnerContacts", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.PartnerContacts", new[] { "CreatedBy_Id" });
            DropIndex("dbo.PartnerContacts", new[] { "CityId" });
            DropIndex("dbo.PartnerContacts", new[] { "StateId" });
            DropIndex("dbo.PartnerContacts", new[] { "CountryId" });
            DropIndex("dbo.PartnerContacts", new[] { "ContactTypeId" });
            DropIndex("dbo.PartnerContacts", new[] { "PartnerId" });
            DropIndex("dbo.States", new[] { "CountryId" });
            DropIndex("dbo.Cities", new[] { "StateId" });
            DropTable("dbo.Logoes");
            DropTable("dbo.Partners");
            DropTable("dbo.Users");
            DropTable("dbo.PartnerContacts");
            DropTable("dbo.ContactTypes");
            DropTable("dbo.Countries");
            DropTable("dbo.States");
            DropTable("dbo.Cities");
        }
    }
}
