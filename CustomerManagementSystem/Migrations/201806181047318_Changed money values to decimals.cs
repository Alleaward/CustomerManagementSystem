namespace CustomerManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changedmoneyvaluestodecimals : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invoices", "Tax", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "SubTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "TotalCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Items", "Cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Items", "Cost", c => c.Int(nullable: false));
            AlterColumn("dbo.Invoices", "TotalCost", c => c.Int(nullable: false));
            AlterColumn("dbo.Invoices", "SubTotal", c => c.Int(nullable: false));
            AlterColumn("dbo.Invoices", "Tax", c => c.Int(nullable: false));
        }
    }
}
