namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterDocuments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "Archive", c => c.Binary());
            AlterColumn("dbo.Documents", "Category", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Documents", "Category", c => c.String(nullable: false));
            DropColumn("dbo.Documents", "Archive");
        }
    }
}
