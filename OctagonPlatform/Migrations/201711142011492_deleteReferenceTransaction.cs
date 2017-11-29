namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteReferenceTransaction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Disputes", "TransacNo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Disputes", "TransacNo");
        }
    }
}
