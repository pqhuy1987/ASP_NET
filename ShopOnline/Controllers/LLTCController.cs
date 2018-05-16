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

    public class LLTCController : Controller
    {
        //
        // GET: /LLTC/

        public ActionResult Index()
        {
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                LLTCViewModel model = new LLTCViewModel();
                model.LLTC = db.LLTCs.OrderBy(m => m.ID).Take(100).ToList();
                
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
            //--------Add Dropdown for Project Name-------------------//
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                LLTCViewModel model = new LLTCViewModel();
                model.LLTC = db.LLTCs.OrderBy(m => m.ID).Take(100).ToList();
                model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                model.Project_Name_All = new List<SelectListItem>();
                var items = new List<SelectListItem>();

                foreach (var CS_Project_Name in model.Project)
                {
                    items.Add(new SelectListItem()
                    {
                        Value = CS_Project_Name.Project_Name,
                        Text = CS_Project_Name.Project_Name,
                    });
                }
                model.Project_Name_All = items;
                return View(model);
            }
            //--------Add Dropdown for Project Name-------------------//
        }

        //
        // POST: /LLTC/Create

        [HttpPost]
        public ActionResult Create(LLTCViewModel collection)
        {
            try
            {
                using (OnlineShopDbContext db = new OnlineShopDbContext())
                {
                    LLTC obj = new LLTC();

                    obj.Main_Name_LLTC = collection.SelectedLLTC.Main_Name_LLTC;
                    obj.Main_Name_Ower = collection.SelectedLLTC.Main_Name_Ower;
                    obj.Main_Number = collection.SelectedLLTC.Main_Number;
                    obj.Main_Total_Number = collection.SelectedLLTC.Main_Total_Number;
                    obj.Main_Name_Job = collection.SelectedLLTC.Main_Name_Job;
                    obj.Main_Area       = collection.SelectedLLTC.Main_Area;
                    obj.Main_Status = collection.SelectedLLTC.Main_Status;
                    obj.Main_Rate = collection.SelectedLLTC.Main_Rate;
                    obj.Main_Note       = collection.SelectedLLTC.Main_Note;                                                                            
                    db.LLTCs.Add(obj);
                    db.SaveChanges();

                    //--------Add Dropdown for Project Name-------------------//
                    LLTCViewModel model = new LLTCViewModel();
                    model.LLTC = db.LLTCs.OrderBy(m => m.ID).Take(100).ToList();
                    model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                    model.Project_Name_All = new List<SelectListItem>();
                    var items = new List<SelectListItem>();

                    foreach (var CS_Project_Name in model.Project)
                    {
                        items.Add(new SelectListItem()
                        {
                            Value = CS_Project_Name.Project_Name,
                            Text = CS_Project_Name.Project_Name,
                        });
                    }
                    model.Project_Name_All = items;
                    return View(model);
                    //--------Add Dropdown for Project Name-------------------//
                }
            }
            catch
            {
                //--------Add Dropdown for Project Name-------------------//
                using (OnlineShopDbContext db = new OnlineShopDbContext())
                {
                    LLTCViewModel model = new LLTCViewModel();
                    model.LLTC = db.LLTCs.OrderBy(m => m.ID).Take(100).ToList();
                    model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                    model.Project_Name_All = new List<SelectListItem>();
                    var items = new List<SelectListItem>();

                    foreach (var CS_Project_Name in model.Project)
                    {
                        items.Add(new SelectListItem()
                        {
                            Value = CS_Project_Name.Project_Name,
                            Text = CS_Project_Name.Project_Name,
                        });
                    }
                    model.Project_Name_All = items;
                    return View(model);
                }
                //--------Add Dropdown for Project Name-------------------//
            }
        }

        //
        // GET: /LLTC/Edit/5

        public ActionResult Edit(int id)
        {
            //--------Add Dropdown for Project Name-------------------//
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                LLTCViewModel model = new LLTCViewModel();
                //--------Select ID trả kết quả về View-----------//
                model.SelectedLLTC = db.LLTCs.Find(id);
                //--------Add Dropdown for ProjectName-------------------//
                model.LLTC = db.LLTCs.OrderBy(m => m.ID).Take(100).ToList();
                model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                model.Project_Name_All = new List<SelectListItem>();
                var items = new List<SelectListItem>();

                foreach (var CS_Project_Name in model.Project)
                {
                    items.Add(new SelectListItem()
                    {
                        Value = CS_Project_Name.Project_Name,
                        Text = CS_Project_Name.Project_Name,
                    });
                }
                model.Project_Name_All = items;
                return View(model);
            }
            //--------Add Dropdown for Project Name-------------------//
        }

        //
        // POST: /LLTC/Edit/5

        [HttpPost]
        public ActionResult Save(int id, LLTCViewModel collection)
        {
            try
            {
                using (OnlineShopDbContext db = new OnlineShopDbContext())
                {
                    LLTCViewModel model = new LLTCViewModel();
                    //--------Select ID trả kết quả về View-----------//
                    model.SelectedLLTC = db.LLTCs.Find(id);
                    //--------Add Dropdown for ProjectName-------------------//
                    model.LLTC = db.LLTCs.OrderBy(m => m.ID).Take(100).ToList();
                    model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                    model.Project_Name_All = new List<SelectListItem>();
                    var items = new List<SelectListItem>();

                    foreach (var CS_Project_Name in model.Project)
                    {
                        items.Add(new SelectListItem()
                        {
                            Value = CS_Project_Name.Project_Name,
                            Text = CS_Project_Name.Project_Name,
                        });
                    }
                    model.Project_Name_All = items;
                    return View(model);
                }
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
            //--------Add Dropdown for Project Name-------------------//
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                LLTCViewModel model = new LLTCViewModel();
                //--------Select ID trả kết quả về View-----------//
                model.SelectedLLTC = db.LLTCs.Find(id);
                //--------Add Dropdown for ProjectName-------------------//
                model.LLTC = db.LLTCs.OrderBy(m => m.ID).Take(100).ToList();
                model.Project = db.Projects.OrderBy(m => m.ID).Take(100).ToList();
                model.Project_Name_All = new List<SelectListItem>();
                var items = new List<SelectListItem>();

                foreach (var CS_Project_Name in model.Project)
                {
                    items.Add(new SelectListItem()
                    {
                        Value = CS_Project_Name.Project_Name,
                        Text = CS_Project_Name.Project_Name,
                    });
                }
                model.Project_Name_All = items;
                return View(model);
            }
            //--------Add Dropdown for Project Name-------------------//
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
