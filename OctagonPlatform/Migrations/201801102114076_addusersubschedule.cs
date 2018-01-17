namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addusersubschedule : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subscriptions", "PartnerId", "dbo.Partners");
            DropForeignKey("dbo.Schedules", "PartnerId", "dbo.Partners");
            DropIndex("dbo.Subscriptions", new[] { "PartnerId" });
            DropIndex("dbo.Schedules", new[] { "PartnerId" });
            AddColumn("dbo.Subscriptions", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Schedules", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Subscriptions", "UserId");
            CreateIndex("dbo.Schedules", "UserId");
            AddForeignKey("dbo.Schedules", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.Subscriptions", "UserId", "dbo.Users", "Id");
            DropColumn("dbo.Subscriptions", "PartnerId");
            DropColumn("dbo.Schedules", "PartnerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Schedules", "PartnerId", c => c.Int(nullable: false));
            AddColumn("dbo.Subscriptions", "PartnerId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Subscriptions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Schedules", "UserId", "dbo.Users");
            DropIndex("dbo.Schedules", new[] { "UserId" });
            DropIndex("dbo.Subscriptions", new[] { "UserId" });
            DropColumn("dbo.Schedules", "UserId");
            DropColumn("dbo.Subscriptions", "UserId");
            CreateIndex("dbo.Schedules", "PartnerId");
            CreateIndex("dbo.Subscriptions", "PartnerId");
            AddForeignKey("dbo.Schedules", "PartnerId", "dbo.Partners", "Id");
            AddForeignKey("dbo.Subscriptions", "PartnerId", "dbo.Partners", "Id");
        }
    }
}
