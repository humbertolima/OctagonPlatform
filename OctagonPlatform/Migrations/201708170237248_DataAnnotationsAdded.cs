using System.Data.Entity.Migrations;

namespace OctagonPlatform.Migrations
{
    public partial class DataAnnotationsAdded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Partners", "LogoId", "dbo.Logoes");
            DropIndex("dbo.Partners", new[] { "LogoId" });
            AddColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 16));
            AlterColumn("dbo.Cities", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.States", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Countries", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.ContactTypes", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.PartnerContacts", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.PartnerContacts", "LastName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.PartnerContacts", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.PartnerContacts", "Address1", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.PartnerContacts", "Address2", c => c.String(maxLength: 50));
            AlterColumn("dbo.PartnerContacts", "CreatedBy", c => c.Int());
            AlterColumn("dbo.PartnerContacts", "DeletedBy", c => c.Int());
            AlterColumn("dbo.PartnerContacts", "UpdatedBy", c => c.Int());
            AlterColumn("dbo.Partners", "BusinessName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Partners", "Address1", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Partners", "Address2", c => c.String(maxLength: 50));
            AlterColumn("dbo.Partners", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Partners", "Mobile", c => c.String(nullable: false, maxLength: 16));
            AlterColumn("dbo.Partners", "LogoId", c => c.Int());
            AlterColumn("dbo.Partners", "CreatedBy", c => c.Int());
            AlterColumn("dbo.Partners", "DeletedBy", c => c.Int());
            AlterColumn("dbo.Partners", "UpdatedBy", c => c.Int());
            AlterColumn("dbo.Logoes", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Logoes", "Picture", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 16));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Users", "IsLocked", c => c.Boolean());
            AlterColumn("dbo.Users", "CreatedBy", c => c.Int());
            AlterColumn("dbo.Users", "DeletedBy", c => c.Int());
            AlterColumn("dbo.Users", "UpdatedBy", c => c.Int());
            CreateIndex("dbo.Partners", "LogoId");
            AddForeignKey("dbo.Partners", "LogoId", "dbo.Logoes", "Id");
            DropColumn("dbo.PartnerContacts", "BusinessName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PartnerContacts", "BusinessName", c => c.String());
            DropForeignKey("dbo.Partners", "LogoId", "dbo.Logoes");
            DropIndex("dbo.Partners", new[] { "LogoId" });
            AlterColumn("dbo.Users", "UpdatedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "DeletedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "CreatedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "IsLocked", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "UserName", c => c.String());
            AlterColumn("dbo.Logoes", "Picture", c => c.String());
            AlterColumn("dbo.Logoes", "Name", c => c.String());
            AlterColumn("dbo.Partners", "UpdatedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.Partners", "DeletedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.Partners", "CreatedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.Partners", "LogoId", c => c.Int(nullable: false));
            AlterColumn("dbo.Partners", "Mobile", c => c.String());
            AlterColumn("dbo.Partners", "Email", c => c.String());
            AlterColumn("dbo.Partners", "Address2", c => c.String());
            AlterColumn("dbo.Partners", "Address1", c => c.String());
            AlterColumn("dbo.Partners", "BusinessName", c => c.String());
            AlterColumn("dbo.PartnerContacts", "UpdatedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.PartnerContacts", "DeletedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.PartnerContacts", "CreatedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.PartnerContacts", "Address2", c => c.String());
            AlterColumn("dbo.PartnerContacts", "Address1", c => c.String());
            AlterColumn("dbo.PartnerContacts", "Email", c => c.String());
            AlterColumn("dbo.PartnerContacts", "LastName", c => c.String());
            AlterColumn("dbo.PartnerContacts", "Name", c => c.String());
            AlterColumn("dbo.ContactTypes", "Name", c => c.String());
            AlterColumn("dbo.Countries", "Name", c => c.String());
            AlterColumn("dbo.States", "Name", c => c.String());
            AlterColumn("dbo.Cities", "Name", c => c.String());
            DropColumn("dbo.Users", "Email");
            CreateIndex("dbo.Partners", "LogoId");
            AddForeignKey("dbo.Partners", "LogoId", "dbo.Logoes", "Id", cascadeDelete: false);
        }
    }
}
