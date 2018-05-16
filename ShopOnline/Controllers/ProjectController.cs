using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Framework;

namespace ShopOnline.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        //
        // GET: /Admin/Project

        public ActionResult Index()
        {
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                //--------Add Dropdown for Type-------------------//
                ProjectViewModel model      = new ProjectViewModel();
                model.Project                       = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                model.CS_tbConstructionSiteType     = db.CS_tbConstructionSiteType.OrderBy(m => m.ID).Take(100).ToList();
                model.Project_Type_All = new List<SelectListItem>();
                var items = new List<SelectListItem>();

                foreach (var CS_tbConstructionSiteType in model.CS_tbConstructionSiteType)
                {
                    items.Add(new SelectListItem()
                    {
                        Value   = CS_tbConstructionSiteType.Type,
                        Text    = CS_tbConstructionSiteType.Type,
                    });
                }

                model.Project_Type_All = items;
                return View(model);
                //--------Add Dropdown for Type-------------------//               
            }
        }

        //
        // GET: /Admin/Project/Details/5

        public ActionResult Details(int id)
        {
            return View("");
        }

        //
        // GET: /Admin/Project/Create

        public ActionResult Create()
        {
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                //--------Add Dropdown for Type-------------------//
                ProjectViewModel model = new ProjectViewModel();
                model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                model.CS_tbConstructionSiteType = db.CS_tbConstructionSiteType.OrderBy(m => m.ID).Take(100).ToList();
                model.Project_Type_All = new List<SelectListItem>();
                var items = new List<SelectListItem>();

                foreach (var CS_tbConstructionSiteType in model.CS_tbConstructionSiteType)
                {
                    items.Add(new SelectListItem()
                    {
                        Value = CS_tbConstructionSiteType.Type,
                        Text = CS_tbConstructionSiteType.Type,
                    });
                }

                model.Project_Type_All = items;
                return View(model);
                //--------Add Dropdown for Type-------------------//
            }
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
                        Project obj             = new Project();
                        obj.Project_Name        = collection.SelectedProject.Project_Name;
                        obj.Site_Type           = collection.SelectedProject.Site_Type;
                        obj.General_Director    = collection.SelectedProject.General_Director;
                        obj.Site_Manager        = collection.SelectedProject.Site_Manager;
                        obj.Site_Address        = collection.SelectedProject.Site_Address;
                        obj.Value_Cost          = collection.SelectedProject.Value_Cost;
                        obj.Start_Date          = collection.SelectedProject.Start_Date;
                        obj.End_Date            = collection.SelectedProject.End_Date;
                        obj.Operation_Status    = collection.SelectedProject.Operation_Status;
                        obj.Site_Area           = collection.SelectedProject.Site_Area;

                        db.Projects.Add(obj);
                        db.SaveChanges();

                        //--------Add Dropdown for Type-------------------//
                        ProjectViewModel model = new ProjectViewModel();
                        model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                        model.CS_tbConstructionSiteType = db.CS_tbConstructionSiteType.OrderBy(m => m.ID).Take(100).ToList();
                        model.Project_Type_All = new List<SelectListItem>();
                        var items = new List<SelectListItem>();

                        foreach (var CS_tbConstructionSiteType in model.CS_tbConstructionSiteType)
                        {
                            items.Add(new SelectListItem()
                            {
                                Value = CS_tbConstructionSiteType.Type,
                                Text = CS_tbConstructionSiteType.Type,
                            });
                        }
                        model.Project_Type_All = items;
                        return RedirectToAction("Create", model);
                        //--------Add Dropdown for Type-------------------//
                    }
            }
            catch
            {
                using (OnlineShopDbContext db = new OnlineShopDbContext())
                {
                    //--------Add Dropdown for Type-------------------//
                    ProjectViewModel model = new ProjectViewModel();
                    model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                    model.CS_tbConstructionSiteType = db.CS_tbConstructionSiteType.OrderBy(m => m.ID).Take(100).ToList();
                    model.Project_Type_All = new List<SelectListItem>();
                    var items = new List<SelectListItem>();

                    foreach (var CS_tbConstructionSiteType in model.CS_tbConstructionSiteType)
                    {
                        items.Add(new SelectListItem()
                        {
                            Value = CS_tbConstructionSiteType.Type,
                            Text = CS_tbConstructionSiteType.Type,
                        });
                    }

                    model.Project_Type_All = items;
                    return View(model);
                    //--------Add Dropdown for Type-------------------//
                }
            }
        }

        //
        // GET: /Admin/Project/Edit/5

        public ActionResult Edit(int id)
        {
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                ProjectViewModel model = new ProjectViewModel();
                    //--------Select ID trả kết quả về View-----------//
                    model.SelectedProject = db.Projects.Find(id);
                    //--------Add Dropdown for Type-------------------//
                //--------Model để phía trên----------------------//
                model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                model.CS_tbConstructionSiteType = db.CS_tbConstructionSiteType.OrderBy(m => m.ID).Take(100).ToList();
                model.Project_Type_All = new List<SelectListItem>();
                var items = new List<SelectListItem>();

                foreach (var CS_tbConstructionSiteType in model.CS_tbConstructionSiteType)
                {
                    items.Add(new SelectListItem()
                    {
                        Value = CS_tbConstructionSiteType.Type,
                        Text = CS_tbConstructionSiteType.Type,
                    });
                }
                model.Project_Type_All = items;
                return View("Edit", model);
                //--------Add Dropdown for Type-------------------//
            }
        }

        [HttpPost]
        public ActionResult Save(int id, ProjectViewModel collection)
        {
            try
            {
                using (OnlineShopDbContext db = new OnlineShopDbContext())
                { 
                    Project Exsiting_Project = db.Projects.Find(id);
                    Exsiting_Project.Project_Name = collection.SelectedProject.Project_Name;
                    Exsiting_Project.Site_Type = collection.SelectedProject.Site_Type;
                    Exsiting_Project.General_Director = collection.SelectedProject.General_Director;
                    Exsiting_Project.Site_Manager = collection.SelectedProject.General_Director;
                    Exsiting_Project.Site_Address = collection.SelectedProject.Site_Address;
                    Exsiting_Project.Value_Cost = collection.SelectedProject.Value_Cost;
                    Exsiting_Project.Start_Date = collection.SelectedProject.Start_Date;
                    Exsiting_Project.End_Date = collection.SelectedProject.End_Date;
                    Exsiting_Project.Operation_Status = collection.SelectedProject.Operation_Status;
                    Exsiting_Project.Site_Area = collection.SelectedProject.Site_Area;
                    db.SaveChanges();

                    //--------Add Dropdown for Type-------------------//
                    ProjectViewModel model = new ProjectViewModel();
                        //--------Select ID trả kết quả về View-----------//
                        model.SelectedProject = db.Projects.Find(id);
                        //--------Select ID trả kết quả về View-----------//
                    model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                    model.CS_tbConstructionSiteType = db.CS_tbConstructionSiteType.OrderBy(m => m.ID).Take(100).ToList();
                    model.Project_Type_All = new List<SelectListItem>();
                    var items = new List<SelectListItem>();

                    foreach (var CS_tbConstructionSiteType in model.CS_tbConstructionSiteType)
                    {
                        items.Add(new SelectListItem()
                        {
                            Value = CS_tbConstructionSiteType.Type,
                            Text = CS_tbConstructionSiteType.Type,
                        });
                    }

                    model.Project_Type_All = items;
                    return View("Edit", model);
                    //--------Add Dropdown for Type-------------------//              
                }
            }
            catch
            {
                using (OnlineShopDbContext db = new OnlineShopDbContext())
                {
                    //--------Add Dropdown for Type-------------------//
                    ProjectViewModel model = new ProjectViewModel();
                        //--------Select ID trả kết quả về View-----------//
                        model.SelectedProject = db.Projects.Find(id);
                        //--------Select ID trả kết quả về View-----------//
                    model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                    model.CS_tbConstructionSiteType = db.CS_tbConstructionSiteType.OrderBy(m => m.ID).Take(100).ToList();
                    model.Project_Type_All = new List<SelectListItem>();
                    var items = new List<SelectListItem>();

                    foreach (var CS_tbConstructionSiteType in model.CS_tbConstructionSiteType)
                    {
                        items.Add(new SelectListItem()
                        {
                            Value = CS_tbConstructionSiteType.Type,
                            Text = CS_tbConstructionSiteType.Type,
                        });
                    }

                    model.Project_Type_All = items;
                    return View("Edit", model);
                    //--------Add Dropdown for Type-------------------//
                }
            }
        }

        //
        // GET: /Admin/Project/Delete/5

        public ActionResult Delete(int id)
        {
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                //--------Add Dropdown for Type-------------------//
                ProjectViewModel model = new ProjectViewModel();
                    //--------Select ID trả kết quả về View-----------//
                    model.SelectedProject = db.Projects.Find(id);
                    //--------Select ID trả kết quả về View-----------//
                model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                model.CS_tbConstructionSiteType = db.CS_tbConstructionSiteType.OrderBy(m => m.ID).Take(100).ToList();
                model.Project_Type_All = new List<SelectListItem>();
                var items = new List<SelectListItem>();

                foreach (var CS_tbConstructionSiteType in model.CS_tbConstructionSiteType)
                {
                    items.Add(new SelectListItem()
                    {
                        Value = CS_tbConstructionSiteType.Type,
                        Text = CS_tbConstructionSiteType.Type,
                    });
                }

                model.Project_Type_All = items;
                return View(model);
                //--------Add Dropdown for Type-------------------//
            }
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
                    ProjectViewModel model = new ProjectViewModel();

                    Project Exsiting_Type = db.Projects.Find(id);
                    db.Projects.Remove(Exsiting_Type);
                    db.SaveChanges();

                    return View("Finish", model);
                }
            }
            catch
            {
                using (OnlineShopDbContext db = new OnlineShopDbContext())
                {
                    //--------Add Dropdown for Type-------------------//
                    ProjectViewModel model = new ProjectViewModel();
                        //--------Select ID trả kết quả về View-----------//
                        model.SelectedProject = db.Projects.Find(id);
                        //--------Select ID trả kết quả về View-----------//
                    model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                    model.CS_tbConstructionSiteType = db.CS_tbConstructionSiteType.OrderBy(m => m.ID).Take(100).ToList();
                    model.Project_Type_All = new List<SelectListItem>();
                    var items = new List<SelectListItem>();

                    foreach (var CS_tbConstructionSiteType in model.CS_tbConstructionSiteType)
                    {
                        items.Add(new SelectListItem()
                        {
                            Value = CS_tbConstructionSiteType.Type,
                            Text = CS_tbConstructionSiteType.Type,
                        });
                    }

                    model.Project_Type_All = items;
                    return View(model);
                    //--------Add Dropdown for Type-------------------//
                }
            }
        }
    }
}
