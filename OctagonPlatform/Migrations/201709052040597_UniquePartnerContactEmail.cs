namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniquePartnerContactEmail : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.PartnerContacts", "Email", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.PartnerContacts", new[] { "Email" });
        }
    }
}
