using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagementSystem.Models
{
    public class BusinessAccount
    {
        public BusinessAccount()
        {
            this.Invoices = new List<Invoice>();
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
    }
}