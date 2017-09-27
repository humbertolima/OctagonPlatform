namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBoolBindedKeys : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Terminals", "BindedKeys", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Terminals", "BindedKeys");
        }
    }
}
