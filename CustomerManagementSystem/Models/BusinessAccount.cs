﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using CustomerManagementSystem.ViewModels;

namespace CustomerManagementSystem.Models
{
    public class BusinessAccount
    {
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

        public static void UpdateBusiness(FormCollection collection, int businessNumber)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var business = context.BusinessAccounts.Where(x => x.BusinessNumber == businessNumber).FirstOrDefault();
                if (collection["BusinessName"] != null)
                {
                    business.BusinessName = collection["BusinessName"];
                }
                if (collection["BusinessOwner"] != null)
                {
                    business.BusinessOwner = collection["BusinessOwner"];
                }
                if (collection["PhoneNumber"] != null)
                {
                    business.PhoneNumber = collection["PhoneNumber"];
                }
                if (collection["Email"] != null)
                {
                    business.Email = collection["Email"];
                }
                if (collection["Website"] != null)
                {
                    business.Website = collection["Website"];
                }
                if (collection["Logo"] != null)
                {
                    business.Logo = collection["Logo"];
                }
                if (collection["ABN"] != null)
                {
                    business.ABN = collection["ABN"];
                }
                context.SaveChanges();
            }
        }

        public static void DeleteBusiness(int id)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var business = new BusinessAccount(id);
                context.BusinessAccounts.Attach(business);
                context.BusinessAccounts.Remove(business);
                context.SaveChanges();
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

        public int NewInvoice(int BusinessNumber, int CustomerNumber){
            
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var business = new BusinessAccount(BusinessNumber);
                var customer = new Customer(CustomerNumber);

                var newInvoice = new Invoice
                {
                    CreationDate = DateTime.Now,

                    BusinessNumber = business.BusinessNumber,
                    BusinessName = business.BusinessName,
                    BusinessOwner = business.BusinessOwner,
                    PhoneNumber = business.PhoneNumber,
                    Email = business.Email,
                    Website = business.Website,
                    Logo = business.Logo,
                    ABN = business.ABN,
                    CustomerId = customer.CustomerId,
                    Tax = (decimal)3.00,

                    CustomerName = customer.CustomerName,
                    CustomerAddress = customer.CustomerAddress,
                    CustomerPhone = customer.CustomerPhoneNumber,
                    CustomerEmail = customer.CustomerEmail,
                };
                context.Invoices.Add(newInvoice);
                context.SaveChanges();
                return newInvoice.InvoiceNumber;
            }
        }

        public InvoiceDisplay NewInvoiceDisplay(int Id)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var invoice = new InvoiceDisplay();
                var business = new BusinessAccount(Id);

                invoice.BusinessNumber = business.BusinessNumber;
                invoice.BusinessName = business.BusinessName;
                invoice.BusinessOwner = business.BusinessOwner;
                invoice.PhoneNumber = business.PhoneNumber;
                invoice.Email = business.Email;
                invoice.Website = business.Website;
                invoice.Logo = business.Logo;
                invoice.ABN = business.ABN;

                //Fill Customers up
                invoice.Customers = context.Customers.Where(x => x.BusinessNumber == Id).ToList();
                return invoice;
            }
        }
    }
}