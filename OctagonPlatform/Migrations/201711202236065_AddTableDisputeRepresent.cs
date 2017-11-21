namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableDisputeRepresent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DisputeRepresents",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Phone = c.String(),
                        CC = c.String(),
                        Comments = c.String(),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Disputes", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DisputeRepresents", "Id", "dbo.Disputes");
            DropIndex("dbo.DisputeRepresents", new[] { "Id" });
            DropTable("dbo.DisputeRepresents");
        }
    }
}
