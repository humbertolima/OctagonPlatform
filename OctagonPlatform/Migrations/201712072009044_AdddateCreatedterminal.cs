namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdddateCreatedterminal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Terminals", "DateCreated", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Terminals", "DateCreated");
        }
    }
}
