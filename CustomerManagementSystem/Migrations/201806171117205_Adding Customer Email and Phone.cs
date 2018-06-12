namespace CustomerManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCustomerEmailandPhone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "CustomerPhone", c => c.String());
            AddColumn("dbo.Invoices", "CustomerEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "CustomerEmail");
            DropColumn("dbo.Invoices", "CustomerPhone");
        }
    }
}
