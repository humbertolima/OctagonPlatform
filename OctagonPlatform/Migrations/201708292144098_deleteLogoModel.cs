namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteLogoModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Partners", "LogoId", "dbo.Logoes");
            DropIndex("dbo.Partners", new[] { "LogoId" });
            AddColumn("dbo.Partners", "Logo", c => c.String());
            DropColumn("dbo.Partners", "LogoId");
            DropTable("dbo.Logoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Logoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Picture = c.String(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Partners", "LogoId", c => c.Int());
            DropColumn("dbo.Partners", "Logo");
            CreateIndex("dbo.Partners", "LogoId");
            AddForeignKey("dbo.Partners", "LogoId", "dbo.Logoes", "Id");
        }
    }
}
