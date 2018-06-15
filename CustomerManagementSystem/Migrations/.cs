namespace CustomerManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Begundatabasenormalisation2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "BusinessName", c => c.String());
            AddColumn("dbo.Invoices", "BusinessOwner", c => c.String());
            AddColumn("dbo.Invoices", "PhoneNumber", c => c.String());
            AddColumn("dbo.Invoices", "Email", c => c.String());
            AddColumn("dbo.Invoices", "Website", c => c.String());
            AddColumn("dbo.Invoices", "Logo", c => c.String());
            AddColumn("dbo.Invoices", "ABN", c => c.String());
            AddColumn("dbo.Invoices", "CustomerName", c => c.String());
            AddColumn("dbo.Invoices", "CustomerAddress", c => c.String());
            AddColumn("dbo.Invoices", "CustomerPhone", c => c.String());
            AddColumn("dbo.Invoices", "CustomerEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "CustomerEmail");
            DropColumn("dbo.Invoices", "CustomerPhone");
            DropColumn("dbo.Invoices", "CustomerAddress");
            DropColumn("dbo.Invoices", "CustomerName");
            DropColumn("dbo.Invoices", "ABN");
            DropColumn("dbo.Invoices", "Logo");
            DropColumn("dbo.Invoices", "Website");
            DropColumn("dbo.Invoices", "Email");
            DropColumn("dbo.Invoices", "PhoneNumber");
            DropColumn("dbo.Invoices", "BusinessOwner");
            DropColumn("dbo.Invoices", "BusinessName");
        }
    }
}
