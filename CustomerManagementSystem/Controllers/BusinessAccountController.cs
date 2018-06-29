﻿using CustomerManagementSystem.Models;
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
        // GET: BusinessAccount~
        public ActionResult Index()
        {
            //check user is logged in
            if (User.Identity.IsAuthenticated)
            {
                using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
                {
                    var userId = User.Identity.GetUserId();
                    ViewBag.userId = userId;
                    //retrieve only businesses associated with this user account
                    var list = context.BusinessAccounts.Where(x => x.UserAccount == userId).OrderBy(x => x.BusinessName).ToList();
                    return View(list);
                }
            }
            else
            {
                //Upon failure redirect to home page
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: BusinessAccount/Create~
        public ActionResult Create()
        {
            ViewBag.userId = User.Identity.GetUserId();
            return View();
        }

        // POST: BusinessAccount/Create~
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            if (User.Identity.IsAuthenticated)
            {
                using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
                {
                    var userId = User.Identity.GetUserId();
                    var newBusinessAccount = new BusinessAccount
                    {
                        BusinessName = Request.Form["BusinessName"],
                        BusinessOwner = Request.Form["BusinessOwner"],
                        PhoneNumber = Request.Form["PhoneNumber"],
                        Email = Request.Form["Email"],
                        Website = Request.Form["Website"],
                        Logo = Request.Form["Logo"],
                        ABN = Request.Form["ABN"],
                        UserAccount = userId,
                    };
                    context.BusinessAccounts.Add(newBusinessAccount);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: BusinessAccount/Edit/5~
        public ActionResult Edit(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
                {
                    var userId = User.Identity.GetUserId();
                    var item = context.BusinessAccounts.Where(x => x.BusinessNumber == id && x.UserAccount == userId).ToList();
                    return View(item);
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: BusinessAccount/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (User.Identity.IsAuthenticated)
            {
                using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
                {
                    var userId = User.Identity.GetUserId();
                    var item = context.BusinessAccounts.Where(x => x.BusinessNumber == id && x.UserAccount == userId).FirstOrDefault();

                    if (Request.Form["item.BusinessName"] != null)
                    {
                        item.BusinessName = Request.Form["item.BusinessName"];
                    }
                    if (Request.Form["item.BusinessOwner"] != null)
                    {
                        item.BusinessOwner = Request.Form["item.BusinessOwner"];
                    }
                    if (Request.Form["item.PhoneNumber"] != null)
                    {
                        item.PhoneNumber = Request.Form["item.PhoneNumber"];
                    }
                    if (Request.Form["item.Email"] != null)
                    {
                        item.Email = Request.Form["item.Email"];
                    }
                    if (Request.Form["item.Website"] != null)
                    {
                        item.Website = Request.Form["item.Website"];
                    }
                    if (Request.Form["item.Logo"] != null)
                    {
                        item.Logo = Request.Form["item.Logo"];
                    }
                    if (Request.Form["item.ABN"] != null)
                    {
                        item.ABN = Request.Form["item.ABN"];
                    }

                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: BusinessAccount/Delete/5~
        public ActionResult Delete(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
                {
                    var userId = User.Identity.GetUserId();
                    var item = context.BusinessAccounts.Where(x => x.BusinessNumber == id && x.UserAccount == userId).ToList();
                    return View(item);
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: BusinessAccount/Delete/5~
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
                    {
                        var business = new BusinessAccount { BusinessNumber = id };
                        context.BusinessAccounts.Attach(business);
                        context.BusinessAccounts.Remove(business);
                        context.SaveChanges();
                        return RedirectToAction("Index", "BusinessAccount");
                    }
                }
                catch
                {
                    return RedirectToAction("Index", "BusinessAccount");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: BusinessAccount/Manage/5
        public ActionResult Manage(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    CustomerManagementSystemContext context = new CustomerManagementSystemContext();
                    
                        ViewBag.BusinessNumber = id;

                        var business = context.BusinessAccounts.Where(x => x.BusinessNumber == id).First();
                        return View(business);

                    
                }
                catch
                {
                    return RedirectToAction("Index", "BusinessAccount");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: BusinessAccount/Manage/5
        [HttpPost]
        public ActionResult Manage(int id, FormCollection collection)
        {
            return RedirectToAction("Login", "Account");
        }

        //GET: BusinessAccount/AddInvoice
        public ActionResult AddInvoice(int id)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var userId = User.Identity.GetUserId();
                var invoice = new InvoiceDisplay();

                //Get the right business

                var business = context.BusinessAccounts.Where(x => x.BusinessNumber == id).FirstOrDefault();
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

                //invoice.Items = context.Items.Where(x => x.BusinessNumber == id).ToList();

                var item = context.BusinessAccounts.Where(x => x.BusinessNumber == id).FirstOrDefault();

                return View(invoice);
            }
        }

        //POST: BusinessAccount/AddInvoice
        [HttpPost]
        public ActionResult AddInvoice(int id, FormCollection collection)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var business = context.BusinessAccounts.Where(x => x.BusinessNumber == id).FirstOrDefault();
                var CustomerId = Int32.Parse(Request.Form["CustomerNumber"]);
                var customer = context.Customers.Where(x => x.CustomerId == CustomerId).FirstOrDefault();

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
        public ActionResult AddCustomer(int id)
        {
            return View();
        }

        //POST: BusinessAccount/AddCustomer
        [HttpPost]
        public ActionResult AddCustomer(int id, FormCollection collection)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var newCustomer = new Customer
                {
                    BusinessNumber = id,
                    CustomerName = Request.Form["CustomerName"],
                    CustomerAddress = Request.Form["CustomerAddress"],
                    CustomerPhoneNumber = Request.Form["CustomerPhoneNumber"],
                    CustomerEmail = Request.Form["CustomerEmail"],
                };
                context.Customers.Add(newCustomer);
                context.SaveChanges();
                return RedirectToAction("AddInvoice/" + id, "BusinessAccount");
            }
        }
        //GET: BusinessAccount/AddInvoiceItem/id
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
        //Post: BusinessAccount/AddInvoiceItem/id
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
        public ActionResult AddItem(int id, int option)
        {
            return View();
        }

        //POST: BusinessAccount/AddItem/id
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
        //POST: BusinessAccount/DeleteInvoice/id
        public ActionResult InvoiceDelete(int id)
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var model = context.Invoices.Where(x => x.InvoiceNumber == id).FirstOrDefault();
                return View(model);
            }
        }
        //POST: BusinessAccount/DeleteInvoice/id
        [HttpPost]
        public ActionResult InvoiceDelete(int id, FormCollection collection)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
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
                catch
                {
                    return RedirectToAction("Index", "BusinessAccount");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        
        //POST: BusinessAccount/InvoiceDetails/id
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
