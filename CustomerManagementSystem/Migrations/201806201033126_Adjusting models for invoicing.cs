namespace CustomerManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adjustingmodelsforinvoicing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "BusinessNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "BusinessNumber");
        }
    }
}
