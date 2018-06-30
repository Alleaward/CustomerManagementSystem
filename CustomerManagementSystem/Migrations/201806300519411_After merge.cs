namespace CustomerManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Aftermerge : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "BusinessNumber", "dbo.BusinessAccounts");
            DropIndex("dbo.Customers", new[] { "BusinessNumber" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Customers", "BusinessNumber");
            AddForeignKey("dbo.Customers", "BusinessNumber", "dbo.BusinessAccounts", "BusinessNumber", cascadeDelete: true);
        }
    }
}
