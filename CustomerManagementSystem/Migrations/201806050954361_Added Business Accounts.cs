namespace CustomerManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBusinessAccounts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusinessAccounts",
                c => new
                    {
                        BusinessNumber = c.Int(nullable: false, identity: true),
                        BusinessName = c.String(),
                        BusinessOwner = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        Website = c.String(),
                        Logo = c.String(),
                        ABN = c.String(),
                    })
                .PrimaryKey(t => t.BusinessNumber);
            
            AddColumn("dbo.Invoices", "BusinessAccount_BusinessNumber", c => c.Int());
            CreateIndex("dbo.Invoices", "BusinessAccount_BusinessNumber");
            AddForeignKey("dbo.Invoices", "BusinessAccount_BusinessNumber", "dbo.BusinessAccounts", "BusinessNumber");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "BusinessAccount_BusinessNumber", "dbo.BusinessAccounts");
            DropIndex("dbo.Invoices", new[] { "BusinessAccount_BusinessNumber" });
            DropColumn("dbo.Invoices", "BusinessAccount_BusinessNumber");
            DropTable("dbo.BusinessAccounts");
        }
    }
}
