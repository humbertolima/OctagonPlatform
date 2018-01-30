namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cascadeschedule : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subscriptions", "ScheduleId", "dbo.Schedules");
            AddForeignKey("dbo.Subscriptions", "ScheduleId", "dbo.Schedules", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subscriptions", "ScheduleId", "dbo.Schedules");
            AddForeignKey("dbo.Subscriptions", "ScheduleId", "dbo.Schedules", "ID");
        }
    }
}
