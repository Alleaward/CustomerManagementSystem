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
        public int BusinessNumber { get; set; }
        public int InvoiceId { get; set; }
        public int ItemId { get; set; }
        public int ItemQuantity { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }

        public InvoiceItemVM(int BusinessNumber, int InvoiceId){
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                this.Items = context.Items.Where(x => x.BusinessNumber == BusinessNumber).ToList();
                this.Ordered = context.InvoiceItems.Where(x => x.InvoiceId == InvoiceId).ToList();

                foreach (var order in this.Ordered)
                {
                    var item = context.Items.Where(x => x.ItemNumber == order.ItemId).First();
                    order.ItemName = item.ItemName;
                }

                var orderItems = context.InvoiceItems.Where(x => x.InvoiceId == InvoiceId).ToList();

                Subtotal = (decimal)0.00;
                foreach (var item in orderItems)
                {
                    var itemCost = context.Items.Where(x => x.ItemNumber == item.ItemId).First();
                    var quantity = item.ItemQuantity;
                    var cost = itemCost.Cost;
                    var totalCost = cost * quantity;
                    Subtotal += totalCost;
                }
                var invoice = context.Invoices.Where(x => x.InvoiceNumber == InvoiceId).FirstOrDefault();

                Total = Subtotal + ((Subtotal / 100) * invoice.Tax);
                this.BusinessNumber = BusinessNumber;
                this.InvoiceId = InvoiceId;
            }
        }
    }
}