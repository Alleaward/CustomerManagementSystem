using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using CustomerManagementSystem.Models;

namespace CustomerManagementSystem.ViewModels
{
    public class InvoiceDisplay
    {
        public InvoiceDisplay()
        {

        }
        public InvoiceDisplay([Optional]int id, [Optional]int BusinessNumber)
        {
            this.InvoiceNumber = id;
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var invoice = context.Invoices.Where(x => x.InvoiceNumber == id).First();
                this.InvoiceNumber = invoice.InvoiceNumber;
                this.CreationDate = invoice.CreationDate;
                this.BusinessNumber = invoice.BusinessNumber;
                this.BusinessName = invoice.BusinessName;
                this.BusinessOwner = invoice.BusinessOwner;
                this.PhoneNumber = invoice.PhoneNumber;
                this.Email = invoice.Email;
                this.Website = invoice.Website;
                this.Logo = invoice.Logo;
                this.ABN = invoice.ABN;
                this.Customers = context.Customers.Where(x => x.BusinessNumber == BusinessNumber).ToList();
                this.Notes = invoice.Notes;
                this.InvoiceItem = invoice.InvoiceItem.Where(x => x.InvoiceId == InvoiceNumber).ToList();
                this.Items = context.Items.Where(x => x.BusinessNumber == BusinessNumber).ToList();
                this.Tax = invoice.Tax;
                this.SubTotal = invoice.SubTotal;
                this.TotalCost = invoice.TotalCost;
            }
        }
        public int InvoiceNumber { get; set; }
        public DateTime CreationDate { get; set; }

        public int BusinessNumber { get; set; }
        public string BusinessName { get; set; }
        public string BusinessOwner { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Logo { get; set; }
        public string ABN { get; set; }

        public virtual List<Customer> Customers { get; set; }

        public string Notes { get; set; }

        public virtual List<InvoiceItem> InvoiceItem { get; set; }

        public virtual List<Item> Items { get; set; }

        public decimal Tax { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalCost { get; set; }
    }
}