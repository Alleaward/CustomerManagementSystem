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
    }
}