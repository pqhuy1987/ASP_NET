using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Framework;

namespace ShopOnline.Controllers
{
    public class CS_tbWorkTypeController : Controller
    {
        //
        // GET: /CS_tbWorkType/

        public ActionResult Index()
        {
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                CS_tbWorkTypeViewModel model = new CS_tbWorkTypeViewModel();
                model.CS_tbWorkType = db.CS_tbWorkType.OrderBy(m => m.ID).Take(100).ToList();
                return View(model);
            }
        }

        //
        // GET: /CS_tbWorkType/Details/5

        public ActionResult Details(CS_tbWorkTypeViewModel collection)
        {
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                CS_tbWorkTypeViewModel model = new CS_tbWorkTypeViewModel();
                model.CS_tbWorkType = db.CS_tbWorkType.Where(m => m.CoreWorkType == collection.CS_tbWorkTypeSelect.CoreWorkType).OrderBy(i => i.ID).ToList();
                return View("Index",model);
            }
        }

        //
        // GET: /CS_tbWorkType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CS_tbWorkType/Create

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

        //
        // GET: /CS_tbWorkType/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /CS_tbWorkType/Edit/5

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

        //
        // GET: /CS_tbWorkType/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /CS_tbWorkType/Delete/5

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
