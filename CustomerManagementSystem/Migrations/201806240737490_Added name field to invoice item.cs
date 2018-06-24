namespace CustomerManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addednamefieldtoinvoiceitem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvoiceItems", "ItemName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvoiceItems", "ItemName");
        }
    }
}
