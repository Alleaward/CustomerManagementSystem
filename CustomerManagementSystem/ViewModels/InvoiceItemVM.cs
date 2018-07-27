using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using CustomerManagementSystem.Models;

namespace CustomerManagementSystem.ViewModels
{
    public class InvoiceItemVM
    {
        public virtual List<Item> Items { get; set; }
        public virtual List<InvoiceItem> Ordered { get; set; }

        [Key]
        public int InvoiceItemId { get; set; }
        public int InvoiceId { get; set; }
        public int ItemId { get; set; }
        public int ItemQuantity { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
    }
}