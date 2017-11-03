namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class MigrationAPartirdeLaCreacionDeTerminalHastaBankAccounts : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PartnerContacts", new[] { "Email" });
            DropIndex("dbo.Partners", new[] { "Email" });
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NickName = c.String(nullable: false, maxLength: 50),
                        RoutingNumber = c.String(nullable: false, maxLength: 50),
                        AccountNumber = c.String(nullable: false, maxLength: 50),
                        Status = c.Int(nullable: false),
                        NameOnCheck = c.String(nullable: false),
                        FedTax = c.String(nullable: false, maxLength: 50),
                        Ssn = c.String(nullable: false, maxLength: 50),
                        CountryId = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        Address1 = c.String(nullable: false),
                        Address2 = c.String(),
                        Zip = c.Int(nullable: false),
                        Phone = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        PartnerId = c.Int(nullable: false),
                        AccountType = c.Int(nullable: false),
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
                .ForeignKey("dbo.Cities", t => t.CityId)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.Partners", t => t.PartnerId, cascadeDelete: false)
                .ForeignKey("dbo.States", t => t.StateId)
                .Index(t => t.NickName, unique: true)
                .Index(t => t.RoutingNumber, unique: true)
                .Index(t => t.AccountNumber, unique: true)
                .Index(t => t.FedTax, unique: true)
                .Index(t => t.Ssn, unique: true)
                .Index(t => t.CountryId)
                .Index(t => t.StateId)
                .Index(t => t.CityId)
                .Index(t => t.Phone, unique: true)
                .Index(t => t.Email, unique: true)
                .Index(t => t.PartnerId);
            
            CreateTable(
                "dbo.TerminalContacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        TerminalId = c.Int(nullable: false),
                        ContactTypeId = c.Int(nullable: false),
                        Address1 = c.String(nullable: false),
                        Address2 = c.String(),
                        CountryId = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 16),
                        Phone = c.String(nullable: false, maxLength: 50),
                        Zip = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.Int(),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.Int(),
                        UpdatedByName = c.String(),
                        CreatedByName = c.String(),
                        DeletedByName = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .ForeignKey("dbo.ContactTypes", t => t.ContactTypeId)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.States", t => t.StateId)
                .ForeignKey("dbo.Terminals", t => t.TerminalId, cascadeDelete: false)
                .Index(t => t.TerminalId)
                .Index(t => t.ContactTypeId)
                .Index(t => t.CountryId)
                .Index(t => t.StateId)
                .Index(t => t.CityId)
                .Index(t => t.Email, unique: true)
                .Index(t => t.Phone, unique: true);
            
            CreateTable(
                "dbo.Terminals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PartnerId = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        LocationTypeId = c.Int(nullable: false),
                        MakeId = c.Int(nullable: false),
                        ModelId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CommunicationType = c.Int(nullable: false),
                        Address1 = c.String(nullable: false),
                        Address2 = c.String(),
                        Zip = c.Int(nullable: false),
                        EmvReady = c.Boolean(nullable: false),
                        MachineSerialNumber = c.String(nullable: false, maxLength: 50),
                        VeppSerailNumber = c.String(),
                        SoftwareVersion = c.String(),
                        FimwareVersion = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.Int(),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.Int(),
                        UpdatedByName = c.String(),
                        CreatedByName = c.String(),
                        DeletedByName = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.LocationTypes", t => t.LocationTypeId)
                .ForeignKey("dbo.Models", t => t.ModelId)
                .ForeignKey("dbo.Makes", t => t.MakeId)
                .ForeignKey("dbo.Partners", t => t.PartnerId)
                .ForeignKey("dbo.States", t => t.StateId)
                .Index(t => t.PartnerId)
                .Index(t => t.CountryId)
                .Index(t => t.StateId)
                .Index(t => t.CityId)
                .Index(t => t.LocationTypeId)
                .Index(t => t.MakeId)
                .Index(t => t.ModelId)
                .Index(t => t.MachineSerialNumber, unique: true);
            
            CreateTable(
                "dbo.LocationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Makes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Models",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        MakeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Makes", t => t.MakeId, cascadeDelete: false)
                .Index(t => t.MakeId);
            
            CreateTable(
                "dbo.UserBankAccounts",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        BankAccount_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.BankAccount_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: false)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccount_Id, cascadeDelete: false)
                .Index(t => t.User_Id)
                .Index(t => t.BankAccount_Id);
            
            CreateTable(
                "dbo.UserTerminals",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Terminal_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Terminal_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: false)
                .ForeignKey("dbo.Terminals", t => t.Terminal_Id, cascadeDelete: false)
                .Index(t => t.User_Id)
                .Index(t => t.Terminal_Id);
            
            AddColumn("dbo.PartnerContacts", "UpdatedByName", c => c.String());
            AddColumn("dbo.PartnerContacts", "CreatedByName", c => c.String());
            AddColumn("dbo.PartnerContacts", "DeletedByName", c => c.String());
            AddColumn("dbo.Partners", "UpdatedByName", c => c.String());
            AddColumn("dbo.Partners", "CreatedByName", c => c.String());
            AddColumn("dbo.Partners", "DeletedByName", c => c.String());
            AddColumn("dbo.Users", "UpdatedByName", c => c.String());
            AddColumn("dbo.Users", "CreatedByName", c => c.String());
            AddColumn("dbo.Users", "DeletedByName", c => c.String());
            AlterColumn("dbo.PartnerContacts", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Partners", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Partners", "Email", unique: true);
            CreateIndex("dbo.PartnerContacts", "Email", unique: true);
            CreateIndex("dbo.Users", "Email", unique: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BankAccounts", "StateId", "dbo.States");
            DropForeignKey("dbo.UserTerminals", "Terminal_Id", "dbo.Terminals");
            DropForeignKey("dbo.UserTerminals", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserBankAccounts", "BankAccount_Id", "dbo.BankAccounts");
            DropForeignKey("dbo.UserBankAccounts", "User_Id", "dbo.Users");
            DropForeignKey("dbo.TerminalContacts", "TerminalId", "dbo.Terminals");
            DropForeignKey("dbo.Terminals", "StateId", "dbo.States");
            DropForeignKey("dbo.Terminals", "PartnerId", "dbo.Partners");
            DropForeignKey("dbo.Terminals", "MakeId", "dbo.Makes");
            DropForeignKey("dbo.Terminals", "ModelId", "dbo.Models");
            DropForeignKey("dbo.Models", "MakeId", "dbo.Makes");
            DropForeignKey("dbo.Terminals", "LocationTypeId", "dbo.LocationTypes");
            DropForeignKey("dbo.Terminals", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Terminals", "CityId", "dbo.Cities");
            DropForeignKey("dbo.TerminalContacts", "StateId", "dbo.States");
            DropForeignKey("dbo.TerminalContacts", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.TerminalContacts", "ContactTypeId", "dbo.ContactTypes");
            DropForeignKey("dbo.TerminalContacts", "CityId", "dbo.Cities");
            DropForeignKey("dbo.BankAccounts", "PartnerId", "dbo.Partners");
            DropForeignKey("dbo.BankAccounts", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.BankAccounts", "CityId", "dbo.Cities");
            DropIndex("dbo.UserTerminals", new[] { "Terminal_Id" });
            DropIndex("dbo.UserTerminals", new[] { "User_Id" });
            DropIndex("dbo.UserBankAccounts", new[] { "BankAccount_Id" });
            DropIndex("dbo.UserBankAccounts", new[] { "User_Id" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.Models", new[] { "MakeId" });
            DropIndex("dbo.Terminals", new[] { "MachineSerialNumber" });
            DropIndex("dbo.Terminals", new[] { "ModelId" });
            DropIndex("dbo.Terminals", new[] { "MakeId" });
            DropIndex("dbo.Terminals", new[] { "LocationTypeId" });
            DropIndex("dbo.Terminals", new[] { "CityId" });
            DropIndex("dbo.Terminals", new[] { "StateId" });
            DropIndex("dbo.Terminals", new[] { "CountryId" });
            DropIndex("dbo.Terminals", new[] { "PartnerId" });
            DropIndex("dbo.TerminalContacts", new[] { "Phone" });
            DropIndex("dbo.TerminalContacts", new[] { "Email" });
            DropIndex("dbo.TerminalContacts", new[] { "CityId" });
            DropIndex("dbo.TerminalContacts", new[] { "StateId" });
            DropIndex("dbo.TerminalContacts", new[] { "CountryId" });
            DropIndex("dbo.TerminalContacts", new[] { "ContactTypeId" });
            DropIndex("dbo.TerminalContacts", new[] { "TerminalId" });
            DropIndex("dbo.PartnerContacts", new[] { "Email" });
            DropIndex("dbo.Partners", new[] { "Email" });
            DropIndex("dbo.BankAccounts", new[] { "PartnerId" });
            DropIndex("dbo.BankAccounts", new[] { "Email" });
            DropIndex("dbo.BankAccounts", new[] { "Phone" });
            DropIndex("dbo.BankAccounts", new[] { "CityId" });
            DropIndex("dbo.BankAccounts", new[] { "StateId" });
            DropIndex("dbo.BankAccounts", new[] { "CountryId" });
            DropIndex("dbo.BankAccounts", new[] { "Ssn" });
            DropIndex("dbo.BankAccounts", new[] { "FedTax" });
            DropIndex("dbo.BankAccounts", new[] { "AccountNumber" });
            DropIndex("dbo.BankAccounts", new[] { "RoutingNumber" });
            DropIndex("dbo.BankAccounts", new[] { "NickName" });
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 16));
            AlterColumn("dbo.Partners", "Email", c => c.String(nullable: false, maxLength: 16));
            AlterColumn("dbo.PartnerContacts", "Email", c => c.String(nullable: false, maxLength: 16));
            DropColumn("dbo.Users", "DeletedByName");
            DropColumn("dbo.Users", "CreatedByName");
            DropColumn("dbo.Users", "UpdatedByName");
            DropColumn("dbo.Partners", "DeletedByName");
            DropColumn("dbo.Partners", "CreatedByName");
            DropColumn("dbo.Partners", "UpdatedByName");
            DropColumn("dbo.PartnerContacts", "DeletedByName");
            DropColumn("dbo.PartnerContacts", "CreatedByName");
            DropColumn("dbo.PartnerContacts", "UpdatedByName");
            DropTable("dbo.UserTerminals");
            DropTable("dbo.UserBankAccounts");
            DropTable("dbo.Models");
            DropTable("dbo.Makes");
            DropTable("dbo.LocationTypes");
            DropTable("dbo.Terminals");
            DropTable("dbo.TerminalContacts");
            DropTable("dbo.BankAccounts");
            CreateIndex("dbo.Partners", "Email", unique: true);
            CreateIndex("dbo.PartnerContacts", "Email", unique: true);
        }
    }
}
