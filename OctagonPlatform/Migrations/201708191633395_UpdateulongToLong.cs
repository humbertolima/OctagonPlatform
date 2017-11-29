using System.Data.Entity.Migrations;

namespace OctagonPlatform.Migrations
{
    public partial class UpdateulongToLong : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Partners", "WorkPhone", c => c.Long());
            AddColumn("dbo.Partners", "Fax", c => c.Long());
            //aqui.
        }
        
        public override void Down()
        {
            DropColumn("dbo.Partners", "Fax");
            DropColumn("dbo.Partners", "WorkPhone");
        }
    }
}
