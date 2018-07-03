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
    public class BusinessAccountController : Controller
    {
        // GET: BusinessAccount~~~
        [Authorize]
        public ActionResult Index()
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var userId = User.Identity.GetUserId();
                ViewBag.userId = userId;
                var businesses = context.BusinessAccounts.Where(x => x.UserAccount == userId).OrderBy(x => x.BusinessName).ToList();
                return View(businesses);
            }
        }

        // GET: BusinessAccount/Create~~~
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.userId = User.Identity.GetUserId();
            return View();
        }

        // POST: BusinessAccount/Create~~~
        [HttpPost]
        [Authorize]
        public ActionResult Create(FormCollection collection)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var newBusinessAccount = new BusinessAccount(
                        BusinessName: Request.Form["BusinessName"],
                        BusinessOwner: Request.Form["BusinessOwner"],
                        PhoneNumber: Request.Form["PhoneNumber"],
                        Email: Request.Form["Email"],
                        Website: Request.Form["Website"],
                        Logo: Request.Form["Logo"],
                        ABN: Request.Form["ABN"],
                        UserAccount: User.Identity.GetUserId()
                    );
                return RedirectToAction("Index");
            }
        }

        // GET: BusinessAccount/Edit/5~~~
        [Authorize]
        public ActionResult Edit(int id)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var userId = User.Identity.GetUserId();
                var business = new BusinessAccount(id);
                if (userId == business.UserAccount)
                {
                    return View(business);
                }else{
                    return RedirectToAction("Index", "BusinessAccount");
                }
            }
        }

        // POST: BusinessAccount/Edit/5~
        [HttpPost]
        [Authorize]
        public ActionResult Edit(int id, FormCollection collection)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var userId = User.Identity.GetUserId();
                var business = context.BusinessAccounts.Where(x => x.BusinessNumber == id && x.UserAccount == userId).FirstOrDefault();
                //THIS CAN BE REWORKED
                if (Request.Form["BusinessName"] != null){business.BusinessName = Request.Form["BusinessName"];}
                if (Request.Form["BusinessOwner"] != null){business.BusinessOwner = Request.Form["BusinessOwner"];}
                if (Request.Form["PhoneNumber"] != null){business.PhoneNumber = Request.Form["PhoneNumber"];}
                if (Request.Form["Email"] != null){business.Email = Request.Form["Email"];}
                if (Request.Form["Website"] != null){business.Website = Request.Form["Website"];}
                if (Request.Form["Logo"] != null){business.Logo = Request.Form["Logo"];}
                if (Request.Form["ABN"] != null){business.ABN = Request.Form["ABN"];}
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: BusinessAccount/Delete/5~~~
        [Authorize]
        public ActionResult Delete(int id)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var userId = User.Identity.GetUserId();
                var business = new BusinessAccount(id);
                if(userId == business.UserAccount){
                    return View(business);
                }else{
                    return RedirectToAction("Index", "BusinessAccount");
                }
            }
        }

        // POST: BusinessAccount/Delete/5~
        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id, FormCollection collection)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var business = new BusinessAccount(id);
                if (User.Identity.GetUserId() == business.UserAccount)
                {
                    context.BusinessAccounts.Attach(business);
                    context.BusinessAccounts.Remove(business);
                    context.SaveChanges();
                }
                return RedirectToAction("Index", "BusinessAccount");
            }
        }

        // GET: BusinessAccount/Manage/5
        [Authorize]
        public ActionResult Manage(int id)
        {
            try { return View(new BusinessAccount(id)); }
            catch { return RedirectToAction("Index", "BusinessAccount"); }
        }

        //GET: BusinessAccount/AddInvoice--------------REWORK THIS
        [Authorize]
        public ActionResult AddInvoice(int id)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var userId = User.Identity.GetUserId();
                var invoice = new InvoiceDisplay();
                var business = new BusinessAccount(id);
                //var business = context.BusinessAccounts.Where(x => x.BusinessNumber == id).FirstOrDefault();
                invoice.BusinessNumber = business.BusinessNumber;
                invoice.BusinessName = business.BusinessName;
                invoice.BusinessOwner = business.BusinessOwner;
                invoice.PhoneNumber = business.PhoneNumber;
                invoice.Email = business.Email;
                invoice.Website = business.Website;
                invoice.Logo = business.Logo;
                invoice.ABN = business.ABN;
                
                //Fill Customers up
                invoice.Customers = context.Customers.Where(x => x.BusinessNumber == id).ToList();

                return View(invoice);
            }
        }

        //POST: BusinessAccount/AddInvoice--------------REWORK THIS
        [HttpPost]
        [Authorize]
        public ActionResult AddInvoice(int id, FormCollection collection)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                //change to class declaration
                var business = new BusinessAccount(id);
                //var business = context.BusinessAccounts.Where(x => x.BusinessNumber == id).FirstOrDefault();
                var CustomerId = Int32.Parse(Request.Form["CustomerNumber"]);
                //change to class declaration
                var customer = new Customer(CustomerId);
                //var customer = context.Customers.Where(x => x.CustomerId == CustomerId).FirstOrDefault();

                //make a new constructor that takes in business number and customer number and creates and saves  new invoice to db
                var newInvoice = new Invoice
                {
                    CreationDate = DateTime.Now,

                    BusinessNumber = business.BusinessNumber,
                    BusinessName = business.BusinessName,
                    BusinessOwner = business.BusinessOwner,
                    PhoneNumber = business.PhoneNumber,
                    Email = business.Email,
                    Website = business.Website,
                    Logo = business.Logo,
                    ABN = business.ABN,
                    CustomerId = CustomerId,

                    CustomerName = customer.CustomerName,
                    CustomerAddress = customer.CustomerAddress,
                    CustomerPhone = customer.CustomerPhoneNumber,
                    CustomerEmail = customer.CustomerEmail,
                    /*
                    Notes = Request.Form["Notes"],
                    SubTotal = Decimal.Parse(Request.Form["SubTotal"]),
                    Tax = Decimal.Parse(Request.Form["Tax"]),
                    TotalCost = Decimal.Parse(Request.Form["TotalCost"]),
                     */
                };
                context.Invoices.Add(newInvoice);
                context.SaveChanges();

                var invoiceNumber = newInvoice.InvoiceNumber;

                return RedirectToAction("AddInvoiceItem/" + business.BusinessNumber + "/" + invoiceNumber, "BusinessAccount");
            }
        }

        //GET: BusinessAccount/AddCustomer
        [Authorize]
        public ActionResult AddCustomer(int id)
        {
            return View();
        }

        //POST: BusinessAccount/AddCustomer~~~
        [HttpPost]
        [Authorize]
        public ActionResult AddCustomer(int id, FormCollection collection)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var customer = new Customer(
                    BusinessNumber: id, 
                    CustomerName: Request.Form["CustomerName"], 
                    CustomerAddress: Request.Form["CustomerAddress"], 
                    CustomerPhoneNumber: Request.Form["CustomerPhoneNumber"], 
                    CustomerEmail: Request.Form["CustomerEmail"]);

                return RedirectToAction("AddInvoice/" + id, "BusinessAccount");
            }
        }
        //GET: BusinessAccount/AddInvoiceItem/id--------------REWORK THIS
        [Authorize]
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
        [Authorize]
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
                    invoice.SubTotal = subtotal;

                    TempData["Subtotal"] = subtotal;

                    //calculate and save total/tax
                    var tax = (decimal)0.00;
                        tax = Decimal.Parse(Request.Form["taxRate"]);
                    invoice.Tax = tax;

                    var total = subtotal + ((subtotal/100)*tax);
                    invoice.TotalCost = total;

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

                }else{
                    return RedirectToAction("AddInvoiceItem/" + id + "/" + option, "BusinessAccount");
                }
            }
        }
        //GET: BusinessAccount/AddItem/id
        [Authorize]
        public ActionResult AddItem(int id, int option)
        {
            return View();
        }

        //POST: BusinessAccount/AddItem/id--------------REWORK THIS
        [HttpPost]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
