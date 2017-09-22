namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SerialNumberRectificadodeNombre : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Terminals", "VeppSerialNumber", c => c.String());
            DropColumn("dbo.Terminals", "VeppSerailNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Terminals", "VeppSerailNumber", c => c.String());
            DropColumn("dbo.Terminals", "VeppSerialNumber");
        }
    }
}
