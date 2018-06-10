using CustomerManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

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

        // POST: BusinessAccount/Create
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

        // GET: BusinessAccount/Edit/5
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
                    var item = context.BusinessAccounts.Where(x => x.BusinessNumber == id && x.UserAccount == userId).ToList();
                    TryUpdateModel(item);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: BusinessAccount/Delete/5
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

        // POST: BusinessAccount/Delete/5
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
                    using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
                    {
                        var userId = User.Identity.GetUserId();
                        var item = context.BusinessAccounts.Where(x => x.BusinessNumber == id && x.UserAccount == userId).ToList();
                        return View(item);
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

        // POST: BusinessAccount/Manage/5
        [HttpPost]
        public ActionResult Manage(int id, FormCollection collection)
        {
            return RedirectToAction("Login", "Account");
        }
    }
}
