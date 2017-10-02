namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CambiadoChargedByLoadedByEnTerminal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Terminals", "LoadedBy", c => c.String());
            DropColumn("dbo.Terminals", "ChargedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Terminals", "ChargedBy", c => c.String());
            DropColumn("dbo.Terminals", "LoadedBy");
        }
    }
}
