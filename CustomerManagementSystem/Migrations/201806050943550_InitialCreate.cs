namespace CustomerManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        InvoiceNumber = c.Int(nullable: false, identity: true),
                        BusinessName = c.String(),
                        BusinessOwner = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        Website = c.String(),
                        Logo = c.String(),
                        ABN = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        CustomerName = c.String(),
                        CustomerAddress = c.String(),
                        Notes = c.String(),
                        SubTotal = c.Int(nullable: false),
                        TotalCost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InvoiceNumber);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemNumber = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        ItemDescription = c.String(),
                        Quantity = c.Int(nullable: false),
                        CostPerUnit = c.Int(nullable: false),
                        TotalCost = c.Int(nullable: false),
                        Invoice_InvoiceNumber = c.Int(),
                    })
                .PrimaryKey(t => t.ItemNumber)
                .ForeignKey("dbo.Invoices", t => t.Invoice_InvoiceNumber)
                .Index(t => t.Invoice_InvoiceNumber);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "Invoice_InvoiceNumber", "dbo.Invoices");
            DropIndex("dbo.Items", new[] { "Invoice_InvoiceNumber" });
            DropTable("dbo.Items");
            DropTable("dbo.Invoices");
        }
    }
}
