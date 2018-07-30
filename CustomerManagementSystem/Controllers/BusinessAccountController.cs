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

        //GET: BusinessAccount/AddInvoiceItem/id/{option}
        public ActionResult AddInvoiceItem(int id, int option)
        {
            InvoiceItemVM vm = new InvoiceItemVM(BusinessNumber: id, InvoiceId: option);
            return View(vm);
        }

        //Post: BusinessAccount/AddInvoiceItem/id/{option}
        [HttpPost]
        public ActionResult AddInvoiceItem(int id, int option, FormCollection collection)
        {
            if (Request.Form["Submit"] == "Add Item to Order.")
            {
                InvoiceItem.AddInvoiceItem(id, option, collection);
                return RedirectToAction("AddInvoiceItem/" + id + "/" + option, "BusinessAccount");
            }
            else if (Request.Form["Submit"] == "Create")
            {
                Invoice.InvoiceCreate(id, option, collection);
                return RedirectToAction("Manage/" + id + "/" + option, "BusinessAccount");
            }
            else if (Request.Form["removeItem"] != null)
            {
                InvoiceItem.RemoveInvoiceItem(id, collection);
                return RedirectToAction("AddInvoiceItem/" + id + "/" + option, "BusinessAccount");
            }
            else
            {
                return RedirectToAction("AddInvoiceItem/" + id + "/" + option, "BusinessAccount");
            }
        }

        //GET: BusinessAccount/AddItem/id/{option}
        public ActionResult AddItem(int id, int option)
        {
            return View();
        }

        //POST: BusinessAccount/AddItem/id/{option}
        [HttpPost]
        public ActionResult AddItem(int id, int option, FormCollection collection)
        {
            new Item(id, collection);
            return RedirectToAction("AddInvoiceItem/" + id + "/" + option, "BusinessAccount");
        }

        //NEED A REMOVE ITEM OPTION<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        //POST: BusinessAccount/DeleteInvoice/id
        public ActionResult InvoiceDelete(int id)
        {
            return View(new Invoice(id));
        }

        //POST: BusinessAccount/DeleteInvoice/id
        [HttpPost]
        public ActionResult InvoiceDelete(int id, FormCollection collection)
        {
            Invoice.InvoiceDelete(id);
            return RedirectToAction("Manage/" + Request.Form["BusinessNumber"]);
        }

        //POST: BusinessAccount/InvoiceDetails/id
        public ActionResult InvoiceDetails(int id)
        {
            return View(new InvoiceDetails(id));
        }
    }
}
