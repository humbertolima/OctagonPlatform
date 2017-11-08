namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RemovingEMVMinimunCashPropertyFromTerminal : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Terminals", "EmvReady");
            DropColumn("dbo.Terminals", "InstalledDate");
            DropColumn("dbo.Terminals", "LoadedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Terminals", "LoadedBy", c => c.String());
            AddColumn("dbo.Terminals", "InstalledDate", c => c.DateTime());
            AddColumn("dbo.Terminals", "EmvReady", c => c.Boolean(nullable: false));
        }
    }
}
