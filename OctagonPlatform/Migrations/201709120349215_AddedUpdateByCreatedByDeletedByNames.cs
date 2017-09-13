namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUpdateByCreatedByDeletedByNames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PartnerContacts", "UpdatedByName", c => c.String());
            AddColumn("dbo.PartnerContacts", "CreatedByName", c => c.String());
            AddColumn("dbo.PartnerContacts", "DeletedByName", c => c.String());
            AddColumn("dbo.Partners", "UpdatedByName", c => c.String());
            AddColumn("dbo.Partners", "CreatedByName", c => c.String());
            AddColumn("dbo.Partners", "DeletedByName", c => c.String());
            AddColumn("dbo.Terminals", "UpdatedByName", c => c.String());
            AddColumn("dbo.Terminals", "CreatedByName", c => c.String());
            AddColumn("dbo.Terminals", "DeletedByName", c => c.String());
            AddColumn("dbo.TerminalContacts", "UpdatedByName", c => c.String());
            AddColumn("dbo.TerminalContacts", "CreatedByName", c => c.String());
            AddColumn("dbo.TerminalContacts", "DeletedByName", c => c.String());
            AddColumn("dbo.Users", "UpdatedByName", c => c.String());
            AddColumn("dbo.Users", "CreatedByName", c => c.String());
            AddColumn("dbo.Users", "DeletedByName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "DeletedByName");
            DropColumn("dbo.Users", "CreatedByName");
            DropColumn("dbo.Users", "UpdatedByName");
            DropColumn("dbo.TerminalContacts", "DeletedByName");
            DropColumn("dbo.TerminalContacts", "CreatedByName");
            DropColumn("dbo.TerminalContacts", "UpdatedByName");
            DropColumn("dbo.Terminals", "DeletedByName");
            DropColumn("dbo.Terminals", "CreatedByName");
            DropColumn("dbo.Terminals", "UpdatedByName");
            DropColumn("dbo.Partners", "DeletedByName");
            DropColumn("dbo.Partners", "CreatedByName");
            DropColumn("dbo.Partners", "UpdatedByName");
            DropColumn("dbo.PartnerContacts", "DeletedByName");
            DropColumn("dbo.PartnerContacts", "CreatedByName");
            DropColumn("dbo.PartnerContacts", "UpdatedByName");
        }
    }
}
