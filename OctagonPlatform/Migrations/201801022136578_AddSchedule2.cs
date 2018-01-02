namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSchedule2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Schedules", "StopDate", c => c.String());
            AlterColumn("dbo.Schedules", "StopDate1", c => c.String());
            AlterColumn("dbo.Schedules", "StopDate2", c => c.String());
            AlterColumn("dbo.Schedules", "StopDate3", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Schedules", "StopDate3", c => c.DateTime());
            AlterColumn("dbo.Schedules", "StopDate2", c => c.DateTime());
            AlterColumn("dbo.Schedules", "StopDate1", c => c.DateTime());
            AlterColumn("dbo.Schedules", "StopDate", c => c.DateTime());
        }
    }
}
