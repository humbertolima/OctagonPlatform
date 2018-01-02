namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSchedule : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Repeats = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        Time = c.String(),
                        RepeatOn = c.Int(),
                        StopDate = c.DateTime(),
                        Time1 = c.String(),
                        RepeatOnDay = c.Int(),
                        RepeatOnMonth = c.Int(),
                        StopDate1 = c.DateTime(),
                        Time2 = c.String(),
                        RepeatOnFirst = c.String(),
                        RepeatOnDay1 = c.String(),
                        RepeatOnMonth1 = c.Int(),
                        StopDate2 = c.DateTime(),
                        Time3 = c.String(),
                        Time4 = c.String(),
                        RepeatOnWeeks = c.Int(),
                        RepeatOnDaysWeeks = c.String(),
                        StopDate3 = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Schedules");
        }
    }
}
