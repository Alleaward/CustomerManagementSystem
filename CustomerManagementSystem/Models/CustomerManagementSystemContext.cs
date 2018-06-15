namespace CustomerManagementSystem.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class CustomerManagementSystemContext : DbContext
    {
        // Your context has been configured to use a 'CustomerManagementSystemContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CustomerManagementSystem.Models.CustomerManagementSystemContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'CustomerManagementSystemContext' 
        // connection string in the application configuration file.
        public CustomerManagementSystemContext()
            : base("name=CustomerManagementSystemContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<BusinessAccount> BusinessAccounts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}