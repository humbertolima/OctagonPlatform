namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingInterChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InterChanges", "CalculationMethod", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InterChanges", "CalculationMethod", c => c.String());
        }
    }
}
