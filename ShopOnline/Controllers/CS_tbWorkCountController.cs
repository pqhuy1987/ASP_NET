using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Framework;

namespace ShopOnline.Controllers
{
    public class CS_tbWorkCountController : Controller
    {
        public ActionResult Index()
        {
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                CS_tbWorkCountViewModels model = new CS_tbWorkCountViewModels();
                model.CS_tbWorkCount = db.CS_tbWorkCount.OrderBy(m => m.ID).Take(100).ToList();

                //--------Add Dropdown for ProjectName-------------------//
                model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                model.Project_Name_All = new List<SelectListItem>();
                var items = new List<SelectListItem>();
                foreach (var CS_Project_Name in model.Project)
                {
                    items.Add(new SelectListItem()
                    {
                        Value = CS_Project_Name.ID.ToString(),
                        Text = CS_Project_Name.Project_Name,
                    });
                }
                model.Project_Name_All = items;
                //--------Add Dropdown for ProjectName-------------------//

                return View(model);
            }
        }

        //
        // GET: /CS_tbWorkCount/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /CS_tbWorkCount/Create

        public ActionResult Create()
        {
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                CS_tbWorkCountViewModels model = new CS_tbWorkCountViewModels();
                model.CS_tbWorkCount = db.CS_tbWorkCount.OrderBy(m => m.ID).Take(100).ToList();

                //--------Add Dropdown for ProjectName-------------------//
                model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                model.Project_Name_All = new List<SelectListItem>();
                var items = new List<SelectListItem>();
                foreach (var CS_Project_Name in model.Project)
                {
                    items.Add(new SelectListItem()
                    {
                        Value = CS_Project_Name.ID.ToString(),
                        Text = CS_Project_Name.Project_Name,
                    });
                }
                model.Project_Name_All = items;
                //--------Add Dropdown for ProjectName-------------------//

                return View("Create", model);
            }
        }

        //
        // POST: /CS_tbWorkCount/Create

        [HttpPost]
        public ActionResult Create(CS_tbWorkCountViewModels collection)
        {
            try
            {
                using (OnlineShopDbContext db = new OnlineShopDbContext())
                {
                    CS_tbWorkCount obj = new CS_tbWorkCount();
                    obj.tb_WorkCountProject_ID = collection.CS_tbWorkCount_Select.tb_WorkCountProject_ID;
                    obj.tb_WorkCountForDate = collection.CS_tbWorkCount_Select.tb_WorkCountForDate;
                    obj.tb_WorkCountName_Report = collection.CS_tbWorkCount_Select.tb_WorkCountName_Report;
                    obj.tb_WorkCountDateTime_Report = DateTime.Today;
                    db.CS_tbWorkCount.Add(obj);
                    db.SaveChanges();

                    //--------Add Dropdown for ProjectName-------------------//
                    CS_tbWorkCountViewModels model = new CS_tbWorkCountViewModels();
                    model.CS_tbWorkCount = db.CS_tbWorkCount.OrderBy(m => m.ID).Take(100).ToList();
                    model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                    model.Project_Name_All = new List<SelectListItem>();
                    var items = new List<SelectListItem>();
                    foreach (var CS_Project_Name in model.Project)
                    {
                        items.Add(new SelectListItem()
                        {
                            Value = CS_Project_Name.ID.ToString(),
                            Text = CS_Project_Name.Project_Name,
                        });
                    }
                    model.Project_Name_All = items;
                    //--------Add Dropdown for ProjectName-------------------//

                    return View("Create", model);
                }
            }
            catch
            {
                using (OnlineShopDbContext db = new OnlineShopDbContext())
                {
                    CS_tbWorkCountViewModels model = new CS_tbWorkCountViewModels();
                    model.CS_tbWorkCount = db.CS_tbWorkCount.OrderBy(m => m.ID).Take(100).ToList();

                    //--------Add Dropdown for ProjectName-------------------//
                    model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                    model.Project_Name_All = new List<SelectListItem>();
                    var items = new List<SelectListItem>();
                    foreach (var CS_Project_Name in model.Project)
                    {
                        items.Add(new SelectListItem()
                        {
                            Value = CS_Project_Name.ID.ToString(),
                            Text = CS_Project_Name.Project_Name,
                        });
                    }
                    model.Project_Name_All = items;
                    //--------Add Dropdown for ProjectName-------------------//

                    return View("Create", model);
                }
            }
        }

        //
        // GET: /CS_tbWorkCount/Edit/5

        public ActionResult Edit(int id)
        {
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                CS_tbWorkCountViewModels model = new CS_tbWorkCountViewModels();
                //--------Select ID trả kết quả về View-----------//
                model.CS_tbWorkCount_Select = db.CS_tbWorkCount.Find(id);
                model.CS_tbWorkCount = db.CS_tbWorkCount.OrderBy(m => m.ID).Take(100).ToList();

                //--------Add Dropdown for ProjectName-------------------//
                model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                model.Project_Name_All = new List<SelectListItem>();
                var items = new List<SelectListItem>();
                foreach (var CS_Project_Name in model.Project)
                {
                    items.Add(new SelectListItem()
                    {
                        Value = CS_Project_Name.ID.ToString(),
                        Text = CS_Project_Name.Project_Name,
                    });
                }
                model.Project_Name_All = items;
                //--------Add Dropdown for ProjectName-------------------//

                return View("Edit", model);
            }
        }

        //
        // POST: /CS_tbWorkCount/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, CS_tbWorkCountViewModels collection)
        {
            try
            {
                using (OnlineShopDbContext db = new OnlineShopDbContext())
                {
                    CS_tbWorkCountViewModels model = new CS_tbWorkCountViewModels();
                    

                    CS_tbWorkCount Exsiting_CS_tbWorkCount = db.CS_tbWorkCount.Find(id);

                    Exsiting_CS_tbWorkCount.tb_WorkCountProject_ID = collection.CS_tbWorkCount_Select.tb_WorkCountProject_ID;
                    Exsiting_CS_tbWorkCount.tb_WorkCountForDate = collection.CS_tbWorkCount_Select.tb_WorkCountForDate;
                    Exsiting_CS_tbWorkCount.tb_WorkCountName_Report = collection.CS_tbWorkCount_Select.tb_WorkCountName_Edit;
                    Exsiting_CS_tbWorkCount.tb_WorkCountDateTime_Report = collection.CS_tbWorkCount_Select.tb_WorkCountDateTime_Report;
                    Exsiting_CS_tbWorkCount.tb_WorkCountName_Edit = collection.CS_tbWorkCount_Select.tb_WorkCountName_Edit;
                    Exsiting_CS_tbWorkCount.tb_WorkCountDateTime_Edit = DateTime.Today;
                    db.SaveChanges();

                    model.CS_tbWorkCount_Select = db.CS_tbWorkCount.Find(id);
                    model.CS_tbWorkCount = db.CS_tbWorkCount.OrderBy(m => m.ID).Take(100).ToList();

                    //--------Add Dropdown for ProjectName-------------------//
                    model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                    model.Project_Name_All = new List<SelectListItem>();
                    var items = new List<SelectListItem>();
                    foreach (var CS_Project_Name in model.Project)
                    {
                        items.Add(new SelectListItem()
                        {
                            Value = CS_Project_Name.ID.ToString(),
                            Text = CS_Project_Name.Project_Name,
                        });
                    }
                    model.Project_Name_All = items;
                    //--------Add Dropdown for ProjectName-------------------//

                    return View("Edit", model);
                }
            }
            catch
            {
                using (OnlineShopDbContext db = new OnlineShopDbContext())
                {
                    CS_tbWorkCountViewModels model = new CS_tbWorkCountViewModels();
                    model.CS_tbWorkCount_Select = db.CS_tbWorkCount.Find(id);
                    model.CS_tbWorkCount = db.CS_tbWorkCount.OrderBy(m => m.ID).Take(100).ToList();

                    //--------Add Dropdown for ProjectName-------------------//
                    model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                    model.Project_Name_All = new List<SelectListItem>();
                    var items = new List<SelectListItem>();
                    foreach (var CS_Project_Name in model.Project)
                    {
                        items.Add(new SelectListItem()
                        {
                            Value = CS_Project_Name.ID.ToString(),
                            Text = CS_Project_Name.Project_Name,
                        });
                    }
                    model.Project_Name_All = items;
                    //--------Add Dropdown for ProjectName-------------------//

                    return View("Edit", model);
                }
            }
        }

        //
        // GET: /CS_tbWorkCount/Delete/5

        public ActionResult Delete(int id)
        {
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                CS_tbWorkCountViewModels model = new CS_tbWorkCountViewModels();


                model.CS_tbWorkCount_Select = db.CS_tbWorkCount.Find(id);
                model.CS_tbWorkCount = db.CS_tbWorkCount.OrderBy(m => m.ID).Take(100).ToList();

                //--------Add Dropdown for ProjectName-------------------//
                model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                model.Project_Name_All = new List<SelectListItem>();
                var items = new List<SelectListItem>();
                foreach (var CS_Project_Name in model.Project)
                {
                    items.Add(new SelectListItem()
                    {
                        Value = CS_Project_Name.ID.ToString(),
                        Text = CS_Project_Name.Project_Name,
                    });
                }
                model.Project_Name_All = items;
                //--------Add Dropdown for ProjectName-------------------//

                return View("Delete", model);
            }
        }

        //
        // POST: /CS_tbWorkCount/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, CS_tbWorkCountViewModels collection)
        {
            try
            {
                using (OnlineShopDbContext db = new OnlineShopDbContext())
                {
                    CS_tbWorkCountViewModels model  = new CS_tbWorkCountViewModels();
                    CS_tbWorkCount Exsiting_CS_tbWorkCount = db.CS_tbWorkCount.Find(id);
                    db.CS_tbWorkCount.Remove(Exsiting_CS_tbWorkCount);
                    db.SaveChanges();

                    model.CS_tbWorkCount_Select     = db.CS_tbWorkCount.Find(id);
                    model.CS_tbWorkCount            = db.CS_tbWorkCount.OrderBy(m => m.ID).Take(100).ToList();

                    //--------Add Dropdown for ProjectName-------------------//
                    model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                    model.Project_Name_All = new List<SelectListItem>();
                    var items = new List<SelectListItem>();
                    foreach (var CS_Project_Name in model.Project)
                    {
                        items.Add(new SelectListItem()
                        {
                            Value = CS_Project_Name.ID.ToString(),
                            Text = CS_Project_Name.Project_Name,
                        });
                    }
                    model.Project_Name_All = items;
                    //--------Add Dropdown for ProjectName-------------------//

                    return View("Finish", model);
                }
            }
            catch
            {
                using (OnlineShopDbContext db = new OnlineShopDbContext())
                {
                    CS_tbWorkCountViewModels model = new CS_tbWorkCountViewModels();
                    model.CS_tbWorkCount_Select = db.CS_tbWorkCount.Find(id);
                    model.CS_tbWorkCount = db.CS_tbWorkCount.OrderBy(m => m.ID).Take(100).ToList();

                    //--------Add Dropdown for ProjectName-------------------//
                    model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                    model.Project_Name_All = new List<SelectListItem>();
                    var items = new List<SelectListItem>();
                    foreach (var CS_Project_Name in model.Project)
                    {
                        items.Add(new SelectListItem()
                        {
                            Value = CS_Project_Name.ID.ToString(),
                            Text = CS_Project_Name.Project_Name,
                        });
                    }
                    model.Project_Name_All = items;
                    //--------Add Dropdown for ProjectName-------------------//

                    return View("Finish", model);
                }
            }
        }
    }
}
