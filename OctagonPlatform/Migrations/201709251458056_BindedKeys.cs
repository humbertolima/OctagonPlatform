namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BindedKeys : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BindedKeys",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        FirstKey = c.String(nullable: false),
                        SecondKey = c.String(nullable: false),
                        EncryptionType = c.String(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.Int(),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.Int(),
                        UpdatedByName = c.String(),
                        CreatedByName = c.String(),
                        DeletedByName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Terminals", t => t.Id)
                .Index(t => t.Id);
            
            DropColumn("dbo.Terminals", "Key1");
            DropColumn("dbo.Terminals", "Key2");
            DropColumn("dbo.Terminals", "EncryptionType");
            DropColumn("dbo.Terminals", "DateKeyBounded");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Terminals", "DateKeyBounded", c => c.DateTime());
            AddColumn("dbo.Terminals", "EncryptionType", c => c.String());
            AddColumn("dbo.Terminals", "Key2", c => c.String());
            AddColumn("dbo.Terminals", "Key1", c => c.String());
            DropForeignKey("dbo.BindedKeys", "Id", "dbo.Terminals");
            DropIndex("dbo.BindedKeys", new[] { "Id" });
            DropTable("dbo.BindedKeys");
        }
    }
}
