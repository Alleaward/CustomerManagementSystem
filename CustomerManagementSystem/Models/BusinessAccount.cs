using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Web.Mvc;

namespace CustomerManagementSystem.Models
{
    public class BusinessAccount
    {
        public BusinessAccount()
        {

        }

        public BusinessAccount(FormCollection collection, string userID)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                this.BusinessName = collection["BusinessName"];
                this.BusinessOwner = collection["BusinessOwner"];
                this.PhoneNumber = collection["PhoneNumber"];
                this.Email = collection["Email"];
                this.Website = collection["Website"];
                this.Logo = collection["Logo"];
                this.ABN = collection["ABN"];
                this.UserAccount = userID;
                context.BusinessAccounts.Add(this);
                context.SaveChanges();
            }
        }

        public BusinessAccount(string UserAccount, string BusinessName, string BusinessOwner,
                string PhoneNumber, string Email, string Website, string Logo, string ABN)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                this.UserAccount = UserAccount;
                this.BusinessName = BusinessName;
                this.BusinessOwner = BusinessOwner;
                this.PhoneNumber = PhoneNumber;
                this.Email = Email;
                this.Website = Website;
                this.Logo = Logo;
                this.ABN = ABN;

                context.BusinessAccounts.Add(this);
                context.SaveChanges();
            }
        }

        public BusinessAccount([Optional]int id)
        {
            this.BusinessNumber = id;
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var business = context.BusinessAccounts.Where(x => x.BusinessNumber == id).First();
                this.UserAccount = business.UserAccount;
                this.BusinessName = business.BusinessName;
                this.BusinessOwner = business.BusinessOwner;
                this.PhoneNumber = business.PhoneNumber;
                this.Email = business.Email;
                this.Website = business.Website;
                this.Logo = business.Logo;
                this.ABN = business.ABN;
                this.Invoices = business.Invoices.Where(x => x.invoiceComplete == true).ToList();
                this.Customers = business.Customers.Where(x => x.BusinessNumber == id).ToList();
            }
        }

        public List<BusinessAccount> RetrieveBusinessList(string userId)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var businesses = context.BusinessAccounts.Where(x => x.UserAccount == userId).OrderBy(x => x.BusinessName).ToList();
                return businesses;
            }
        }

        [Key]
        public int BusinessNumber { get; set; }
        [Required]
        public string UserAccount { get; set; }
        [Required]
        public string BusinessName { get; set; }
        [Required]
        public string BusinessOwner { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public string Website { get; set; }
        public string Logo { get; set; }
        [Required]
        public string ABN { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}