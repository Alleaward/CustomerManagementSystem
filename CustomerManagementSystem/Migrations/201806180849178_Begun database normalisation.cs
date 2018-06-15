namespace CustomerManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Begundatabasenormalisation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "Invoice_InvoiceNumber", "dbo.Invoices");
            DropIndex("dbo.Items", new[] { "Invoice_InvoiceNumber" });
            CreateTable(
                "dbo.InvoiceItems",
                c => new
                    {
                        InvoiceItemId = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        ItemQuantity = c.Int(nullable: false),
                        Invoice_InvoiceNumber = c.Int(),
                    })
                .PrimaryKey(t => t.InvoiceItemId)
                .ForeignKey("dbo.Invoices", t => t.Invoice_InvoiceNumber)
                .Index(t => t.Invoice_InvoiceNumber);
            
            DropColumn("dbo.Invoices", "BusinessName");
            DropColumn("dbo.Invoices", "BusinessOwner");
            DropColumn("dbo.Invoices", "PhoneNumber");
            DropColumn("dbo.Invoices", "Email");
            DropColumn("dbo.Invoices", "Website");
            DropColumn("dbo.Invoices", "Logo");
            DropColumn("dbo.Invoices", "ABN");
            DropColumn("dbo.Invoices", "CustomerName");
            DropColumn("dbo.Invoices", "CustomerAddress");
            DropColumn("dbo.Invoices", "CustomerPhone");
            DropColumn("dbo.Invoices", "CustomerEmail");
            DropColumn("dbo.Items", "Invoice_InvoiceNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "Invoice_InvoiceNumber", c => c.Int());
            AddColumn("dbo.Invoices", "CustomerEmail", c => c.String());
            AddColumn("dbo.Invoices", "CustomerPhone", c => c.String());
            AddColumn("dbo.Invoices", "CustomerAddress", c => c.String());
            AddColumn("dbo.Invoices", "CustomerName", c => c.String());
            AddColumn("dbo.Invoices", "ABN", c => c.String());
            AddColumn("dbo.Invoices", "Logo", c => c.String());
            AddColumn("dbo.Invoices", "Website", c => c.String());
            AddColumn("dbo.Invoices", "Email", c => c.String());
            AddColumn("dbo.Invoices", "PhoneNumber", c => c.String());
            AddColumn("dbo.Invoices", "BusinessOwner", c => c.String());
            AddColumn("dbo.Invoices", "BusinessName", c => c.String());
            DropForeignKey("dbo.InvoiceItems", "Invoice_InvoiceNumber", "dbo.Invoices");
            DropIndex("dbo.InvoiceItems", new[] { "Invoice_InvoiceNumber" });
            DropTable("dbo.InvoiceItems");
            CreateIndex("dbo.Items", "Invoice_InvoiceNumber");
            AddForeignKey("dbo.Items", "Invoice_InvoiceNumber", "dbo.Invoices", "InvoiceNumber");
        }
    }
}
