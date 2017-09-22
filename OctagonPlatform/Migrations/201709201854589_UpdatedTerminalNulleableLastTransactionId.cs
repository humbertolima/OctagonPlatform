namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdatedTerminalNulleableLastTransactionId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Terminals", "LastTransactionId", "dbo.Transactions");
            DropIndex("dbo.Terminals", new[] { "LastTransactionId" });
            AlterColumn("dbo.Terminals", "LastTransactionId", c => c.Int());
            CreateIndex("dbo.Terminals", "LastTransactionId");
            AddForeignKey("dbo.Terminals", "LastTransactionId", "dbo.Transactions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Terminals", "LastTransactionId", "dbo.Transactions");
            DropIndex("dbo.Terminals", new[] { "LastTransactionId" });
            AlterColumn("dbo.Terminals", "LastTransactionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Terminals", "LastTransactionId");
            AddForeignKey("dbo.Terminals", "LastTransactionId", "dbo.Transactions", "Id", cascadeDelete: true);
        }
    }
}
