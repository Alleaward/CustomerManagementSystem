namespace CustomerManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedcompletedflagforinvoice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "invoiceComplete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "invoiceComplete");
        }
    }
}
