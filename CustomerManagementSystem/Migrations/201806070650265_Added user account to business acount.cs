namespace CustomerManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addeduseraccounttobusinessacount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BusinessAccounts", "UserAccount", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BusinessAccounts", "UserAccount");
        }
    }
}
