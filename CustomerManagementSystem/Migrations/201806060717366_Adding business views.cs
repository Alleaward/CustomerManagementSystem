namespace CustomerManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addingbusinessviews : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invoices", "BusinessAccount_BusinessNumber", "dbo.BusinessAccounts");
            DropIndex("dbo.Invoices", new[] { "BusinessAccount_BusinessNumber" });
            RenameColumn(table: "dbo.Invoices", name: "BusinessAccount_BusinessNumber", newName: "BusinessNumber");
            AddColumn("dbo.Invoices", "Tax", c => c.Int(nullable: false));
            AlterColumn("dbo.Invoices", "BusinessNumber", c => c.Int(nullable: false));
            CreateIndex("dbo.Invoices", "BusinessNumber");
            AddForeignKey("dbo.Invoices", "BusinessNumber", "dbo.BusinessAccounts", "BusinessNumber", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "BusinessNumber", "dbo.BusinessAccounts");
            DropIndex("dbo.Invoices", new[] { "BusinessNumber" });
            AlterColumn("dbo.Invoices", "BusinessNumber", c => c.Int());
            DropColumn("dbo.Invoices", "Tax");
            RenameColumn(table: "dbo.Invoices", name: "BusinessNumber", newName: "BusinessAccount_BusinessNumber");
            CreateIndex("dbo.Invoices", "BusinessAccount_BusinessNumber");
            AddForeignKey("dbo.Invoices", "BusinessAccount_BusinessNumber", "dbo.BusinessAccounts", "BusinessNumber");
        }
    }
}
