namespace CustomerManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingItemsandCustomers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        BusinessNumber = c.Int(nullable: false),
                        CustomerName = c.String(),
                        CustomerAddress = c.String(),
                        CustomerPhoneNumber = c.String(),
                        CustomerEmail = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            AddColumn("dbo.Invoices", "CustomerId", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "Cost", c => c.Int(nullable: false));
            DropColumn("dbo.Items", "Quantity");
            DropColumn("dbo.Items", "CostPerUnit");
            DropColumn("dbo.Items", "TotalCost");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "TotalCost", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "CostPerUnit", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.Items", "Cost");
            DropColumn("dbo.Invoices", "CustomerId");
            DropTable("dbo.Customers");
        }
    }
}
