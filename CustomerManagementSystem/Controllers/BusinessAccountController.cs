using CustomerManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerManagementSystem.Controllers
{
    public class BusinessAccountController : Controller
    {
        // GET: BusinessAccount
        public ActionResult Index()
        {
            using (CustomerManagementSystemContext context = new CustomerManagementSystemContext())
            {
                var list = context.BusinessAccounts.OrderBy(x => x.BusinessNumber).ToList();
                return View(list);
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
