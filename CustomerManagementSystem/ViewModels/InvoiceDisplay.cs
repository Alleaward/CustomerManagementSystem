using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CustomerManagementSystem.Models;

namespace CustomerManagementSystem.ViewModels
{
    public class InvoiceDisplay
    {
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

        public decimal Tax { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalCost { get; set; }
    }
}