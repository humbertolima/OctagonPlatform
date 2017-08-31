namespace OctagonPlatform.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ScriptSetOfPermisson : DbMigration
    {
        public override void Up()
        {
            Sql("SET ANSI_WARNINGS  OFF;");                     //Evitar que se trunque el string.

            Sql("SET IDENTITY_INSERT SetOfPermissions ON");     //Permite adicionar id en campo indexado       
            Sql("INSERT INTO SetOfPermissions (Id, [Name]) VALUES (1, 'Partner Managment'),(2, 'PartnerContact Managment'), (3, 'Terminal Managment'), (4, 'Users Managment'), (5, 'Bank Account Managment'),(6, 'Report Managment'),(7, 'Terminal Contact MAnagment')");
            Sql("SET IDENTITY_INSERT SetOfPermissions OFF");

            Sql("SET IDENTITY_INSERT Permissions ON");
            Sql("INSERT INTO Permissions (Id, [Name], SetOfPermissionId) " +
                "VALUES (1, 'Create Partner','1'),(2, 'Edit Partner','1'), (3, 'View Partner Details','1'), (4, 'Delete Partner','1'), " +
                "       (5, 'Create PartnerContact','2'),(6, 'Edit PartnerContact','2'), (7, 'View PartnerContact Details','2'), (8, 'Delete PartnerContact','2'), " +
                "       (9, 'Create User','4'),(10, 'Edit User','4'),(11, 'View User Details','4'),(12, 'Delete User','4'), " +
                "       (13, 'Create Bank Account','5'),(14, 'Edit Bank Account','5'), (15, 'View Bank Account Details','5'), (16, 'Delete Bank Account','5'), " +
                "       (17, 'Create Terminal','3'),(18, 'Edit Terminal','3'),(19, 'View Terminal Details','3'),(20, 'Delete Terminal','3'), " +
                "       (21, 'Create TerminalContact','7'),(22, 'Edit TerminalContact','7'),(23, 'View TerminalContact Details','7'),(24, 'Delete Terminal','7'), " +
                "       (25, 'Create Report','6'), (26, 'Edit Report','6'), (27, 'View Report Detail','6'), (28, 'Delete Report','6')  "
                );
            Sql("SET IDENTITY_INSERT Permissions OFF");

            Sql("SET ANSI_WARNINGS  ON;");
        }

        public override void Down()
        {
            Sql("DELETE * FROM Permissions");
            Sql("DELETE * FROM SetOfPermissions");
        }
    }
}
