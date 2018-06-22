namespace CustomerManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedverificationforsomeclasses : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BusinessAccounts", "UserAccount", c => c.String(nullable: false));
            AlterColumn("dbo.BusinessAccounts", "BusinessName", c => c.String(nullable: false));
            AlterColumn("dbo.BusinessAccounts", "BusinessOwner", c => c.String(nullable: false));
            AlterColumn("dbo.BusinessAccounts", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.BusinessAccounts", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.BusinessAccounts", "ABN", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "CustomerName", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "CustomerAddress", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "CustomerPhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "CustomerEmail", c => c.String(nullable: false));
            AlterColumn("dbo.Items", "ItemName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Items", "ItemName", c => c.String());
            AlterColumn("dbo.Customers", "CustomerEmail", c => c.String());
            AlterColumn("dbo.Customers", "CustomerPhoneNumber", c => c.String());
            AlterColumn("dbo.Customers", "CustomerAddress", c => c.String());
            AlterColumn("dbo.Customers", "CustomerName", c => c.String());
            AlterColumn("dbo.BusinessAccounts", "ABN", c => c.String());
            AlterColumn("dbo.BusinessAccounts", "Email", c => c.String());
            AlterColumn("dbo.BusinessAccounts", "PhoneNumber", c => c.String());
            AlterColumn("dbo.BusinessAccounts", "BusinessOwner", c => c.String());
            AlterColumn("dbo.BusinessAccounts", "BusinessName", c => c.String());
            AlterColumn("dbo.BusinessAccounts", "UserAccount", c => c.String());
        }
    }
}
