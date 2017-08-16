namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ModelsUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cities", "Deleted", c => c.Boolean());
            AddColumn("dbo.States", "Deleted", c => c.Boolean());
            AddColumn("dbo.Countries", "Deleted", c => c.Boolean());
            AddColumn("dbo.ContactTypes", "Deleted", c => c.Boolean());
            AddColumn("dbo.PartnerContacts", "Deleted", c => c.Boolean());
            AddColumn("dbo.PartnerContacts", "DeletedBy_Id", c => c.Int());
            AddColumn("dbo.Users", "IsLocked", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "Deleted", c => c.Boolean());
            AddColumn("dbo.Partners", "Deleted", c => c.Boolean());
            AddColumn("dbo.Partners", "DeletedBy_Id", c => c.Int());
            AddColumn("dbo.Logoes", "Deleted", c => c.Boolean());
            CreateIndex("dbo.PartnerContacts", "DeletedBy_Id");
            CreateIndex("dbo.Partners", "DeletedBy_Id");
            AddForeignKey("dbo.PartnerContacts", "DeletedBy_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Partners", "DeletedBy_Id", "dbo.Users", "Id");
            DropColumn("dbo.PartnerContacts", "DeletedBy");
            DropColumn("dbo.Partners", "DeletedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Partners", "DeletedBy", c => c.String());
            AddColumn("dbo.PartnerContacts", "DeletedBy", c => c.String());
            DropForeignKey("dbo.Partners", "DeletedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PartnerContacts", "DeletedBy_Id", "dbo.Users");
            DropIndex("dbo.Partners", new[] { "DeletedBy_Id" });
            DropIndex("dbo.PartnerContacts", new[] { "DeletedBy_Id" });
            DropColumn("dbo.Logoes", "Deleted");
            DropColumn("dbo.Partners", "DeletedBy_Id");
            DropColumn("dbo.Partners", "Deleted");
            DropColumn("dbo.Users", "Deleted");
            DropColumn("dbo.Users", "IsLocked");
            DropColumn("dbo.PartnerContacts", "DeletedBy_Id");
            DropColumn("dbo.PartnerContacts", "Deleted");
            DropColumn("dbo.ContactTypes", "Deleted");
            DropColumn("dbo.Countries", "Deleted");
            DropColumn("dbo.States", "Deleted");
            DropColumn("dbo.Cities", "Deleted");
        }
    }
}
