namespace CustomerManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusinessAccounts",
                c => new
                    {
                        BusinessNumber = c.Int(nullable: false, identity: true),
                        UserAccount = c.String(nullable: false),
                        BusinessName = c.String(nullable: false),
                        BusinessOwner = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Website = c.String(),
                        Logo = c.String(),
                        ABN = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BusinessNumber);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        BusinessNumber = c.Int(nullable: false),
                        CustomerName = c.String(nullable: false),
                        CustomerAddress = c.String(nullable: false),
                        CustomerPhoneNumber = c.String(nullable: false),
                        CustomerEmail = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId)
                .ForeignKey("dbo.BusinessAccounts", t => t.BusinessNumber, cascadeDelete: true)
                .Index(t => t.BusinessNumber);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        InvoiceNumber = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(nullable: false),
                        invoiceComplete = c.Boolean(nullable: false),
                        BusinessNumber = c.Int(nullable: false),
                        BusinessName = c.String(),
                        BusinessOwner = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        Website = c.String(),
                        Logo = c.String(),
                        ABN = c.String(),
                        CustomerId = c.Int(nullable: false),
                        CustomerName = c.String(),
                        CustomerAddress = c.String(),
                        CustomerPhone = c.String(),
                        CustomerEmail = c.String(),
                        Notes = c.String(),
                        Tax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.InvoiceNumber)
                .ForeignKey("dbo.BusinessAccounts", t => t.BusinessNumber, cascadeDelete: true)
                .Index(t => t.BusinessNumber);
            
            CreateTable(
                "dbo.InvoiceItems",
                c => new
                    {
                        InvoiceItemId = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        ItemName = c.String(),
                        ItemId = c.Int(nullable: false),
                        ItemQuantity = c.Int(nullable: false),
                        Invoice_InvoiceNumber = c.Int(),
                    })
                .PrimaryKey(t => t.InvoiceItemId)
                .ForeignKey("dbo.Invoices", t => t.Invoice_InvoiceNumber)
                .Index(t => t.Invoice_InvoiceNumber);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemNumber = c.Int(nullable: false, identity: true),
                        BusinessNumber = c.Int(nullable: false),
                        ItemName = c.String(nullable: false),
                        ItemDescription = c.String(),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ItemNumber);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "BusinessNumber", "dbo.BusinessAccounts");
            DropForeignKey("dbo.InvoiceItems", "Invoice_InvoiceNumber", "dbo.Invoices");
            DropForeignKey("dbo.Customers", "BusinessNumber", "dbo.BusinessAccounts");
            DropIndex("dbo.InvoiceItems", new[] { "Invoice_InvoiceNumber" });
            DropIndex("dbo.Invoices", new[] { "BusinessNumber" });
            DropIndex("dbo.Customers", new[] { "BusinessNumber" });
            DropTable("dbo.Items");
            DropTable("dbo.InvoiceItems");
            DropTable("dbo.Invoices");
            DropTable("dbo.Customers");
            DropTable("dbo.BusinessAccounts");
        }
    }
}
