using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagementSystem.Models
{
    public class Item
    {
        [Key]
        public int ItemNumber { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public decimal Cost { get; set; }
    }
}