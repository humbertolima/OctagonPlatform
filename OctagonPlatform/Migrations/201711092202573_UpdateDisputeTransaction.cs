namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDisputeTransaction : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Disputes", "TransactionId", "dbo.TransactionStatistics");
            DropIndex("dbo.Disputes", new[] { "TransactionId" });
            AddColumn("dbo.Disputes", "TransactionStatistic_Id", c => c.Int());
            CreateIndex("dbo.Disputes", "TransactionStatistic_Id");
            AddForeignKey("dbo.Disputes", "TransactionStatistic_Id", "dbo.TransactionStatistics", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Disputes", "TransactionStatistic_Id", "dbo.TransactionStatistics");
            DropIndex("dbo.Disputes", new[] { "TransactionStatistic_Id" });
            DropColumn("dbo.Disputes", "TransactionStatistic_Id");
            CreateIndex("dbo.Disputes", "TransactionId");
            AddForeignKey("dbo.Disputes", "TransactionId", "dbo.TransactionStatistics", "Id", cascadeDelete: true);
        }
    }
}
