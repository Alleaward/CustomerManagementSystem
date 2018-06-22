using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagementSystem.Models
{
    public class Item
    {
        public int BusinessNumber { get; set; }

        [Key]
        public int ItemNumber { get; set; }
        [Required]
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        [Required]
        public decimal Cost { get; set; }
    }
}