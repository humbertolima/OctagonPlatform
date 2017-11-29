namespace OctagonPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScriptSetOfPermisson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Permissions", "Type", c => c.String(nullable: false));

            Sql("SET ANSI_WARNINGS  OFF;");                     //Evitar que se trunque el string.

            Sql("SET IDENTITY_INSERT SetOfPermissions ON");     //Permite adicionar id en campo indexado       
            Sql("INSERT INTO SetOfPermissions (Id, [Name]) VALUES (1, 'Partner Managment'),(2, 'PartnerContact Managment'), (3, 'Terminal Managment'), (4, 'Users Managment'), (5, 'Bank Account Managment'),(6, 'Report Managment'),(7, 'Terminal Contact MAnagment')");
            Sql("SET IDENTITY_INSERT SetOfPermissions OFF");

            Sql("SET IDENTITY_INSERT Permissions ON");
            Sql("INSERT INTO Permissions (Id, [Name], [Type], SetOfPermissionId) " +
                "VALUES (1, 'Create Partner','Create','1'),(2, 'Edit Partner','Edit','1'), (3, 'View Partner Details','View','1'), (4, 'Delete Partner','Delete','1'), " +
                "       (5, 'Create PartnerContact','Create','2'),(6, 'Edit PartnerContact','Edit','2'), (7, 'View PartnerContact Details','View','2'), (8, 'Delete PartnerContact','Delete','2'), " +
                "       (9, 'Create User','Create','4'),(10, 'Edit User','Edit','4'),(11, 'View User Details','View','4'),(12, 'Delete User','Delete','4'), " +
                "       (13, 'Create Bank Account','Create','5'),(14, 'Edit Bank Account','Edit','5'), (15, 'View Bank Account Details','View','5'), (16, 'Delete Bank Account','Delete','5'), " +
                "       (17, 'Create Terminal','Create','3'),(18, 'Edit Terminal','Edit','3'),(19, 'View Terminal Details','View','3'),(20, 'Delete Terminal','Delete','3'), " +
                "       (21, 'Create TerminalContact','Create','7'),(22, 'Edit TerminalContact','Edit','7'),(23, 'View TerminalContact Details','View','7'),(24, 'Delete Terminal','Delete','7'), " +
                "       (25, 'Create Report','Create','6'), (26, 'Edit Report','Edit','6'), (27, 'View Report Detail','View','6'), (28, 'Delete Report','Delete','6')  "
                );
            Sql("SET IDENTITY_INSERT Permissions OFF");

            Sql("SET ANSI_WARNINGS  ON;");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Permissions", "Type");

        }
    }
}
