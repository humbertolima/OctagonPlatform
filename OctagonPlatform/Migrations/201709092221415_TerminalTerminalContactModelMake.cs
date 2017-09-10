namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class TerminalTerminalContactModelMake : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TerminalContacts", "ContactType_Id", "dbo.ContactTypes");
            DropForeignKey("dbo.Terminals", "Partner_Id", "dbo.Partners");
            DropIndex("dbo.Terminals", new[] { "Partner_Id" });
            DropIndex("dbo.TerminalContacts", new[] { "ContactType_Id" });
            RenameColumn(table: "dbo.TerminalContacts", name: "ContactType_Id", newName: "ContactTypeId");
            RenameColumn(table: "dbo.Terminals", name: "Partner_Id", newName: "PartnerId");
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
            
            AddColumn("dbo.Terminals", "CountryId", c => c.Int(nullable: false));
            AddColumn("dbo.Terminals", "StateId", c => c.Int(nullable: false));
            AddColumn("dbo.Terminals", "CityId", c => c.Int(nullable: false));
            AddColumn("dbo.Terminals", "LocationTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Terminals", "MakeId", c => c.Int(nullable: false));
            AddColumn("dbo.Terminals", "ModelId", c => c.Int(nullable: false));
            AddColumn("dbo.Terminals", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.Terminals", "CommunicationType", c => c.Int(nullable: false));
            AddColumn("dbo.Terminals", "Address1", c => c.String(nullable: false));
            AddColumn("dbo.Terminals", "Address2", c => c.String());
            AddColumn("dbo.Terminals", "Email", c => c.String(nullable: false, maxLength: 16));
            AddColumn("dbo.Terminals", "Phone", c => c.String(nullable: false));
            AddColumn("dbo.Terminals", "Zip", c => c.Int(nullable: false));
            AddColumn("dbo.Terminals", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.Terminals", "CreatedBy", c => c.Int());
            AddColumn("dbo.Terminals", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.Terminals", "DeletedBy", c => c.Int());
            AddColumn("dbo.Terminals", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.Terminals", "UpdatedBy", c => c.Int());
            AddColumn("dbo.Terminals", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.TerminalContacts", "Name", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.TerminalContacts", "LastName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.TerminalContacts", "TerminalId", c => c.Int(nullable: false));
            AddColumn("dbo.TerminalContacts", "Address1", c => c.String(nullable: false));
            AddColumn("dbo.TerminalContacts", "Address2", c => c.String());
            AddColumn("dbo.TerminalContacts", "CountryId", c => c.Int(nullable: false));
            AddColumn("dbo.TerminalContacts", "StateId", c => c.Int(nullable: false));
            AddColumn("dbo.TerminalContacts", "CityId", c => c.Int(nullable: false));
            AddColumn("dbo.TerminalContacts", "Email", c => c.String(nullable: false, maxLength: 16));
            AddColumn("dbo.TerminalContacts", "Phone", c => c.String(nullable: false));
            AddColumn("dbo.TerminalContacts", "Zip", c => c.Int(nullable: false));
            AddColumn("dbo.TerminalContacts", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.TerminalContacts", "CreatedBy", c => c.Int());
            AddColumn("dbo.TerminalContacts", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.TerminalContacts", "DeletedBy", c => c.Int());
            AddColumn("dbo.TerminalContacts", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.TerminalContacts", "UpdatedBy", c => c.Int());
            AddColumn("dbo.TerminalContacts", "Deleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Terminals", "PartnerId", c => c.Int(nullable: false));
            AlterColumn("dbo.TerminalContacts", "ContactTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Terminals", "PartnerId");
            CreateIndex("dbo.Terminals", "CountryId");
            CreateIndex("dbo.Terminals", "StateId");
            CreateIndex("dbo.Terminals", "CityId");
            CreateIndex("dbo.Terminals", "LocationTypeId");
            CreateIndex("dbo.Terminals", "MakeId");
            CreateIndex("dbo.Terminals", "ModelId");
            CreateIndex("dbo.Terminals", "Email", unique: true);
            CreateIndex("dbo.TerminalContacts", "TerminalId");
            CreateIndex("dbo.TerminalContacts", "ContactTypeId");
            CreateIndex("dbo.TerminalContacts", "CountryId");
            CreateIndex("dbo.TerminalContacts", "CityId");
            CreateIndex("dbo.TerminalContacts", "Email", unique: true);
            AddForeignKey("dbo.Terminals", "CityId", "dbo.Cities", "Id");
            AddForeignKey("dbo.Terminals", "CountryId", "dbo.Countries", "Id");
            AddForeignKey("dbo.Terminals", "LocationTypeId", "dbo.LocationTypes", "Id");
            AddForeignKey("dbo.Terminals", "ModelId", "dbo.Models", "Id");
            AddForeignKey("dbo.Terminals", "MakeId", "dbo.Makes", "Id");
            AddForeignKey("dbo.Terminals", "StateId", "dbo.States", "Id");
            AddForeignKey("dbo.TerminalContacts", "CityId", "dbo.Cities", "Id");
            AddForeignKey("dbo.TerminalContacts", "CountryId", "dbo.Countries", "Id");
            AddForeignKey("dbo.TerminalContacts", "TerminalId", "dbo.Terminals", "Id", true);
            AddForeignKey("dbo.TerminalContacts", "ContactTypeId", "dbo.ContactTypes", "Id");
            AddForeignKey("dbo.Terminals", "PartnerId", "dbo.Partners", "Id", true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Terminals", "PartnerId", "dbo.Partners");
            DropForeignKey("dbo.TerminalContacts", "ContactTypeId", "dbo.ContactTypes");
            DropForeignKey("dbo.TerminalContacts", "TerminalId", "dbo.Terminals");
            DropForeignKey("dbo.TerminalContacts", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.TerminalContacts", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Terminals", "StateId", "dbo.States");
            DropForeignKey("dbo.Terminals", "MakeId", "dbo.Makes");
            DropForeignKey("dbo.Terminals", "ModelId", "dbo.Models");
            DropForeignKey("dbo.Models", "MakeId", "dbo.Makes");
            DropForeignKey("dbo.Terminals", "LocationTypeId", "dbo.LocationTypes");
            DropForeignKey("dbo.Terminals", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Terminals", "CityId", "dbo.Cities");
            DropIndex("dbo.TerminalContacts", new[] { "Email" });
            DropIndex("dbo.TerminalContacts", new[] { "CityId" });
            DropIndex("dbo.TerminalContacts", new[] { "CountryId" });
            DropIndex("dbo.TerminalContacts", new[] { "ContactTypeId" });
            DropIndex("dbo.TerminalContacts", new[] { "TerminalId" });
            DropIndex("dbo.Models", new[] { "MakeId" });
            DropIndex("dbo.Terminals", new[] { "Email" });
            DropIndex("dbo.Terminals", new[] { "ModelId" });
            DropIndex("dbo.Terminals", new[] { "MakeId" });
            DropIndex("dbo.Terminals", new[] { "LocationTypeId" });
            DropIndex("dbo.Terminals", new[] { "CityId" });
            DropIndex("dbo.Terminals", new[] { "StateId" });
            DropIndex("dbo.Terminals", new[] { "CountryId" });
            DropIndex("dbo.Terminals", new[] { "PartnerId" });
            AlterColumn("dbo.TerminalContacts", "ContactTypeId", c => c.Int());
            AlterColumn("dbo.Terminals", "PartnerId", c => c.Int());
            DropColumn("dbo.TerminalContacts", "Deleted");
            DropColumn("dbo.TerminalContacts", "UpdatedBy");
            DropColumn("dbo.TerminalContacts", "UpdatedAt");
            DropColumn("dbo.TerminalContacts", "DeletedBy");
            DropColumn("dbo.TerminalContacts", "DeletedAt");
            DropColumn("dbo.TerminalContacts", "CreatedBy");
            DropColumn("dbo.TerminalContacts", "CreatedAt");
            DropColumn("dbo.TerminalContacts", "Zip");
            DropColumn("dbo.TerminalContacts", "Phone");
            DropColumn("dbo.TerminalContacts", "Email");
            DropColumn("dbo.TerminalContacts", "CityId");
            DropColumn("dbo.TerminalContacts", "StateId");
            DropColumn("dbo.TerminalContacts", "CountryId");
            DropColumn("dbo.TerminalContacts", "Address2");
            DropColumn("dbo.TerminalContacts", "Address1");
            DropColumn("dbo.TerminalContacts", "TerminalId");
            DropColumn("dbo.TerminalContacts", "LastName");
            DropColumn("dbo.TerminalContacts", "Name");
            DropColumn("dbo.Terminals", "Deleted");
            DropColumn("dbo.Terminals", "UpdatedBy");
            DropColumn("dbo.Terminals", "UpdatedAt");
            DropColumn("dbo.Terminals", "DeletedBy");
            DropColumn("dbo.Terminals", "DeletedAt");
            DropColumn("dbo.Terminals", "CreatedBy");
            DropColumn("dbo.Terminals", "CreatedAt");
            DropColumn("dbo.Terminals", "Zip");
            DropColumn("dbo.Terminals", "Phone");
            DropColumn("dbo.Terminals", "Email");
            DropColumn("dbo.Terminals", "Address2");
            DropColumn("dbo.Terminals", "Address1");
            DropColumn("dbo.Terminals", "CommunicationType");
            DropColumn("dbo.Terminals", "Status");
            DropColumn("dbo.Terminals", "ModelId");
            DropColumn("dbo.Terminals", "MakeId");
            DropColumn("dbo.Terminals", "LocationTypeId");
            DropColumn("dbo.Terminals", "CityId");
            DropColumn("dbo.Terminals", "StateId");
            DropColumn("dbo.Terminals", "CountryId");
            DropTable("dbo.Models");
            DropTable("dbo.Makes");
            DropTable("dbo.LocationTypes");
            RenameColumn(table: "dbo.Terminals", name: "PartnerId", newName: "Partner_Id");
            RenameColumn(table: "dbo.TerminalContacts", name: "ContactTypeId", newName: "ContactType_Id");
            CreateIndex("dbo.TerminalContacts", "ContactType_Id");
            CreateIndex("dbo.Terminals", "Partner_Id");
            AddForeignKey("dbo.Terminals", "Partner_Id", "dbo.Partners", "Id");
            AddForeignKey("dbo.TerminalContacts", "ContactType_Id", "dbo.ContactTypes", "Id");
        }
    }
}
