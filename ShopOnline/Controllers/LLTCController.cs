using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Framework;

namespace ShopOnline.Controllers
{
    public class LLTCController : Controller
    {
        //
        // GET: /LLTC/

        public ActionResult Index()
        {
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                LLTCViewModel model = new LLTCViewModel();

                model.Catelory  = db.Catelories.OrderBy(m => m.ID).Take(100).ToList();

                model.LLTC = db.LLTCs.OrderBy(i => i.Main_Area).Take(100).ToList();
                
                return View(model);
            }
        }

        //
        // GET: /LLTC/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /LLTC/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /LLTC/Create

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
                return View("Create");
            }
        }

        //
        // GET: /LLTC/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /LLTC/Edit/5

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
        // GET: /LLTC/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /LLTC/Delete/5

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
