namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ChangedSoftDelete : DbMigration
    {
        public override void Up()
        {
            
            AlterColumn("dbo.PartnerContacts", "Deleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Partners", "Deleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Logoes", "Deleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Users", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Deleted", c => c.Boolean());
            AlterColumn("dbo.Logoes", "Deleted", c => c.Boolean());
            AlterColumn("dbo.Partners", "Deleted", c => c.Boolean());
            AlterColumn("dbo.PartnerContacts", "Deleted", c => c.Boolean());
            
        }
    }
}
