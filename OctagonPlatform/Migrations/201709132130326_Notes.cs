namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Notes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TerminalPictures", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.TerminalPictures", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.TerminalPictures", "CreatedBy", c => c.Int());
            AddColumn("dbo.TerminalPictures", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.TerminalPictures", "DeletedBy", c => c.Int());
            AddColumn("dbo.TerminalPictures", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.TerminalPictures", "UpdatedBy", c => c.Int());
            AddColumn("dbo.TerminalPictures", "UpdatedByName", c => c.String());
            AddColumn("dbo.TerminalPictures", "CreatedByName", c => c.String());
            AddColumn("dbo.TerminalPictures", "DeletedByName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TerminalPictures", "DeletedByName");
            DropColumn("dbo.TerminalPictures", "CreatedByName");
            DropColumn("dbo.TerminalPictures", "UpdatedByName");
            DropColumn("dbo.TerminalPictures", "UpdatedBy");
            DropColumn("dbo.TerminalPictures", "UpdatedAt");
            DropColumn("dbo.TerminalPictures", "DeletedBy");
            DropColumn("dbo.TerminalPictures", "DeletedAt");
            DropColumn("dbo.TerminalPictures", "CreatedBy");
            DropColumn("dbo.TerminalPictures", "CreatedAt");
            DropColumn("dbo.TerminalPictures", "Deleted");
        }
    }
}
