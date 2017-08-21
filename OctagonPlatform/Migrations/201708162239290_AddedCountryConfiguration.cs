using System.Data.Entity.Migrations;

namespace OctagonPlatform.Migrations
{
    public partial class AddedCountryConfiguration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PartnerContacts", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PartnerContacts", "DeletedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Partners", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Partners", "DeletedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Partners", "UpdatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PartnerContacts", "UpdatedBy_Id", "dbo.Users");
            DropIndex("dbo.PartnerContacts", new[] { "CreatedBy_Id" });
            DropIndex("dbo.PartnerContacts", new[] { "DeletedBy_Id" });
            DropIndex("dbo.PartnerContacts", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.Partners", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Partners", new[] { "DeletedBy_Id" });
            DropIndex("dbo.Partners", new[] { "UpdatedBy_Id" });
            AddColumn("dbo.PartnerContacts", "CreatedBy", c => c.Int(nullable: false));
            AddColumn("dbo.PartnerContacts", "DeletedBy", c => c.Int(nullable: false));
            AddColumn("dbo.PartnerContacts", "UpdatedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.Users", "CreatedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.Users", "DeletedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.Users", "UpdatedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Partners", "CreatedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Partners", "DeletedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Partners", "UpdatedBy", c => c.Int(nullable: false));
            DropColumn("dbo.PartnerContacts", "CreatedBy_Id");
            DropColumn("dbo.PartnerContacts", "DeletedBy_Id");
            DropColumn("dbo.PartnerContacts", "UpdatedBy_Id");
            DropColumn("dbo.Partners", "CreatedBy_Id");
            DropColumn("dbo.Partners", "DeletedBy_Id");
            DropColumn("dbo.Partners", "UpdatedBy_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Partners", "UpdatedBy_Id", c => c.Int());
            AddColumn("dbo.Partners", "DeletedBy_Id", c => c.Int());
            AddColumn("dbo.Partners", "CreatedBy_Id", c => c.Int());
            AddColumn("dbo.PartnerContacts", "UpdatedBy_Id", c => c.Int());
            AddColumn("dbo.PartnerContacts", "DeletedBy_Id", c => c.Int());
            AddColumn("dbo.PartnerContacts", "CreatedBy_Id", c => c.Int());
            DropColumn("dbo.Partners", "UpdatedBy");
            DropColumn("dbo.Partners", "DeletedBy");
            DropColumn("dbo.Partners", "CreatedBy");
            DropColumn("dbo.Users", "UpdatedBy");
            DropColumn("dbo.Users", "UpdatedAt");
            DropColumn("dbo.Users", "DeletedBy");
            DropColumn("dbo.Users", "DeletedAt");
            DropColumn("dbo.Users", "CreatedBy");
            DropColumn("dbo.Users", "CreatedAt");
            DropColumn("dbo.PartnerContacts", "UpdatedBy");
            DropColumn("dbo.PartnerContacts", "DeletedBy");
            DropColumn("dbo.PartnerContacts", "CreatedBy");
            CreateIndex("dbo.Partners", "UpdatedBy_Id");
            CreateIndex("dbo.Partners", "DeletedBy_Id");
            CreateIndex("dbo.Partners", "CreatedBy_Id");
            CreateIndex("dbo.PartnerContacts", "UpdatedBy_Id");
            CreateIndex("dbo.PartnerContacts", "DeletedBy_Id");
            CreateIndex("dbo.PartnerContacts", "CreatedBy_Id");
            AddForeignKey("dbo.PartnerContacts", "UpdatedBy_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Partners", "UpdatedBy_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Partners", "DeletedBy_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Partners", "CreatedBy_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.PartnerContacts", "DeletedBy_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.PartnerContacts", "CreatedBy_Id", "dbo.Users", "Id");
        }
    }
}
