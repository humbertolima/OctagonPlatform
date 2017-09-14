namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class VaultCash : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VaultCashes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        BankAccountId = c.Int(nullable: false),
                        TotalAmmount = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        StopDate = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.Int(),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.Int(),
                        UpdatedByName = c.String(),
                        CreatedByName = c.String(),
                        DeletedByName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccountId, cascadeDelete: true)
                .ForeignKey("dbo.Terminals", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.BankAccountId);
            
            AlterColumn("dbo.Contracts", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Contracts", "TerminationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VaultCashes", "Id", "dbo.Terminals");
            DropForeignKey("dbo.VaultCashes", "BankAccountId", "dbo.BankAccounts");
            DropIndex("dbo.VaultCashes", new[] { "BankAccountId" });
            DropIndex("dbo.VaultCashes", new[] { "Id" });
            AlterColumn("dbo.Contracts", "TerminationDate", c => c.DateTime());
            AlterColumn("dbo.Contracts", "StartDate", c => c.DateTime());
            DropTable("dbo.VaultCashes");
        }
    }
}
