namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LastTransaction : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transactions", "TerminalId", "dbo.Terminals");
            AddColumn("dbo.Terminals", "LastTransactionId", c => c.Int(nullable: false));
            AddColumn("dbo.Transactions", "Terminal_Id", c => c.Int());
            CreateIndex("dbo.Terminals", "LastTransactionId");
            CreateIndex("dbo.Transactions", "Terminal_Id");
            AddForeignKey("dbo.Terminals", "LastTransactionId", "dbo.Transactions", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Transactions", "Terminal_Id", "dbo.Terminals", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "Terminal_Id", "dbo.Terminals");
            DropForeignKey("dbo.Terminals", "LastTransactionId", "dbo.Transactions");
            DropIndex("dbo.Transactions", new[] { "Terminal_Id" });
            DropIndex("dbo.Terminals", new[] { "LastTransactionId" });
            DropColumn("dbo.Transactions", "Terminal_Id");
            DropColumn("dbo.Terminals", "LastTransactionId");
            AddForeignKey("dbo.Transactions", "TerminalId", "dbo.Terminals", "Id", cascadeDelete: false);
        }
    }
}
