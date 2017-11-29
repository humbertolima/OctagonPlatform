namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualizacionTerminal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Terminals", "InstalledDate", c => c.DateTime());
            AddColumn("dbo.Terminals", "ChargedBy", c => c.String());
            AddColumn("dbo.Terminals", "LastCommunicationDate", c => c.DateTime());
            AddColumn("dbo.Terminals", "Balance", c => c.Int());
            AddColumn("dbo.Terminals", "LastTransactionDate", c => c.DateTime());
            AddColumn("dbo.Terminals", "MinAmmountCash", c => c.Int());
            AddColumn("dbo.Terminals", "Offline", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Terminals", "Offline");
            DropColumn("dbo.Terminals", "MinAmmountCash");
            DropColumn("dbo.Terminals", "LastTransactionDate");
            DropColumn("dbo.Terminals", "Balance");
            DropColumn("dbo.Terminals", "LastCommunicationDate");
            DropColumn("dbo.Terminals", "ChargedBy");
            DropColumn("dbo.Terminals", "InstalledDate");
        }
    }
}
