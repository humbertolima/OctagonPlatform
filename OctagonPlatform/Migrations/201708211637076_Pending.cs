namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Pending : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PartnerContacts", "CreatedBy", c => c.Int());
            AlterColumn("dbo.PartnerContacts", "DeletedBy", c => c.Int());
            AlterColumn("dbo.PartnerContacts", "UpdatedBy", c => c.Int());
            AlterColumn("dbo.Partners", "CreatedBy", c => c.Int());
            AlterColumn("dbo.Partners", "DeletedBy", c => c.Int());
            AlterColumn("dbo.Partners", "UpdatedBy", c => c.Int());
            AlterColumn("dbo.Users", "CreatedBy", c => c.Int());
            AlterColumn("dbo.Users", "DeletedBy", c => c.Int());
            AlterColumn("dbo.Users", "UpdatedBy", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "UpdatedBy", c => c.String());
            AlterColumn("dbo.Users", "DeletedBy", c => c.String());
            AlterColumn("dbo.Users", "CreatedBy", c => c.String());
            AlterColumn("dbo.Partners", "UpdatedBy", c => c.String());
            AlterColumn("dbo.Partners", "DeletedBy", c => c.String());
            AlterColumn("dbo.Partners", "CreatedBy", c => c.String());
            AlterColumn("dbo.PartnerContacts", "UpdatedBy", c => c.String());
            AlterColumn("dbo.PartnerContacts", "DeletedBy", c => c.String());
            AlterColumn("dbo.PartnerContacts", "CreatedBy", c => c.String());
        }
    }
}
