namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationPartnerSchedule : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Schedules", "PartnerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Schedules", "PartnerId");
            AddForeignKey("dbo.Schedules", "PartnerId", "dbo.Partners", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "PartnerId", "dbo.Partners");
            DropIndex("dbo.Schedules", new[] { "PartnerId" });
            DropColumn("dbo.Schedules", "PartnerId");
        }
    }
}
