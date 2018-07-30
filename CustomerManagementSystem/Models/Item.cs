using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

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

        public Item()
        {

        }

        public Item(int id, FormCollection collection)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                BusinessNumber = id;
                ItemName = collection["ItemName"];
                ItemDescription = collection["ItemDescription"];
                Cost = Decimal.Parse(collection["Cost"]);
                context.Items.Add(this);
                context.SaveChanges();
            }
        }
    }
}