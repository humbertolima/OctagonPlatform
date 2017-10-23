namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTerminalWorksHours : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TerminalWorkingHours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TerminalId = c.String(nullable: false),
                        Day = c.Int(nullable: false),
                        StartTime = c.Time(nullable: false, precision: 7),
                        EndTime = c.Time(nullable: false, precision: 7),
                        Terminal_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Terminals", t => t.Terminal_Id)
                .Index(t => t.Terminal_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TerminalWorkingHours", "Terminal_Id", "dbo.Terminals");
            DropIndex("dbo.TerminalWorkingHours", new[] { "Terminal_Id" });
            DropTable("dbo.TerminalWorkingHours");
        }
    }
}
