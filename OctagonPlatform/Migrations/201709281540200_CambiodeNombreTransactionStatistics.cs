namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CambiodeNombreTransactionStatistics : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Transactions", newName: "TransactionStatistics");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.TransactionStatistics", newName: "Transactions");
        }
    }
}
