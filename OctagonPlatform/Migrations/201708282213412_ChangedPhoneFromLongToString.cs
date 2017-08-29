namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ChangedPhoneFromLongToString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PartnerContacts", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Partners", "WorkPhone", c => c.String());
            //AlterColumn("dbo.Partners", "Mobile", c => c.String());
            AlterColumn("dbo.Partners", "Fax", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Partners", "Fax", c => c.Long());
            AlterColumn("dbo.Partners", "Mobile", c => c.Long(nullable: false));
            AlterColumn("dbo.Partners", "WorkPhone", c => c.Long());
            DropColumn("dbo.PartnerContacts", "Phone");
        }
    }
}
