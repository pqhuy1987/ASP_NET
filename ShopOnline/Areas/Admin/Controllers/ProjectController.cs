﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Framework;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class ProjectController : Controller
    {
        //
        // GET: /Admin/Project

        public ActionResult Index()
        {
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                ProjectViewModel model      = new ProjectViewModel();
                model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();

                model.SelectedProject = null;
                model.Catelory_Project = null;
                //model.SelectedProject.Number_Project = 100;
                return View(model);
            }
        }

        //
        // GET: /Admin/Project/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Project/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Project/Create

        [HttpPost]
        public ActionResult Create(ProjectViewModel collection)
        {
            try
            {
                    using (OnlineShopDbContext db = new OnlineShopDbContext())
                    {
                        Project obj = new Project();
                        obj.Project_Name = collection.SelectedProject.Project_Name;
                        db.Projects.Add(obj);
                        db.SaveChanges();

                        ProjectViewModel model1 = new ProjectViewModel();
                        model1.Project = db.Projects.OrderByDescending(m => m.ID).Take(100).ToList();
                        model1.SelectedProject = null;
                        return RedirectToAction("Index", model1);
                    }
            }
            catch
            {
                using (OnlineShopDbContext db = new OnlineShopDbContext())
                {
                    ProjectViewModel model1 = new ProjectViewModel();
                    model1.Project = db.Projects.OrderBy(
                            m => m.ID).Take(100).ToList();
                    model1.SelectedProject = null;
                    return View("Index", model1);
                }
            }
        }

        //
        // GET: /Admin/Project/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Project/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, string number, FormCollection collection)
        {
            try
            {
                using (OnlineShopDbContext db = new OnlineShopDbContext())
                {
                    ProjectViewModel model1 = new ProjectViewModel();

                    if (id == 123456789)
                    {
                        model1.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();

                        model1.Catelory = db.Catelories.Where(i => i.Prj_Name == null).ToList();
                        model1.DisplayMode = "Edit";
                        model1.SelectedProject.ID = 123456789;
                        return View("Index", model1);
                    }
                    else
                    {
                        
                        model1.SelectedProject = db.Projects.Find(id);
                        model1.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();

                        model1.Catelory = db.Catelories.Where(i => i.Prj_Name == model1.SelectedProject.Project_Name).ToList();
                        if (number == "123")
                        {
                            model1.Catelory_Project = null;
                        }
                        else
                        {
                            model1.Catelory_Project = db.Catelories.Where(i => i.Phone_Number == number).ToList();
                        }
                        
                        model1.SelectedProject.Number_Project =  db.Catelories.Where(i => i.Prj_Name == model1.SelectedProject.Project_Name).Count();
                        model1.SelectedProject.Number_Person = 0;
                        model1.DisplayMode = "Edit";

                        foreach (var item1 in model1.Catelory)
                        {
                            model1.SelectedProject.Number_Person = model1.SelectedProject.Number_Person + (int)item1.Person_Number;
                        }

                        return View("Index", model1);
                    }

                }
            }
            catch
            {
                using (OnlineShopDbContext db = new OnlineShopDbContext())
                {
                    ProjectViewModel model1 = new ProjectViewModel();
                    model1.Project = db.Projects.OrderBy(
                            m => m.ID).Take(100).ToList();
                    model1.SelectedProject = null;
                    return View("Index", model1);
                }
            }
        }

        //
        // POST: /Admin/Project/Save/5

        [HttpPost]
        public ActionResult Save(int id, ProjectViewModel collection)
        {
            try
            {
                using (OnlineShopDbContext db = new OnlineShopDbContext())
                {
                    Project exsiting = db.Projects.Find(id);
                    List<Catelory> exsiting_2;
                    exsiting_2 = db.Catelories.Where(i => i.Prj_Name == exsiting.Project_Name).ToList();
                    foreach (var item1 in exsiting_2)
                    {
                        item1.Prj_Name = collection.SelectedProject.Project_Name;
                    }
                    exsiting.Project_Name = collection.SelectedProject.Project_Name;
                    db.SaveChanges();

                    ProjectViewModel model1 = new ProjectViewModel();
                    model1.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                    model1.DisplayMode = "Add";
                    model1.SelectedProject = null;
                    return RedirectToAction("Index", model1);
                }
            }
            catch
            {
                using (OnlineShopDbContext db = new OnlineShopDbContext())
                {
                    ProjectViewModel model1 = new ProjectViewModel();
                    model1.Project = db.Projects.OrderBy(
                            m => m.ID).Take(100).ToList();
                    model1.SelectedProject = null;
                    return View("Index", model1);
                }
            }
        }

        //
        // GET: /Admin/Project/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Project/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (OnlineShopDbContext db = new OnlineShopDbContext())
                {
                    Project existing = db.Projects.Find(id);
                    db.Projects.Remove(existing);
                    db.SaveChanges();

                    ProjectViewModel model1 = new ProjectViewModel();
                    model1.Project = db.Projects.OrderBy(
                            m => m.ID).Take(100).ToList();
                    model1.SelectedProject = null;
                    return View("Index", model1);
                }
            }
            catch
            {
                using (OnlineShopDbContext db = new OnlineShopDbContext())
                {
                    ProjectViewModel model1 = new ProjectViewModel();
                    model1.Project = db.Projects.OrderBy(
                            m => m.ID).Take(100).ToList();
                    model1.SelectedProject = null;
                    return View("Index", model1);
                }
            }
        }
    }
}
