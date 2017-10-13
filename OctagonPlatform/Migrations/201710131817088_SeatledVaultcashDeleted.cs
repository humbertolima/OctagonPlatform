namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeatledVaultcashDeleted : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.VaultCashes", "SettledType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VaultCashes", "SettledType", c => c.Int(nullable: false));
        }
    }
}
