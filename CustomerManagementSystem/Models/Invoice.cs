using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace CustomerManagementSystem.Models
{
    public class Invoice
    {
        public Invoice()
        {

        }
        public Invoice([Optional]int id)
        {
            this.InvoiceNumber = id;
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var invoice = context.Invoices.Where(x => x.InvoiceNumber == id).First();
                this.InvoiceNumber = invoice.InvoiceNumber;
                this.CreationDate = invoice.CreationDate;
                this.invoiceComplete = invoice.invoiceComplete;
                this.BusinessNumber = invoice.BusinessNumber;
                this.BusinessName = invoice.BusinessName;
                this.BusinessOwner = invoice.BusinessOwner;
                this.PhoneNumber = invoice.PhoneNumber;
                this.Email = invoice.Email;
                this.Website = invoice.Website;
                this.Logo = invoice.Logo;
                this.ABN = invoice.ABN;
                this.CustomerId = invoice.CustomerId;
                this.CustomerName = invoice.CustomerName;
                this.CustomerAddress = invoice.CustomerAddress;
                this.CustomerPhone = invoice.CustomerPhone;
                this.CustomerEmail = invoice.CustomerEmail;
                this.Notes = invoice.Notes;
                this.InvoiceItem = invoice.InvoiceItem.Where(x => x.InvoiceId == InvoiceNumber).ToList();
                this.Tax = invoice.Tax;
                this.SubTotal = invoice.SubTotal;
                this.TotalCost = invoice.TotalCost;
            }
        }
        [Key]
        public int InvoiceNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public bool invoiceComplete { get; set; }

        //foriegn key
        public int BusinessNumber { get; set; }
        public string BusinessName { get; set; }
        public string BusinessOwner { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Logo { get; set; }
        public string ABN { get; set; }

        //foreign key
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<InvoiceItem> InvoiceItem { get; set; }

        public decimal Tax { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalCost { get; set; }
    }
}