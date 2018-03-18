using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Framework;


namespace ShopOnline.Areas.Admin.Controllers
{
    public class WorkCountController : Controller
    {
        //
        // GET: /Admin/WorkCount/

        public ActionResult Index()
        {
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                WorkCountViewModel model = new WorkCountViewModel();
                
                model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                model.ProjectAll = new List<SelectListItem>();

                var items = new List<SelectListItem>();
                foreach (var project in model.Project)
                {
                    items.Add(new SelectListItem()
                    {
                        Value = project.Project_Name,
                        Text = project.Project_Name,
                    });
                }

                model.ProjectAll = items;

                var Check = model.Project[0].Project_Name;

                model.Catelory_Project = db.Catelories.Where(i => i.Prj_Name == Check).ToList();

                return View(model);
            }
        }

        //
        // GET: /Admin/WorkCount/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/WorkCount/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/WorkCount/Create

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
        // GET: /Admin/WorkCount/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/WorkCount/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, WorkCountViewModel collection)
        {
            try
            {
                var dates = new List<DateTime>();

                for (var dt = collection.StartDate; dt <= collection.EndDate; dt = dt.AddDays(1))
                {
                    dates.Add(dt);
                }

                collection.SelectDate = dates;

                return View("Index", collection);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/WorkCount/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/WorkCount/Delete/5

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
