namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ini : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NickName = c.String(nullable: false, maxLength: 50),
                        BankName = c.String(nullable: false, maxLength: 50),
                        RoutingNumber = c.String(nullable: false, maxLength: 50),
                        AccountNumber = c.String(nullable: false, maxLength: 50),
                        Status = c.Int(nullable: false),
                        NameOnCheck = c.String(nullable: false),
                        FedTax = c.String(nullable: false),
                        Ssn = c.String(),
                        CountryId = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        Address1 = c.String(nullable: false),
                        Address2 = c.String(),
                        Zip = c.Int(nullable: false),
                        Phone = c.String(nullable: false),
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
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("dbo.Partners", t => t.PartnerId, cascadeDelete: true)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .Index(t => t.CountryId)
                .Index(t => t.StateId)
                .Index(t => t.CityId)
                .Index(t => t.PartnerId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        StateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Partners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(nullable: false),
                        BusinessName = c.String(nullable: false, maxLength: 50),
                        Status = c.Int(nullable: false),
                        Address1 = c.String(nullable: false, maxLength: 50),
                        Address2 = c.String(maxLength: 50),
                        CountryId = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                        CityId = c.Int(),
                        Email = c.String(nullable: false, maxLength: 50),
                        WorkPhone = c.String(),
                        Mobile = c.String(nullable: false),
                        Fax = c.String(),
                        WebSite = c.String(),
                        Logo = c.String(),
                        Interchange = c.Double(nullable: false),
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
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("dbo.Partners", t => t.ParentId)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .Index(t => t.ParentId)
                .Index(t => t.CountryId)
                .Index(t => t.StateId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.PartnerContacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PartnerId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 50),
                        ContactTypeId = c.Int(nullable: false),
                        Phone = c.String(nullable: false),
                        Address1 = c.String(nullable: false, maxLength: 50),
                        Address2 = c.String(maxLength: 50),
                        Zip = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                        CityId = c.Int(),
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
                .ForeignKey("dbo.ContactTypes", t => t.ContactTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("dbo.Partners", t => t.PartnerId, cascadeDelete: true)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .Index(t => t.PartnerId)
                .Index(t => t.ContactTypeId)
                .Index(t => t.CountryId)
                .Index(t => t.StateId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.ContactTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.ContactTypes", t => t.ContactTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .ForeignKey("dbo.Terminals", t => t.TerminalId, cascadeDelete: true)
                .Index(t => t.TerminalId)
                .Index(t => t.ContactTypeId)
                .Index(t => t.CountryId)
                .Index(t => t.StateId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Terminals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TerminalId = c.String(nullable: false),
                        PartnerId = c.Int(nullable: false),
                        Phone = c.String(nullable: false),
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
                        MachineSerialNumber = c.String(nullable: false, maxLength: 50),
                        VeppSerialNumber = c.String(),
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
                        Balance = c.Double(),
                        LastTransactionId = c.Int(),
                        WhoInitiates = c.Int(nullable: false),
                        SurchargeAmountFee = c.Double(nullable: false),
                        SurchargePercentageFee = c.Double(nullable: false),
                        SurchargeType = c.Int(nullable: false),
                        CryptoPercentChargeAmount = c.Double(nullable: false),
                        BankAccount_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("dbo.TransactionStatistics", t => t.LastTransactionId)
                .ForeignKey("dbo.LocationTypes", t => t.LocationTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Models", t => t.ModelId, cascadeDelete: true)
                .ForeignKey("dbo.Makes", t => t.MakeId, cascadeDelete: true)
                .ForeignKey("dbo.Partners", t => t.PartnerId, cascadeDelete: true)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccount_Id)
                .Index(t => t.PartnerId)
                .Index(t => t.CountryId)
                .Index(t => t.StateId)
                .Index(t => t.CityId)
                .Index(t => t.LocationTypeId)
                .Index(t => t.MakeId)
                .Index(t => t.ModelId)
                .Index(t => t.LastTransactionId)
                .Index(t => t.BankAccount_Id);
            
            CreateTable(
                "dbo.BindedKeys",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        FirstKey = c.String(nullable: false),
                        SecondKey = c.String(nullable: false),
                        EncryptionType = c.String(nullable: false),
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
                .ForeignKey("dbo.Terminals", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Cassettes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AutoRecord = c.Boolean(nullable: false),
                        Denomination = c.Int(nullable: false),
                        TerminalId = c.Int(nullable: false),
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
                .ForeignKey("dbo.Terminals", t => t.TerminalId, cascadeDelete: true)
                .Index(t => t.TerminalId);
            
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        TerminationDate = c.DateTime(nullable: false),
                        AutoRenew = c.Boolean(nullable: false),
                        TerminalId = c.Int(nullable: false),
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
                .ForeignKey("dbo.Terminals", t => t.TerminalId, cascadeDelete: true)
                .Index(t => t.TerminalId);
            
            CreateTable(
                "dbo.CryptoChargeAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TerminalId = c.Int(nullable: false),
                        BankAccountId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        StopDate = c.DateTime(nullable: false),
                        SplitAmmount = c.Double(nullable: false),
                        SettledType = c.Int(nullable: false),
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
                .ForeignKey("dbo.Terminals", t => t.TerminalId, cascadeDelete: true)
                .Index(t => t.TerminalId)
                .Index(t => t.BankAccountId);
            
            CreateTable(
                "dbo.CryptoCurrencyTransactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TerminalId = c.Int(nullable: false),
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
                .ForeignKey("dbo.Terminals", t => t.TerminalId, cascadeDelete: true)
                .Index(t => t.TerminalId);
            
            CreateTable(
                "dbo.Disputes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Viewed = c.Boolean(nullable: false),
                        MessageNumber = c.String(),
                        NetworkAdjustmentId = c.String(),
                        IndexId = c.Int(nullable: false),
                        TransacNo = c.Int(nullable: false),
                        Network = c.String(),
                        DisputeType = c.Int(nullable: false),
                        SecuenceNumber = c.String(),
                        AmountRequested = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DisputedAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LastDayToRepresent = c.DateTime(nullable: false),
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
                        Terminal_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Terminals", t => t.Terminal_Id)
                .Index(t => t.Terminal_Id);
            
            CreateTable(
                "dbo.DisputeRepresents",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Phone = c.String(),
                        CC = c.String(),
                        Comments = c.String(),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Disputes", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Category = c.String(nullable: false),
                        Privacy = c.Boolean(nullable: false),
                        TerminalId = c.Int(nullable: false),
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
                .ForeignKey("dbo.Terminals", t => t.TerminalId, cascadeDelete: true)
                .Index(t => t.TerminalId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TerminalId = c.Int(nullable: false),
                        MessageNumber = c.String(),
                        MessageType = c.String(),
                        AtmType = c.String(),
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
                .ForeignKey("dbo.Terminals", t => t.TerminalId, cascadeDelete: true)
                .Index(t => t.TerminalId);
            
            CreateTable(
                "dbo.InterChanges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TerminalId = c.Int(nullable: false),
                        BankAccountId = c.Int(nullable: false),
                        SplitAmount = c.Double(nullable: false),
                        CalculationMethod = c.Int(nullable: false),
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
                .ForeignKey("dbo.Terminals", t => t.TerminalId, cascadeDelete: true)
                .Index(t => t.TerminalId)
                .Index(t => t.BankAccountId);
            
            CreateTable(
                "dbo.TransactionStatistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TerminalId = c.Int(nullable: false),
                        TransactionType = c.String(),
                        Normal = c.Boolean(nullable: false),
                        Reversal = c.Boolean(nullable: false),
                        Dcc = c.String(),
                        Pan = c.String(),
                        CardBrand = c.String(),
                        Input = c.String(),
                        CardSequence = c.String(),
                        Response = c.String(),
                        AmmountRequested = c.Int(nullable: false),
                        AmmountAproved = c.Int(nullable: false),
                        AmmountSurcharge = c.Double(nullable: false),
                        AmmountReversed = c.Double(nullable: false),
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
                        Terminal_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Terminals", t => t.TerminalId, cascadeDelete: true)
                .ForeignKey("dbo.Terminals", t => t.Terminal_Id)
                .Index(t => t.TerminalId)
                .Index(t => t.Terminal_Id);
            
            CreateTable(
                "dbo.LoadCashes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PreviousBalance = c.Int(nullable: false),
                        AmmountLoaded = c.Int(nullable: false),
                        CurrentBalance = c.Int(nullable: false),
                        TerminalId = c.Int(nullable: false),
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
                .ForeignKey("dbo.Terminals", t => t.TerminalId, cascadeDelete: true)
                .Index(t => t.TerminalId);
            
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
                .ForeignKey("dbo.Makes", t => t.MakeId, cascadeDelete: true)
                .Index(t => t.MakeId);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nota = c.String(nullable: false),
                        TerminalId = c.Int(nullable: false),
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
                .ForeignKey("dbo.Terminals", t => t.TerminalId, cascadeDelete: true)
                .Index(t => t.TerminalId);
            
            CreateTable(
                "dbo.Surcharges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TerminalId = c.Int(nullable: false),
                        BankAccountId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        StopDate = c.DateTime(nullable: false),
                        SplitAmount = c.Double(nullable: false),
                        SplitAmountPercent = c.Double(nullable: false),
                        SettledType = c.Int(nullable: false),
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
                .ForeignKey("dbo.Terminals", t => t.TerminalId, cascadeDelete: true)
                .Index(t => t.TerminalId)
                .Index(t => t.BankAccountId);
            
            CreateTable(
                "dbo.TerminalAlertConfigs",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        LowCach1 = c.Double(nullable: false),
                        LowCash2 = c.Double(),
                        LowCash3 = c.Double(),
                        IgnoreHoursInactive = c.Int(nullable: false),
                        IgnoreChestDoorOpen = c.Boolean(nullable: false),
                        IgnoreTopDoorOpen = c.Boolean(nullable: false),
                        IgnoreReceiptPaper = c.Boolean(nullable: false),
                        IgnoreReceiptRibbon = c.Boolean(nullable: false),
                        IgnoreJournalPaper = c.Boolean(nullable: false),
                        IgnoreJournalRibbon = c.Boolean(nullable: false),
                        IgnoreCassetteNotes = c.Boolean(nullable: false),
                        IgnoreReceiptNeedAttention = c.Boolean(nullable: false),
                        IgnoreJournalNeedAttention = c.Boolean(nullable: false),
                        IgnoreBillDispenserNeedAttention = c.Boolean(nullable: false),
                        IgnoreCommNeedAttention = c.Boolean(nullable: false),
                        IgnoreCardReaderNeedAttention = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Terminals", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.TerminalAlerts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TerminalId = c.String(nullable: false),
                        Notificated = c.Boolean(nullable: false),
                        CashAvailable = c.String(),
                        AlarmChestdooropen = c.String(),
                        AlarmTopdooropen = c.String(),
                        AlarmSupervisoractive = c.String(),
                        Receiptprinterpaperstatus = c.String(),
                        ReceiptPrinterRibbonStatus = c.String(),
                        JournalPrinterPaperStatus = c.String(),
                        JournalPrinterRibbonStatus = c.String(),
                        NoteStatusDispenser = c.String(),
                        ReceiptPrinter = c.String(),
                        JournalPrinter = c.String(),
                        Dispenser = c.String(),
                        CommunicationsSystem = c.String(),
                        CardReader = c.String(),
                        Terminal_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Terminals", t => t.Terminal_Id)
                .Index(t => t.Terminal_Id);
            
            CreateTable(
                "dbo.TerminalPictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(nullable: false),
                        TerminalId = c.Int(nullable: false),
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
                .ForeignKey("dbo.Terminals", t => t.TerminalId, cascadeDelete: true)
                .Index(t => t.TerminalId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 100),
                        Name = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Status = c.Int(nullable: false),
                        PartnerId = c.Int(nullable: false),
                        Password = c.String(nullable: false, maxLength: 100),
                        Key = c.String(nullable: false),
                        IsLocked = c.Boolean(nullable: false),
                        Email = c.String(nullable: false, maxLength: 50),
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
                .ForeignKey("dbo.Partners", t => t.PartnerId, cascadeDelete: true)
                .Index(t => t.PartnerId);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Type = c.String(nullable: false),
                        SetOfPermissionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SetOfPermissions", t => t.SetOfPermissionId, cascadeDelete: true)
                .Index(t => t.SetOfPermissionId);
            
            CreateTable(
                "dbo.SetOfPermissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VaultCashes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        BankAccountId = c.Int(nullable: false),
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
            
            CreateTable(
                "dbo.TerminalWorkingHours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TerminalId = c.String(nullable: false),
                        Day = c.Int(nullable: false),
                        StartTime = c.Time(nullable: false, precision: 7),
                        EndTime = c.Time(nullable: false, precision: 7),
                        Terminal_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Terminals", t => t.Terminal_Id)
                .Index(t => t.Terminal_Id);
            
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        Description = c.String(),
                        IsShowDashboard = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserBankAccounts",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        BankAccount_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.BankAccount_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccount_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.BankAccount_Id);
            
            CreateTable(
                "dbo.PermissionUsers",
                c => new
                    {
                        Permission_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Permission_Id, t.User_Id })
                .ForeignKey("dbo.Permissions", t => t.Permission_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Permission_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserTerminals",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Terminal_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Terminal_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Terminals", t => t.Terminal_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Terminal_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Terminals", "BankAccount_Id", "dbo.BankAccounts");
            DropForeignKey("dbo.BankAccounts", "StateId", "dbo.States");
            DropForeignKey("dbo.Partners", "StateId", "dbo.States");
            DropForeignKey("dbo.PartnerContacts", "StateId", "dbo.States");
            DropForeignKey("dbo.PartnerContacts", "PartnerId", "dbo.Partners");
            DropForeignKey("dbo.PartnerContacts", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.TerminalWorkingHours", "Terminal_Id", "dbo.Terminals");
            DropForeignKey("dbo.VaultCashes", "Id", "dbo.Terminals");
            DropForeignKey("dbo.VaultCashes", "BankAccountId", "dbo.BankAccounts");
            DropForeignKey("dbo.UserTerminals", "Terminal_Id", "dbo.Terminals");
            DropForeignKey("dbo.UserTerminals", "User_Id", "dbo.Users");
            DropForeignKey("dbo.PermissionUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.PermissionUsers", "Permission_Id", "dbo.Permissions");
            DropForeignKey("dbo.Permissions", "SetOfPermissionId", "dbo.SetOfPermissions");
            DropForeignKey("dbo.Users", "PartnerId", "dbo.Partners");
            DropForeignKey("dbo.UserBankAccounts", "BankAccount_Id", "dbo.BankAccounts");
            DropForeignKey("dbo.UserBankAccounts", "User_Id", "dbo.Users");
            DropForeignKey("dbo.TransactionStatistics", "Terminal_Id", "dbo.Terminals");
            DropForeignKey("dbo.TerminalPictures", "TerminalId", "dbo.Terminals");
            DropForeignKey("dbo.TerminalContacts", "TerminalId", "dbo.Terminals");
            DropForeignKey("dbo.TerminalAlerts", "Terminal_Id", "dbo.Terminals");
            DropForeignKey("dbo.TerminalAlertConfigs", "Id", "dbo.Terminals");
            DropForeignKey("dbo.Surcharges", "TerminalId", "dbo.Terminals");
            DropForeignKey("dbo.Surcharges", "BankAccountId", "dbo.BankAccounts");
            DropForeignKey("dbo.Terminals", "StateId", "dbo.States");
            DropForeignKey("dbo.Terminals", "PartnerId", "dbo.Partners");
            DropForeignKey("dbo.Notes", "TerminalId", "dbo.Terminals");
            DropForeignKey("dbo.Terminals", "MakeId", "dbo.Makes");
            DropForeignKey("dbo.Terminals", "ModelId", "dbo.Models");
            DropForeignKey("dbo.Models", "MakeId", "dbo.Makes");
            DropForeignKey("dbo.Terminals", "LocationTypeId", "dbo.LocationTypes");
            DropForeignKey("dbo.LoadCashes", "TerminalId", "dbo.Terminals");
            DropForeignKey("dbo.Terminals", "LastTransactionId", "dbo.TransactionStatistics");
            DropForeignKey("dbo.TransactionStatistics", "TerminalId", "dbo.Terminals");
            DropForeignKey("dbo.InterChanges", "TerminalId", "dbo.Terminals");
            DropForeignKey("dbo.InterChanges", "BankAccountId", "dbo.BankAccounts");
            DropForeignKey("dbo.Events", "TerminalId", "dbo.Terminals");
            DropForeignKey("dbo.Documents", "TerminalId", "dbo.Terminals");
            DropForeignKey("dbo.Disputes", "Terminal_Id", "dbo.Terminals");
            DropForeignKey("dbo.DisputeRepresents", "Id", "dbo.Disputes");
            DropForeignKey("dbo.CryptoCurrencyTransactions", "TerminalId", "dbo.Terminals");
            DropForeignKey("dbo.CryptoChargeAccounts", "TerminalId", "dbo.Terminals");
            DropForeignKey("dbo.CryptoChargeAccounts", "BankAccountId", "dbo.BankAccounts");
            DropForeignKey("dbo.Terminals", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Contracts", "TerminalId", "dbo.Terminals");
            DropForeignKey("dbo.Terminals", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Cassettes", "TerminalId", "dbo.Terminals");
            DropForeignKey("dbo.BindedKeys", "Id", "dbo.Terminals");
            DropForeignKey("dbo.TerminalContacts", "StateId", "dbo.States");
            DropForeignKey("dbo.TerminalContacts", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.TerminalContacts", "ContactTypeId", "dbo.ContactTypes");
            DropForeignKey("dbo.TerminalContacts", "CityId", "dbo.Cities");
            DropForeignKey("dbo.PartnerContacts", "ContactTypeId", "dbo.ContactTypes");
            DropForeignKey("dbo.PartnerContacts", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Partners", "ParentId", "dbo.Partners");
            DropForeignKey("dbo.Partners", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Partners", "CityId", "dbo.Cities");
            DropForeignKey("dbo.BankAccounts", "PartnerId", "dbo.Partners");
            DropForeignKey("dbo.BankAccounts", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.BankAccounts", "CityId", "dbo.Cities");
            DropForeignKey("dbo.States", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Cities", "StateId", "dbo.States");
            DropIndex("dbo.UserTerminals", new[] { "Terminal_Id" });
            DropIndex("dbo.UserTerminals", new[] { "User_Id" });
            DropIndex("dbo.PermissionUsers", new[] { "User_Id" });
            DropIndex("dbo.PermissionUsers", new[] { "Permission_Id" });
            DropIndex("dbo.UserBankAccounts", new[] { "BankAccount_Id" });
            DropIndex("dbo.UserBankAccounts", new[] { "User_Id" });
            DropIndex("dbo.TerminalWorkingHours", new[] { "Terminal_Id" });
            DropIndex("dbo.VaultCashes", new[] { "BankAccountId" });
            DropIndex("dbo.VaultCashes", new[] { "Id" });
            DropIndex("dbo.Permissions", new[] { "SetOfPermissionId" });
            DropIndex("dbo.Users", new[] { "PartnerId" });
            DropIndex("dbo.TerminalPictures", new[] { "TerminalId" });
            DropIndex("dbo.TerminalAlerts", new[] { "Terminal_Id" });
            DropIndex("dbo.TerminalAlertConfigs", new[] { "Id" });
            DropIndex("dbo.Surcharges", new[] { "BankAccountId" });
            DropIndex("dbo.Surcharges", new[] { "TerminalId" });
            DropIndex("dbo.Notes", new[] { "TerminalId" });
            DropIndex("dbo.Models", new[] { "MakeId" });
            DropIndex("dbo.LoadCashes", new[] { "TerminalId" });
            DropIndex("dbo.TransactionStatistics", new[] { "Terminal_Id" });
            DropIndex("dbo.TransactionStatistics", new[] { "TerminalId" });
            DropIndex("dbo.InterChanges", new[] { "BankAccountId" });
            DropIndex("dbo.InterChanges", new[] { "TerminalId" });
            DropIndex("dbo.Events", new[] { "TerminalId" });
            DropIndex("dbo.Documents", new[] { "TerminalId" });
            DropIndex("dbo.DisputeRepresents", new[] { "Id" });
            DropIndex("dbo.Disputes", new[] { "Terminal_Id" });
            DropIndex("dbo.CryptoCurrencyTransactions", new[] { "TerminalId" });
            DropIndex("dbo.CryptoChargeAccounts", new[] { "BankAccountId" });
            DropIndex("dbo.CryptoChargeAccounts", new[] { "TerminalId" });
            DropIndex("dbo.Contracts", new[] { "TerminalId" });
            DropIndex("dbo.Cassettes", new[] { "TerminalId" });
            DropIndex("dbo.BindedKeys", new[] { "Id" });
            DropIndex("dbo.Terminals", new[] { "BankAccount_Id" });
            DropIndex("dbo.Terminals", new[] { "LastTransactionId" });
            DropIndex("dbo.Terminals", new[] { "ModelId" });
            DropIndex("dbo.Terminals", new[] { "MakeId" });
            DropIndex("dbo.Terminals", new[] { "LocationTypeId" });
            DropIndex("dbo.Terminals", new[] { "CityId" });
            DropIndex("dbo.Terminals", new[] { "StateId" });
            DropIndex("dbo.Terminals", new[] { "CountryId" });
            DropIndex("dbo.Terminals", new[] { "PartnerId" });
            DropIndex("dbo.TerminalContacts", new[] { "CityId" });
            DropIndex("dbo.TerminalContacts", new[] { "StateId" });
            DropIndex("dbo.TerminalContacts", new[] { "CountryId" });
            DropIndex("dbo.TerminalContacts", new[] { "ContactTypeId" });
            DropIndex("dbo.TerminalContacts", new[] { "TerminalId" });
            DropIndex("dbo.PartnerContacts", new[] { "CityId" });
            DropIndex("dbo.PartnerContacts", new[] { "StateId" });
            DropIndex("dbo.PartnerContacts", new[] { "CountryId" });
            DropIndex("dbo.PartnerContacts", new[] { "ContactTypeId" });
            DropIndex("dbo.PartnerContacts", new[] { "PartnerId" });
            DropIndex("dbo.Partners", new[] { "CityId" });
            DropIndex("dbo.Partners", new[] { "StateId" });
            DropIndex("dbo.Partners", new[] { "CountryId" });
            DropIndex("dbo.Partners", new[] { "ParentId" });
            DropIndex("dbo.States", new[] { "CountryId" });
            DropIndex("dbo.Cities", new[] { "StateId" });
            DropIndex("dbo.BankAccounts", new[] { "PartnerId" });
            DropIndex("dbo.BankAccounts", new[] { "CityId" });
            DropIndex("dbo.BankAccounts", new[] { "StateId" });
            DropIndex("dbo.BankAccounts", new[] { "CountryId" });
            DropTable("dbo.UserTerminals");
            DropTable("dbo.PermissionUsers");
            DropTable("dbo.UserBankAccounts");
            DropTable("dbo.Reports");
            DropTable("dbo.TerminalWorkingHours");
            DropTable("dbo.VaultCashes");
            DropTable("dbo.SetOfPermissions");
            DropTable("dbo.Permissions");
            DropTable("dbo.Users");
            DropTable("dbo.TerminalPictures");
            DropTable("dbo.TerminalAlerts");
            DropTable("dbo.TerminalAlertConfigs");
            DropTable("dbo.Surcharges");
            DropTable("dbo.Notes");
            DropTable("dbo.Models");
            DropTable("dbo.Makes");
            DropTable("dbo.LocationTypes");
            DropTable("dbo.LoadCashes");
            DropTable("dbo.TransactionStatistics");
            DropTable("dbo.InterChanges");
            DropTable("dbo.Events");
            DropTable("dbo.Documents");
            DropTable("dbo.DisputeRepresents");
            DropTable("dbo.Disputes");
            DropTable("dbo.CryptoCurrencyTransactions");
            DropTable("dbo.CryptoChargeAccounts");
            DropTable("dbo.Contracts");
            DropTable("dbo.Cassettes");
            DropTable("dbo.BindedKeys");
            DropTable("dbo.Terminals");
            DropTable("dbo.TerminalContacts");
            DropTable("dbo.ContactTypes");
            DropTable("dbo.PartnerContacts");
            DropTable("dbo.Partners");
            DropTable("dbo.Countries");
            DropTable("dbo.States");
            DropTable("dbo.Cities");
            DropTable("dbo.BankAccounts");
        }
    }
}
