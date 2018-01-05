namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Schedule : DbMigration
    {
        public override void Up()
        {
            
            //DropForeignKey("dbo.Schedules", "PartnerId", "dbo.Partners");
            //DropIndex("dbo.Schedules", new[] { "PartnerId" });
            //DropTable("dbo.Schedules");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Repeats = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        PartnerId = c.Int(nullable: false),
                        Time = c.String(),
                        RepeatOn = c.Int(),
                        StopDate = c.String(),
                        Time1 = c.String(),
                        RepeatOnDay = c.Int(),
                        RepeatOnMonth = c.Int(),
                        StopDate1 = c.String(),
                        Time2 = c.String(),
                        RepeatOnFirst = c.String(),
                        RepeatOnDay1 = c.String(),
                        RepeatOnMonth1 = c.Int(),
                        StopDate2 = c.String(),
                        Time3 = c.String(),
                        Time4 = c.String(),
                        RepeatOnWeeks = c.Int(),
                        RepeatOnDaysWeeks = c.String(),
                        StopDate3 = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.Schedules", "PartnerId");
            AddForeignKey("dbo.Schedules", "PartnerId", "dbo.Partners", "Id");
        }
    }
}
