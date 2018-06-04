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
            if (User.Identity.IsAuthenticated)
            {
                using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
                {
                    var userId = User.Identity.GetUserId();
                    ViewBag.userId = userId;
                    var list = context.BusinessAccounts.Where(x => x.UserAccount == userId).OrderBy(x => x.BusinessName).ToList();
                    return View(list);
                }
            }
            else
            {
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
            return View();
        }

        // POST: BusinessAccount/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
    }
}
