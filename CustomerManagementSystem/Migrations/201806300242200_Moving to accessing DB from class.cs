namespace CustomerManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovingtoaccessingDBfromclass : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Customers", "BusinessNumber");
            AddForeignKey("dbo.Customers", "BusinessNumber", "dbo.BusinessAccounts", "BusinessNumber", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "BusinessNumber", "dbo.BusinessAccounts");
            DropIndex("dbo.Customers", new[] { "BusinessNumber" });
        }
    }
}
