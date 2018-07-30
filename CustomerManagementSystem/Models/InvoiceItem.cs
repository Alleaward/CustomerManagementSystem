using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CustomerManagementSystem.Models
{
    public class InvoiceItem
    {
        [Key]
        public int InvoiceItemId { get; set; }
        public int InvoiceId { get; set; }
        public string ItemName { get; set; }
        public int ItemId { get; set; }
        public int ItemQuantity { get; set; }
        
        public InvoiceItem()
        {

        }

        public static void RemoveInvoiceItem(int id, FormCollection collection)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var invoiceID = Int32.Parse(collection["removeItem"]);
                var invoiceItem = context.InvoiceItems.Where(x => x.InvoiceItemId == invoiceID).FirstOrDefault();
                context.InvoiceItems.Attach(invoiceItem);
                context.InvoiceItems.Remove(invoiceItem);
                context.SaveChanges();
            }
        }

        public static void AddInvoiceItem(int id, int option, FormCollection collection)//Clean up
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var formItemNumber = Int32.Parse(collection["ItemNumber"]);
                var itemForName = context.Items.Where(x => x.ItemNumber == formItemNumber).First();

                var newInvoiceItem = new InvoiceItem
                {
                    InvoiceId = option,
                    ItemId = Int32.Parse(collection["ItemNumber"]),
                    ItemQuantity = Int32.Parse(collection["ItemQuantity"]),
                    ItemName = itemForName.ItemName,
                };
                context.InvoiceItems.Add(newInvoiceItem);
                context.SaveChanges();

                var orderItems = context.InvoiceItems.Where(x => x.InvoiceId == option).ToList();

                var subtotal = (decimal)0.00;
                foreach (var item in orderItems)
                {
                    //returning nothing?
                    var itemCost = context.Items.Where(x => x.ItemNumber == item.ItemId).First();
                    System.Diagnostics.Debug.WriteLine("Item Is:");
                    System.Diagnostics.Debug.WriteLine(itemCost);
                    var quantity = item.ItemQuantity;
                    var cost = itemCost.Cost;
                    var totalCost = cost * quantity;
                    subtotal += totalCost;
                }
                var invoice = context.Invoices.Where(x => x.InvoiceNumber == option).FirstOrDefault();

                // calculate and save total/tax
                var tax = (decimal)0.00;
                tax = Decimal.Parse(collection["taxRate"]);
                invoice.Tax = tax;

                var total = subtotal + ((subtotal / 100) * tax);

                invoice.SubTotal = subtotal;
                invoice.TotalCost = total;
                context.SaveChanges();
            }
        }
    }
}