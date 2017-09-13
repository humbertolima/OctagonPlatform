namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class EmailMoreThat16 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PartnerContacts", new[] { "Email" });
            DropIndex("dbo.Partners", new[] { "Email" });
            AlterColumn("dbo.PartnerContacts", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Partners", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.PartnerContacts", "Email", unique: true);
            CreateIndex("dbo.Partners", "Email", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Partners", new[] { "Email" });
            DropIndex("dbo.PartnerContacts", new[] { "Email" });
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 16));
            AlterColumn("dbo.Partners", "Email", c => c.String(nullable: false, maxLength: 16));
            AlterColumn("dbo.PartnerContacts", "Email", c => c.String(nullable: false, maxLength: 16));
            CreateIndex("dbo.Partners", "Email", unique: true);
            CreateIndex("dbo.PartnerContacts", "Email", unique: true);
        }
    }
}
