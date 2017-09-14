namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ActualizacionStartDateEnContracts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contracts", "StartDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contracts", "StartDate");
        }
    }
}
