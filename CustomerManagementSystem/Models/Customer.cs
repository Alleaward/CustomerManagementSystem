using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace CustomerManagementSystem.Models
{

    public class Customer
    {
        public Customer()
        {

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
        //Foreign Key from "BusinessAccounts"
        public int BusinessNumber { get; set; }

        [Key]
        public int CustomerId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerAddress { get; set; }
        [Required]
        public string CustomerPhoneNumber { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
    }
}