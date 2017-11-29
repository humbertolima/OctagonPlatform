using System.Data.Entity.Migrations;

namespace OctagonPlatform.Migrations
{
    public partial class UpdatedDataNotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PartnerContacts", "Email", c => c.String(nullable: false, maxLength: 16));
            AlterColumn("dbo.Partners", "Email", c => c.String(nullable: false, maxLength: 16));
            // DropColumn("dbo.Partners", "Mobile");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Partners", "Mobile", c => c.String(nullable: false, maxLength: 16));
            AlterColumn("dbo.Partners", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.PartnerContacts", "Email", c => c.String(nullable: false));
        }
    }
}
