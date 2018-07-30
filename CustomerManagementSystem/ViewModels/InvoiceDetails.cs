using CustomerManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerManagementSystem.ViewModels
{
    public class InvoiceDetails
    {
        public int InvoiceNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public bool invoiceComplete { get; set; }

        public int BusinessNumber { get; set; }
        public string BusinessName { get; set; }
        public string BusinessOwner { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Logo { get; set; }
        public string ABN { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string Notes { get; set; }

        public virtual List<InvoiceItem> InvoiceItem { get; set; }

        public decimal Tax { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalCost { get; set; }

        public InvoiceDetails(int id)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var invoice = context.Invoices.Where(x => x.InvoiceNumber == id).FirstOrDefault();

                InvoiceNumber = id;
                CreationDate = invoice.CreationDate;
                invoiceComplete = invoice.invoiceComplete;
                BusinessNumber = invoice.BusinessNumber;
                BusinessName = invoice.BusinessName;
                BusinessOwner = invoice.BusinessOwner;
                PhoneNumber = invoice.PhoneNumber;
                Email = invoice.Email;
                Website = invoice.Website;
                Logo = invoice.Logo;
                ABN = invoice.ABN;
                CustomerId = invoice.CustomerId;
                CustomerName = invoice.CustomerName;
                CustomerAddress = invoice.CustomerAddress;
                CustomerPhone = invoice.CustomerPhone;
                CustomerEmail = invoice.CustomerEmail;
                Notes = invoice.Notes;
                InvoiceItem = context.InvoiceItems.Where(x => x.InvoiceId == id).ToList();
                Tax = invoice.Tax;
                SubTotal = invoice.SubTotal;
                TotalCost = invoice.TotalCost;
            }
        }
    }
}