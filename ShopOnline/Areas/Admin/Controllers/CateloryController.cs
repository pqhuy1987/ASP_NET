using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Framework;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class CateloryController : Controller
    {
        //
        // GET: /Admin/Catelory/

        public ActionResult Index()
        {
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                CateloryViewModel model = new CateloryViewModel();
                model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                model.Catelory = db.Catelories.OrderBy(m => m.ID).Take(100).ToList();

                model.ProjectAll    = new List<SelectListItem>(); 
                var items           = new List<SelectListItem>();

                foreach (var project in model.Project)
                {
                    items.Add(new SelectListItem()
                    {
                        Value = project.Project_Name,
                        Text = project.Project_Name,
                    });
                }

                model.ProjectAll = items;


                model.SelectedCatelory = null;
                model.DisplayMode = null;

                return View(model);
            }
        }

        //
        // GET: /Admin/Catelory/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Catelory/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Catelory/Create

        [HttpPost]
        public ActionResult Create(CateloryViewModel collection)
        {
            try
            {
                using (OnlineShopDbContext db = new OnlineShopDbContext())
                {

                    Catelory obj            = new Catelory();

                    obj.Prj_Name            = collection.SelectedCatelory.Prj_Name;
                    obj.Unit_Name           = collection.SelectedCatelory.Unit_Name;
                    obj.Owner_Name          = collection.SelectedCatelory.Owner_Name;
                    obj.Phone_Number        = collection.SelectedCatelory.Phone_Number;
                    obj.Person_Number       = collection.SelectedCatelory.Person_Number;
                    obj.Create_Date         = DateTime.Now;
                    obj.Status              = collection.SelectedCatelory.Status;

                    db.Catelories.Add(obj);
                    db.SaveChanges();

                    CateloryViewModel model = new CateloryViewModel();
                    model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                    model.Catelory = db.Catelories.OrderBy(m => m.ID).Take(100).ToList();

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

                    model.SelectedCatelory = null;
                    model.DisplayMode = null;

                    return View("Index",model);
                }
            }
            catch
            {
                using (OnlineShopDbContext db = new OnlineShopDbContext())
                {
                    CateloryViewModel model = new CateloryViewModel();
                    model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                    model.Catelory = db.Catelories.OrderBy(m => m.ID).Take(100).ToList();
                    model.SelectedCatelory = null;
                    model.DisplayMode = null;
                    return View("Index", model);
                }
            }
        }

        //
        // GET: /Admin/Catelory/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Catelory/Edit/5

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
        // GET: /Admin/Catelory/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Catelory/Delete/5

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
