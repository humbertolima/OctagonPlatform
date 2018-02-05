namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requirednamereport : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reports", "Name", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reports", "Name", c => c.String(maxLength: 200));
        }
    }
}
