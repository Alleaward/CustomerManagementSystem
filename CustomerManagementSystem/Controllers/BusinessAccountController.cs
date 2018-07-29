using CustomerManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using CustomerManagementSystem.ViewModels;
using System.Globalization;

namespace CustomerManagementSystem.Controllers
{
    [Authorize]
    public class BusinessAccountController : Controller
    {
        // GET: BusinessAccount
        public ActionResult Index()
        {
            BusinessAccount business = new BusinessAccount();
            var businesses = business.RetrieveBusinessList(User.Identity.GetUserId()).ToList();
            return View(businesses);
        }

        // GET: BusinessAccount/Create
        public ActionResult Create()
        {
            ViewBag.userId = User.Identity.GetUserId();
            return View();
        }

        // POST: BusinessAccount/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            new BusinessAccount(collection, User.Identity.GetUserId());
            return RedirectToAction("Index");
        }

        // GET: BusinessAccount/Edit/5
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var business = new BusinessAccount(id);
            return View(business);
        }

        // POST: BusinessAccount/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            BusinessAccount.UpdateBusiness(collection, businessNumber: id);
            return RedirectToAction("Index");
        }

        // GET: BusinessAccount/Delete/5
        public ActionResult Delete(int id)
        {
            var business = new BusinessAccount(id);
            return View(business);
        }

        // POST: BusinessAccount/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            BusinessAccount.DeleteBusiness(id);
            return RedirectToAction("Index", "BusinessAccount");
        }

        // GET: BusinessAccount/Manage/5
        public ActionResult Manage(int id)
        {
            return View(new BusinessAccount(id)); 
        }

        //GET: BusinessAccount/AddInvoice
        public ActionResult AddInvoice(int id)
        {
            var business = new BusinessAccount();
            return View(business.NewInvoiceDisplay(id));
        }

        //POST: BusinessAccount/AddInvoice
        [HttpPost]
        public ActionResult AddInvoice(int id, FormCollection collection)
        {
            var CustomerId = Int32.Parse(Request.Form["CustomerNumber"]);
            BusinessAccount business = new BusinessAccount(id);
            var invoiceId = business.NewInvoice(BusinessNumber: id, CustomerNumber: CustomerId);
            return RedirectToAction("AddInvoiceItem/" + id + "/" + invoiceId, "BusinessAccount");
        }

        //GET: BusinessAccount/AddCustomer
        public ActionResult AddCustomer(int id)
        {
            return View();
        }

        //POST: BusinessAccount/AddCustomer
        [HttpPost]
        public ActionResult AddCustomer(int id, FormCollection collection)
        {
            Customer.AddCustomer(collection, id);
            return RedirectToAction("AddInvoice/" + id, "BusinessAccount");
        }
        
        //GET: BusinessAccount/AddInvoiceItem/id--------------REWORK THIS
        public ActionResult AddInvoiceItem(int id, int option)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var vm = new InvoiceItemVM();

                vm.Items = context.Items.Where(x => x.BusinessNumber == id).ToList();

                //convert to display a list of items ordered
                vm.Ordered = context.InvoiceItems.Where(x => x.InvoiceId == option).ToList();

                foreach(var order in vm.Ordered)
                {
                    var item = context.Items.Where(x => x.ItemNumber == order.ItemId).First();
                    order.ItemName = item.ItemName;
                }

                ViewBag.BusinessNumber = id;
                ViewBag.InvoiceNumber = option;
                ViewBag.Subtotal = TempData["Subtotal"];
                ViewBag.Total = TempData["Total"];

                return View(vm);
            }
        }
        //Post: BusinessAccount/AddInvoiceItem/id--------------REWORK THIS
        [HttpPost]
        public ActionResult AddInvoiceItem(int id, int option, FormCollection collection)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                if(Request.Form["Submit"] == "Add Item to Order."){

                    var formItemNumber = Int32.Parse(Request.Form["ItemNumber"]);
                    var itemForName = context.Items.Where(x => x.ItemNumber == formItemNumber).First();

                    var newInvoiceItem = new InvoiceItem
                    {
                        InvoiceId = option,
                        ItemId = Int32.Parse(Request.Form["ItemNumber"]),
                        ItemQuantity = Int32.Parse(Request.Form["ItemQuantity"]),
                        ItemName = itemForName.ItemName,
                    };
                    context.InvoiceItems.Add(newInvoiceItem);
                    context.SaveChanges();

                    var orderItems = context.InvoiceItems.Where(x => x.InvoiceId == option).ToList();

                    var subtotal = (decimal)0.00;
                    foreach(var item in orderItems)
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
                        tax = Decimal.Parse(Request.Form["taxRate"]);
                    invoice.Tax = tax;

                    var total = subtotal + ((subtotal/100)*tax);

                    invoice.SubTotal = subtotal;
                    invoice.TotalCost = total;

                    TempData["Subtotal"] = subtotal;
                    TempData["Total"] = total;

                    context.SaveChanges();
                    return RedirectToAction("AddInvoiceItem/" + id + "/" + option, "BusinessAccount");

                }else if (Request.Form["Submit"] == "Create"){
                    var notes = Request.Form["Notes"];
                    var invoice = context.Invoices.Where(x => x.InvoiceNumber == option).FirstOrDefault();
                    invoice.Notes = notes;
                    invoice.invoiceComplete = true;
                    context.SaveChanges();
                    return RedirectToAction("Manage/" + id + "/" + option, "BusinessAccount");

                }else if(Request.Form["removeItem"] != null){
                    var invoiceID = Int32.Parse(Request.Form["removeItem"]);
                    var invoiceItem = context.InvoiceItems.Where(x => x.InvoiceItemId == invoiceID).FirstOrDefault();
                    context.InvoiceItems.Attach(invoiceItem);
                    context.InvoiceItems.Remove(invoiceItem);
                    context.SaveChanges();
                    return RedirectToAction("AddInvoiceItem/" + id + "/" + option, "BusinessAccount");
                }
                else
                {
                    return RedirectToAction("AddInvoiceItem/" + id + "/" + option, "BusinessAccount");
                }
            }
        }
        //GET: BusinessAccount/AddItem/id
        public ActionResult AddItem(int id, int option)
        {
            return View();
        }

        //POST: BusinessAccount/AddItem/id--------------REWORK THIS
        [HttpPost]
        public ActionResult AddItem(int id, int option, FormCollection collection)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var newItem = new Item{
                    BusinessNumber = id,
                    ItemName = Request.Form["ItemName"],
                    ItemDescription = Request.Form["ItemDescription"],
                    Cost = Decimal.Parse(Request.Form["Cost"]),
                };
                context.Items.Add(newItem);
                context.SaveChanges();
                return RedirectToAction("AddInvoiceItem/" + id + "/" + option, "BusinessAccount");
            }
        }

        //POST: BusinessAccount/DeleteInvoice/id--------------REWORK THIS
        public ActionResult InvoiceDelete(int id)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var model = context.Invoices.Where(x => x.InvoiceNumber == id).FirstOrDefault();
                return View(model);
            }
        }

        //POST: BusinessAccount/DeleteInvoice/id--------------REWORK THIS
        [HttpPost]
        public ActionResult InvoiceDelete(int id, FormCollection collection)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var invoice = new Invoice { InvoiceNumber = id };
                context.Invoices.Attach(invoice);
                context.Invoices.Remove(invoice);
                context.SaveChanges();

                var model = context.Invoices.Where(x => x.InvoiceNumber == id).FirstOrDefault();
                return RedirectToAction("Manage/" + model.BusinessNumber, "BusinessAccount");
            }
        }

        //POST: BusinessAccount/InvoiceDetails/id--------------REWORK THIS
        public ActionResult InvoiceDetails(int id)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var invoice = context.Invoices.Where(x => x.InvoiceNumber == id).FirstOrDefault();

                var vm = new InvoiceDetails();

                    vm.InvoiceNumber = id;
                    vm.CreationDate = invoice.CreationDate;
                    vm.invoiceComplete = invoice.invoiceComplete;
                    vm.BusinessNumber = invoice.BusinessNumber;
                    vm.BusinessName = invoice.BusinessName;
                    vm.BusinessOwner = invoice.BusinessOwner;
                    vm.PhoneNumber = invoice.PhoneNumber;
                    vm.Email = invoice.Email;
                    vm.Website = invoice.Website;
                    vm.Logo = invoice.Logo;
                    vm.ABN = invoice.ABN;
                    vm.CustomerId = invoice.CustomerId;
                    vm.CustomerName = invoice.CustomerName;
                    vm.CustomerAddress = invoice.CustomerAddress;
                    vm.CustomerPhone = invoice.CustomerPhone;
                    vm.CustomerEmail = invoice.CustomerEmail;
                    vm.Notes = invoice.Notes;
                    vm.InvoiceItem = context.InvoiceItems.Where(x => x.InvoiceId == id).ToList();
                    vm.Tax = invoice.Tax;
                    vm.SubTotal = invoice.SubTotal;
                    vm.TotalCost = invoice.TotalCost;

                return View(vm);
            }
        }
    }
}
