using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagementSystem.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceNumber { get; set; }
        public string BusinessName { get; set; }
        public string BusinessOwner { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Logo { get; set; }
        public string ABN { get; set; }

        public DateTime CreationDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<Item> Items { get; set; }
        public int SubTotal { get; set; }
        public int TotalCost { get; set; }
    }
}