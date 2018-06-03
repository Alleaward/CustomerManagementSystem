using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagementSystem.Models
{
    public class BusinessAccount
    {
        [Key]
        public int BusinessNumber { get; set; }
        public string UserAccount { get; set; }
        public string BusinessName { get; set; }
        public string BusinessOwner { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Logo { get; set; }
        public string ABN { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}