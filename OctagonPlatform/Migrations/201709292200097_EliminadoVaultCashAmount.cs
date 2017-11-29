namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EliminadoVaultCashAmount : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.VaultCashes", "TotalAmmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VaultCashes", "TotalAmmount", c => c.Int(nullable: false));
        }
    }
}
