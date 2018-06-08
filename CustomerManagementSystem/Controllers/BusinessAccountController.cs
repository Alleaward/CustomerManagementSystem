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
        // GET: BusinessAccount
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

        // GET: BusinessAccount/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
            //add database update logic here
            return View();
        }

        // GET: BusinessAccount/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BusinessAccount/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // GET: BusinessAccount/Manage/5
        public ActionResult Manage(int id)
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
        // POST: BusinessAccount/Manage/5
        [HttpPost]
        public ActionResult Manage(int id, FormCollection collection)
        {
            return RedirectToAction("Login", "Account");
        }
    }
}
