using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Web.Mvc;

namespace CustomerManagementSystem.Models
{

    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public int BusinessNumber { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerAddress { get; set; }
        [Required]
        public string CustomerPhoneNumber { get; set; }
        [Required]
        public string CustomerEmail { get; set; }

        public Customer()
        {

        }

        public Customer(int BusinessNumber, string CustomerName, string CustomerAddress, string CustomerPhoneNumber, string CustomerEmail)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var customer = new Customer();
                this.BusinessNumber = BusinessNumber;
                this.CustomerName = CustomerName;
                this.CustomerAddress = CustomerAddress;
                this.CustomerPhoneNumber = CustomerPhoneNumber;
                this.CustomerEmail = CustomerEmail;
                context.Customers.Add(this);
                context.SaveChanges();
            }
        }

        public Customer([Optional]int id)
        {
            this.CustomerId = id;
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var customer = context.Customers.Where(x => x.CustomerId == id).First();

                this.BusinessNumber = customer.BusinessNumber;
                this.CustomerName = customer.CustomerName;
                this.CustomerAddress = customer.CustomerAddress;
                this.CustomerPhoneNumber = customer.CustomerPhoneNumber;
                this.CustomerEmail = customer.CustomerEmail;
            }
        }

        public static void AddCustomer(FormCollection collection, int id){
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var customer = new Customer(
                    BusinessNumber: id,
                    CustomerName: collection["CustomerName"],
                    CustomerAddress: collection["CustomerAddress"],
                    CustomerPhoneNumber: collection["CustomerPhoneNumber"],
                    CustomerEmail: collection["CustomerEmail"]
                );
            }
        }
    }
}