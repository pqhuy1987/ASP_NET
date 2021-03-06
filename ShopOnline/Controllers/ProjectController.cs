﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Framework;
using Excel = Microsoft.Office.Interop.Excel;

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
                model.Project                       = db.Projects.OrderBy(m => m.ID).ToList();
                model.CS_tbConstructionSiteType     = db.CS_tbConstructionSiteType.OrderBy(m => m.ID).ToList();
                //model.CS_tbLLTCTypeSub = db.CS_tbLLTCTypeSub.Where(m => m.CS_tbLLTCNameSiteID == model.SelectedProject.ID).OrderBy(m => new { m.CS_tbLLTCNameJobDetailsSub, m.ID }).ToList();
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
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                ProjectViewModel model = new ProjectViewModel();
                //--------Select ID trả kết quả về View-----------//
                model.SelectedProject   = db.Projects.Find(id);
                model.LLTC              = db.LLTCs.OrderBy(m => m.ID).ToList();
                model.CS_tbLLTCTypeSub  = db.CS_tbLLTCTypeSub.Where(m => m.CS_tbLLTCNameSiteID == model.SelectedProject.ID).OrderBy(m => new { m.CS_tbLLTCNameJobDetailsSub, m.ID}).ToList();
                model.CS_tbWorkType     = db.CS_tbWorkType.OrderBy(m => m.ID).ToList();
                model.CS_tbWorkTypeMain = db.CS_tbWorkTypeMain.OrderBy(m => m.ID).ToList();
                model.Project           = db.Projects.OrderBy(m => m.ID).ToList();

                //--------Add Dropdown for LLTCName-------------------//
                model.LLTC_Name_All = new List<SelectListItem>();
                var items = new List<SelectListItem>();
                foreach (var CS_LLTC_Name in model.LLTC)
                {
                    items.Add(new SelectListItem()
                    {
                        Value = CS_LLTC_Name.ID.ToString(),
                        Text = CS_LLTC_Name.Main_Name_LLTC,
                    });
                }
                model.LLTC_Name_All = items;
                //--------Add Dropdown for LLTCName-------------------//

                //--------Add Dropdown for Details Job-------------------//
                model.WorkTypeDetails_All = new List<SelectListItem>();
                var items_2 = new List<SelectListItem>();
                foreach (var CS_SubJob_Details in model.CS_tbWorkType)
                {
                    items_2.Add(new SelectListItem()
                    {
                        Value = CS_SubJob_Details.ID.ToString(),
                        Text = CS_SubJob_Details.SubWorkType,
                    });
                }
                model.WorkTypeDetails_All = items_2;
                //--------Add Dropdown for Details Job-------------------//

                //--------Add Dropdown for Core Job-------------------//
                model.WorkTypeCore_All = new List<SelectListItem>();
                var items_3 = new List<SelectListItem>();
                foreach (var CS_CoreJob in model.CS_tbWorkTypeMain)
                {
                    items_3.Add(new SelectListItem()
                    {
                        Value = CS_CoreJob.ID.ToString(),
                        Text = CS_CoreJob.CS_WorkTypeMain,
                    });
                }
                model.WorkTypeCore_All = items_3;
                //--------Add Dropdown for Core Job-------------------//

                //--------Add Dropdown for Project All-------------------//
                model.Project_All = new List<SelectListItem>();
                var items_4 = new List<SelectListItem>();
                foreach (var CS_Project in model.Project)
                {
                    items_4.Add(new SelectListItem()
                    {
                        Value = CS_Project.ID.ToString(),
                        Text = CS_Project.Project_Name ,
                    });
                }
                model.Project_All = items_4;
                //--------Add Dropdown for Project All-------------------//
                model.DisplayMode = "Index";

                return View("Details", model);
            }
        }

        //
        // GET: /Admin/Project/Details/5

        public ActionResult DetailsEditGet(int id, int LLTC_ID)
        {
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                ProjectViewModel model = new ProjectViewModel();
                //--------Select ID trả kết quả về View-----------//
                model.CS_tbLLTCTypeSub_Select = db.CS_tbLLTCTypeSub.Find(LLTC_ID);
                model.SelectedProject = db.Projects.Find(id);
                model.LLTC = db.LLTCs.OrderBy(m => m.ID).ToList();
                model.CS_tbLLTCTypeSub = db.CS_tbLLTCTypeSub.Where(m => m.CS_tbLLTCNameSiteID == model.SelectedProject.ID).OrderBy(m => new { m.CS_tbLLTCNameJobDetailsSub, m.ID }).ToList();
                model.CS_tbWorkType = db.CS_tbWorkType.OrderBy(m => m.ID).ToList();
                model.CS_tbWorkTypeMain = db.CS_tbWorkTypeMain.OrderBy(m => m.ID).ToList();
                model.Project = db.Projects.OrderBy(m => m.ID).ToList();

                //--------Add Dropdown for LLTCName-------------------//
                model.LLTC_Name_All = new List<SelectListItem>();
                var items = new List<SelectListItem>();
                foreach (var CS_LLTC_Name in model.LLTC)
                {
                    items.Add(new SelectListItem()
                    {
                        Value = CS_LLTC_Name.ID.ToString(),
                        Text = CS_LLTC_Name.Main_Name_LLTC,
                    });
                }
                model.LLTC_Name_All = items;
                //--------Add Dropdown for LLTCName-------------------//

                //--------Add Dropdown for Details Job-------------------//
                model.WorkTypeDetails_All = new List<SelectListItem>();
                var items_2 = new List<SelectListItem>();
                foreach (var CS_SubJob_Details in model.CS_tbWorkType)
                {
                    items_2.Add(new SelectListItem()
                    {
                        Value = CS_SubJob_Details.ID.ToString(),
                        Text = CS_SubJob_Details.SubWorkType,
                    });
                }
                model.WorkTypeDetails_All = items_2;
                //--------Add Dropdown for Details Job-------------------//

                //--------Add Dropdown for Core Job-------------------//
                model.WorkTypeCore_All = new List<SelectListItem>();
                var items_3 = new List<SelectListItem>();
                foreach (var CS_CoreJob in model.CS_tbWorkTypeMain)
                {
                    items_3.Add(new SelectListItem()
                    {
                        Value = CS_CoreJob.ID.ToString(),
                        Text = CS_CoreJob.CS_WorkTypeMain,
                    });
                }
                model.WorkTypeCore_All = items_3;
                //--------Add Dropdown for Core Job-------------------//

                //--------Add Dropdown for Project All-------------------//
                model.Project_All = new List<SelectListItem>();
                var items_4 = new List<SelectListItem>();
                foreach (var CS_Project in model.Project)
                {
                    items_4.Add(new SelectListItem()
                    {
                        Value = CS_Project.ID.ToString(),
                        Text = CS_Project.Project_Name,
                    });
                }
                model.Project_All = items_4;
                //--------Add Dropdown for Project All-------------------//
                model.DisplayMode = "Edit";

                return View("Details", model);
            }
        }
        //
        // GET: /Admin/Project/Details/5

        public ActionResult DetailsGetList(int id, ProjectViewModel collection )
        {
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                ProjectViewModel model = new ProjectViewModel();
                //--------Select ID trả kết quả về View-----------//
                model.SelectedProject = db.Projects.Find(id);
                model.LLTC_Select = db.LLTCs.Find(collection.CS_tbLLTCTypeSub_Select.CS_tbLLTC_ID);
                model.CS_tbLLTCTypeSub = db.CS_tbLLTCTypeSub.Where(m => m.CS_tbLLTCNameSiteID == model.SelectedProject.ID).OrderBy(m => new { m.CS_tbLLTCNameJobDetailsSub, m.ID }).ToList();
                model.CS_tbWorkType = db.CS_tbWorkType.Where(m => m.CoreWorkType == model.LLTC_Select.Main_Name_Job).OrderBy(m => m.ID).ToList();
                model.Project = db.Projects.OrderBy(m => m.ID).ToList();
                model.LLTC = db.LLTCs.OrderBy(m => m.ID).ToList();
 
                //--------Add Dropdown for LLTCName-------------------//
                model.LLTC_Name_All = new List<SelectListItem>();
                var items = new List<SelectListItem>();
                foreach (var CS_LLTC_Name in model.LLTC)
                {
                    items.Add(new SelectListItem()
                    {
                        Value = CS_LLTC_Name.ID.ToString(),
                        Text = CS_LLTC_Name.Main_Name_LLTC,
                    });
                }
                model.LLTC_Name_All = items;
                //--------Add Dropdown for LLTCName-------------------//

                //--------Add Dropdown for Details Job-------------------//
                model.WorkTypeDetails_All = new List<SelectListItem>();
                var items_2 = new List<SelectListItem>();
                foreach (var CS_SubJob_Details in model.CS_tbWorkType)
                {
                    items_2.Add(new SelectListItem()
                    {
                        Value = CS_SubJob_Details.ID.ToString(),
                        Text = CS_SubJob_Details.SubWorkType,
                    });
                }
                model.WorkTypeDetails_All = items_2;
                //--------Add Dropdown for Details Job-------------------//

                model.DisplayMode = "Index";

                return View("Details", model);
            }
        }

        //
        // GET: /Admin/Project/Details/5

        public ActionResult DetailsGetEditList(int id, int LLTCSub_ID, ProjectViewModel collection)
        {
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                ProjectViewModel model = new ProjectViewModel();
                //--------Select ID trả kết quả về View-----------//
                model.CS_tbLLTCTypeSub_Select = db.CS_tbLLTCTypeSub.Find(LLTCSub_ID);
                model.SelectedProject = db.Projects.Find(id);
                model.LLTC_Select = db.LLTCs.Find(collection.CS_tbLLTCTypeSub_Select.CS_tbLLTC_ID);
                model.CS_tbLLTCTypeSub = db.CS_tbLLTCTypeSub.Where(m => m.CS_tbLLTCNameSiteID == model.SelectedProject.ID).OrderBy(m => new { m.CS_tbLLTCNameJobDetailsSub, m.ID }).ToList();
                model.CS_tbWorkType = db.CS_tbWorkType.Where(m => m.CoreWorkType == model.LLTC_Select.Main_Name_Job).OrderBy(m => m.ID).ToList();
                model.Project = db.Projects.OrderBy(m => m.ID).ToList();
                model.LLTC = db.LLTCs.OrderBy(m => m.ID).ToList();

                //--------Add Dropdown for LLTCName-------------------//
                model.LLTC_Name_All = new List<SelectListItem>();
                var items = new List<SelectListItem>();
                foreach (var CS_LLTC_Name in model.LLTC)
                {
                    items.Add(new SelectListItem()
                    {
                        Value = CS_LLTC_Name.ID.ToString(),
                        Text = CS_LLTC_Name.Main_Name_LLTC,
                    });
                }
                model.LLTC_Name_All = items;
                //--------Add Dropdown for LLTCName-------------------//

                //--------Add Dropdown for Details Job-------------------//
                model.WorkTypeDetails_All = new List<SelectListItem>();
                var items_2 = new List<SelectListItem>();
                foreach (var CS_SubJob_Details in model.CS_tbWorkType)
                {
                    items_2.Add(new SelectListItem()
                    {
                        Value = CS_SubJob_Details.ID.ToString(),
                        Text = CS_SubJob_Details.SubWorkType,
                    });
                }
                model.WorkTypeDetails_All = items_2;
                //--------Add Dropdown for Details Job-------------------//

                model.DisplayMode = "Edit";

                return View("Details", model);
            }
        }

        [HttpPost]
        public ActionResult DetailsPost(int id, ProjectViewModel collection)
        {
            try
            {
                using (OnlineShopDbContext db = new OnlineShopDbContext())
                {
                    ProjectViewModel model = new ProjectViewModel();

                    CS_tbLLTCTypeSub obj = new CS_tbLLTCTypeSub();

                    obj.CS_tbLLTC_ID = collection.CS_tbLLTCTypeSub_Select.CS_tbLLTC_ID;
                    obj.CS_tbLLTCNameSiteID = collection.CS_tbLLTCTypeSub_Select.CS_tbLLTCNameSiteID;
                    obj.CS_tbLLTCNumberRegisterSub = collection.CS_tbLLTCTypeSub_Select.CS_tbLLTCNumberRegisterSub;
                    obj.CS_tbLLTCNameJobDetailsSub = collection.CS_tbLLTCTypeSub_Select.CS_tbLLTCNameJobDetailsSub;
                    obj.CS_tbLLTCNameSiteManagerSub = collection.CS_tbLLTCTypeSub_Select.CS_tbLLTCNameSiteManagerSub;
                    obj.CS_tbLLTCNameSiteManagerMobileSub = collection.CS_tbLLTCTypeSub_Select.CS_tbLLTCNameSiteManagerMobileSub;
                    obj.CS_tbLLTCStartDateSub = collection.CS_tbLLTCTypeSub_Select.CS_tbLLTCStartDateSub;
                    obj.CS_tbLLTCEndDateSub = collection.CS_tbLLTCTypeSub_Select.CS_tbLLTCEndDateSub;
                    obj.CS_tbLLTCStatusSub = collection.CS_tbLLTCTypeSub_Select.CS_tbLLTCStatusSub;
                    db.CS_tbLLTCTypeSub.Add(obj);
                    db.SaveChanges();

                    //--------Select ID trả kết quả về View-----------//
                    model.SelectedProject = db.Projects.Find(id);
                    model.LLTC_Select = db.LLTCs.Find(collection.CS_tbLLTCTypeSub_Select.CS_tbLLTC_ID);
                    model.CS_tbLLTCTypeSub = db.CS_tbLLTCTypeSub.Where(m => m.CS_tbLLTCNameSiteID == model.SelectedProject.ID).OrderBy(m => new { m.CS_tbLLTCNameJobDetailsSub, m.ID }).ToList();
                    model.CS_tbWorkType = db.CS_tbWorkType.Where(m => m.CoreWorkType == model.LLTC_Select.Main_Name_Job).OrderBy(m => m.ID).ToList();
                    model.Project = db.Projects.OrderBy(m => m.ID).ToList();
                    model.LLTC = db.LLTCs.OrderBy(m => m.ID).ToList();

                    //--------Add Dropdown for LLTCName-------------------//
                    model.LLTC_Name_All = new List<SelectListItem>();
                    var items = new List<SelectListItem>();
                    foreach (var CS_LLTC_Name in model.LLTC)
                    {
                        items.Add(new SelectListItem()
                        {
                            Value = CS_LLTC_Name.ID.ToString(),
                            Text = CS_LLTC_Name.Main_Name_LLTC,
                        });
                    }
                    model.LLTC_Name_All = items;
                    //--------Add Dropdown for LLTCName-------------------//

                    //--------Add Dropdown for Details Job-------------------//
                    model.WorkTypeDetails_All = new List<SelectListItem>();
                    var items_2 = new List<SelectListItem>();
                    foreach (var CS_SubJob_Details in model.CS_tbWorkType)
                    {
                        items_2.Add(new SelectListItem()
                        {
                            Value = CS_SubJob_Details.ID.ToString(),
                            Text = CS_SubJob_Details.SubWorkType,
                        });
                    }
                    model.WorkTypeDetails_All = items_2;
                    //--------Add Dropdown for Details Job-------------------//
                    model.DisplayMode = "Index";

                    return View("Details", model);
                }
            }
            catch
            {
                using (OnlineShopDbContext db = new OnlineShopDbContext())
                {
                    ProjectViewModel model = new ProjectViewModel();
                    //--------Select ID trả kết quả về View-----------//
                    model.SelectedProject = db.Projects.Find(id);
                    model.LLTC = db.LLTCs.OrderBy(m => m.ID).ToList();
                    model.CS_tbLLTCTypeSub = db.CS_tbLLTCTypeSub.Where(m => m.CS_tbLLTCNameSiteID == model.SelectedProject.ID).OrderBy(m => new { m.CS_tbLLTCNameJobDetailsSub, m.ID }).ToList();
                    model.CS_tbWorkType = db.CS_tbWorkType.OrderBy(m => m.ID).ToList();
                    model.CS_tbWorkTypeMain = db.CS_tbWorkTypeMain.OrderBy(m => m.ID).ToList();
                    model.Project = db.Projects.OrderBy(m => m.ID).ToList();

                    //--------Add Dropdown for LLTCName-------------------//
                    model.LLTC_Name_All = new List<SelectListItem>();
                    var items = new List<SelectListItem>();
                    foreach (var CS_LLTC_Name in model.LLTC)
                    {
                        items.Add(new SelectListItem()
                        {
                            Value = CS_LLTC_Name.ID.ToString(),
                            Text = CS_LLTC_Name.Main_Name_LLTC,
                        });
                    }
                    model.LLTC_Name_All = items;
                    //--------Add Dropdown for LLTCName-------------------//

                    //--------Add Dropdown for Details Job-------------------//
                    model.WorkTypeDetails_All = new List<SelectListItem>();
                    var items_2 = new List<SelectListItem>();
                    foreach (var CS_SubJob_Details in model.CS_tbWorkType)
                    {
                        items_2.Add(new SelectListItem()
                        {
                            Value = CS_SubJob_Details.ID.ToString(),
                            Text = CS_SubJob_Details.SubWorkType,
                        });
                    }
                    model.WorkTypeDetails_All = items_2;
                    //--------Add Dropdown for Details Job-------------------//

                    //--------Add Dropdown for Core Job-------------------//
                    model.WorkTypeCore_All = new List<SelectListItem>();
                    var items_3 = new List<SelectListItem>();
                    foreach (var CS_CoreJob in model.CS_tbWorkTypeMain)
                    {
                        items_3.Add(new SelectListItem()
                        {
                            Value = CS_CoreJob.ID.ToString(),
                            Text = CS_CoreJob.CS_WorkTypeMain,
                        });
                    }
                    model.WorkTypeCore_All = items_3;
                    //--------Add Dropdown for Core Job-------------------//

                    //--------Add Dropdown for Project All-------------------//
                    model.Project_All = new List<SelectListItem>();
                    var items_4 = new List<SelectListItem>();
                    foreach (var CS_Project in model.Project)
                    {
                        items_4.Add(new SelectListItem()
                        {
                            Value = CS_Project.ID.ToString(),
                            Text = CS_Project.Project_Name,
                        });
                    }
                    model.Project_All = items_4;
                    //--------Add Dropdown for Project All-------------------//
                    model.DisplayMode = "Index";

                    return View("Details", model);
                }
            }
        }

        [HttpPost]
        public ActionResult DetailsEditPost(int id, int LLTCSub_ID, ProjectViewModel collection)
        {
            try
            {
                using (OnlineShopDbContext db = new OnlineShopDbContext())
                {
                    ProjectViewModel model = new ProjectViewModel();

                    CS_tbLLTCTypeSub obj = new CS_tbLLTCTypeSub();
                    obj = db.CS_tbLLTCTypeSub.Find(LLTCSub_ID);

                    obj.CS_tbLLTC_ID = collection.CS_tbLLTCTypeSub_Select.CS_tbLLTC_ID;
                    obj.CS_tbLLTCNameSiteID = collection.CS_tbLLTCTypeSub_Select.CS_tbLLTCNameSiteID;
                    obj.CS_tbLLTCNumberRegisterSub = collection.CS_tbLLTCTypeSub_Select.CS_tbLLTCNumberRegisterSub;
                    obj.CS_tbLLTCNameJobDetailsSub = collection.CS_tbLLTCTypeSub_Select.CS_tbLLTCNameJobDetailsSub;
                    obj.CS_tbLLTCNameSiteManagerSub = collection.CS_tbLLTCTypeSub_Select.CS_tbLLTCNameSiteManagerSub;
                    obj.CS_tbLLTCNameSiteManagerMobileSub = collection.CS_tbLLTCTypeSub_Select.CS_tbLLTCNameSiteManagerMobileSub;
                    obj.CS_tbLLTCStartDateSub = collection.CS_tbLLTCTypeSub_Select.CS_tbLLTCStartDateSub;
                    obj.CS_tbLLTCEndDateSub = collection.CS_tbLLTCTypeSub_Select.CS_tbLLTCEndDateSub;
                    obj.CS_tbLLTCStatusSub = collection.CS_tbLLTCTypeSub_Select.CS_tbLLTCStatusSub;
                    db.SaveChanges();

                    //--------Select ID trả kết quả về View-----------//
                    model.CS_tbLLTCTypeSub_Select = db.CS_tbLLTCTypeSub.Find(LLTCSub_ID);
                    model.SelectedProject = db.Projects.Find(id);
                    model.LLTC_Select = db.LLTCs.Find(collection.CS_tbLLTCTypeSub_Select.CS_tbLLTC_ID);
                    model.CS_tbLLTCTypeSub = db.CS_tbLLTCTypeSub.Where(m => m.CS_tbLLTCNameSiteID == model.SelectedProject.ID).OrderBy(m => new { m.CS_tbLLTCNameJobDetailsSub, m.ID }).ToList();
                    model.CS_tbWorkType = db.CS_tbWorkType.Where(m => m.CoreWorkType == model.LLTC_Select.Main_Name_Job).OrderBy(m => m.ID).ToList();
                    model.Project = db.Projects.OrderBy(m => m.ID).ToList();
                    model.LLTC = db.LLTCs.OrderBy(m => m.ID).ToList();

                    //--------Add Dropdown for LLTCName-------------------//
                    model.LLTC_Name_All = new List<SelectListItem>();
                    var items = new List<SelectListItem>();
                    foreach (var CS_LLTC_Name in model.LLTC)
                    {
                        items.Add(new SelectListItem()
                        {
                            Value = CS_LLTC_Name.ID.ToString(),
                            Text = CS_LLTC_Name.Main_Name_LLTC,
                        });
                    }
                    model.LLTC_Name_All = items;
                    //--------Add Dropdown for LLTCName-------------------//

                    //--------Add Dropdown for Details Job-------------------//
                    model.WorkTypeDetails_All = new List<SelectListItem>();
                    var items_2 = new List<SelectListItem>();
                    foreach (var CS_SubJob_Details in model.CS_tbWorkType)
                    {
                        items_2.Add(new SelectListItem()
                        {
                            Value = CS_SubJob_Details.ID.ToString(),
                            Text = CS_SubJob_Details.SubWorkType,
                        });
                    }
                    model.WorkTypeDetails_All = items_2;
                    //--------Add Dropdown for Details Job-------------------//
                    model.DisplayMode = "Edit";

                    return View("Details", model);
                }
            }
            catch
            {
                using (OnlineShopDbContext db = new OnlineShopDbContext())
                {
                    ProjectViewModel model = new ProjectViewModel();
                    //--------Select ID trả kết quả về View-----------//
                    model.SelectedProject = db.Projects.Find(id);
                    model.LLTC = db.LLTCs.OrderBy(m => m.ID).ToList();
                    model.CS_tbLLTCTypeSub = db.CS_tbLLTCTypeSub.Where(m => m.CS_tbLLTCNameSiteID == model.SelectedProject.ID).OrderBy(m => new { m.CS_tbLLTCNameJobDetailsSub, m.ID }).ToList();
                    model.CS_tbWorkType = db.CS_tbWorkType.OrderBy(m => m.ID).ToList();
                    model.CS_tbWorkTypeMain = db.CS_tbWorkTypeMain.OrderBy(m => m.ID).ToList();
                    model.Project = db.Projects.OrderBy(m => m.ID).ToList();

                    //--------Add Dropdown for LLTCName-------------------//
                    model.LLTC_Name_All = new List<SelectListItem>();
                    var items = new List<SelectListItem>();
                    foreach (var CS_LLTC_Name in model.LLTC)
                    {
                        items.Add(new SelectListItem()
                        {
                            Value = CS_LLTC_Name.ID.ToString(),
                            Text = CS_LLTC_Name.Main_Name_LLTC,
                        });
                    }
                    model.LLTC_Name_All = items;
                    //--------Add Dropdown for LLTCName-------------------//

                    //--------Add Dropdown for Details Job-------------------//
                    model.WorkTypeDetails_All = new List<SelectListItem>();
                    var items_2 = new List<SelectListItem>();
                    foreach (var CS_SubJob_Details in model.CS_tbWorkType)
                    {
                        items_2.Add(new SelectListItem()
                        {
                            Value = CS_SubJob_Details.ID.ToString(),
                            Text = CS_SubJob_Details.SubWorkType,
                        });
                    }
                    model.WorkTypeDetails_All = items_2;
                    //--------Add Dropdown for Details Job-------------------//

                    //--------Add Dropdown for Core Job-------------------//
                    model.WorkTypeCore_All = new List<SelectListItem>();
                    var items_3 = new List<SelectListItem>();
                    foreach (var CS_CoreJob in model.CS_tbWorkTypeMain)
                    {
                        items_3.Add(new SelectListItem()
                        {
                            Value = CS_CoreJob.ID.ToString(),
                            Text = CS_CoreJob.CS_WorkTypeMain,
                        });
                    }
                    model.WorkTypeCore_All = items_3;
                    //--------Add Dropdown for Core Job-------------------//

                    //--------Add Dropdown for Project All-------------------//
                    model.Project_All = new List<SelectListItem>();
                    var items_4 = new List<SelectListItem>();
                    foreach (var CS_Project in model.Project)
                    {
                        items_4.Add(new SelectListItem()
                        {
                            Value = CS_Project.ID.ToString(),
                            Text = CS_Project.Project_Name,
                        });
                    }
                    model.Project_All = items_4;
                    //--------Add Dropdown for Project All-------------------//
                    model.DisplayMode = "Edit";

                    return View("Details", model);
                }
            }
        }

        [HttpPost]
        public ActionResult DetailsSub(int id, int LLTC_ID, int display)
        {
            //--------Add Dropdown for Project Name-------------------//
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                ProjectViewModel model = new ProjectViewModel();
                //--------Select ID trả kết quả về View-----------//
                if (display != 1)
                {
                    display = 1;
                    model.DisplayModeSub = display;
                }
                else
                {
                    display = 2;
                    model.DisplayModeSub = display;
                }
                model.LLTC_Select = db.LLTCs.Find(LLTC_ID);
                model.SelectedProject = db.Projects.Find(id);
                model.LLTC = db.LLTCs.OrderBy(m => m.ID).ToList();
                model.CS_tbLLTCTypeSub = db.CS_tbLLTCTypeSub.Where(m => m.CS_tbLLTCNameSiteID == model.SelectedProject.ID).OrderBy(m => m.ID).ToList();
                model.CS_tbWorkType = db.CS_tbWorkType.OrderBy(m => m.ID).ToList();

                //--------Add Dropdown for LLTCName-------------------//
                model.LLTC_Name_All = new List<SelectListItem>();
                var items = new List<SelectListItem>();
                foreach (var CS_LLTC_Name in model.LLTC)
                {
                    items.Add(new SelectListItem()
                    {
                        Value = CS_LLTC_Name.ID.ToString(),
                        Text = CS_LLTC_Name.Main_Name_LLTC,
                    });
                }
                model.LLTC_Name_All = items;
                //--------Add Dropdown for LLTCName-------------------//

                //--------Add Dropdown for Details Job-------------------//
                model.WorkTypeDetails_All = new List<SelectListItem>();
                var items_2 = new List<SelectListItem>();
                foreach (var CS_SubJob_Details in model.CS_tbWorkType)
                {
                    items_2.Add(new SelectListItem()
                    {
                        Value = CS_SubJob_Details.ID.ToString(),
                        Text = CS_SubJob_Details.SubWorkType,
                    });
                }
                model.WorkTypeDetails_All = items_2;
                //--------Add Dropdown for Details Job-------------------//

                return View("Details", model);
            }
            //--------Add Dropdown for Project Name-------------------//
        }

        //
        // GET: /Admin/Project/Create

        public ActionResult Create()
        {
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                //--------Add Dropdown for Type-------------------//
                ProjectViewModel model = new ProjectViewModel();
                model.Project = db.Projects.OrderBy(m => m.ID).ToList();
                model.CS_tbConstructionSiteType = db.CS_tbConstructionSiteType.OrderBy(m => m.ID).ToList();
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
                        model.Project = db.Projects.OrderBy(m => m.ID).ToList();
                        model.CS_tbConstructionSiteType = db.CS_tbConstructionSiteType.OrderBy(m => m.ID).ToList();
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
                    model.Project = db.Projects.OrderBy(m => m.ID).ToList();
                    model.CS_tbConstructionSiteType = db.CS_tbConstructionSiteType.OrderBy(m => m.ID).ToList();
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
                model.Project = db.Projects.OrderBy(m => m.ID).ToList();
                model.CS_tbConstructionSiteType = db.CS_tbConstructionSiteType.OrderBy(m => m.ID).ToList();
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
                    Exsiting_Project.Site_Manager = collection.SelectedProject.Site_Manager;
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
                    model.Project = db.Projects.OrderBy(m => m.ID).ToList();
                    model.CS_tbConstructionSiteType = db.CS_tbConstructionSiteType.OrderBy(m => m.ID).ToList();
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
                    model.Project = db.Projects.OrderBy(m => m.ID).ToList();
                    model.CS_tbConstructionSiteType = db.CS_tbConstructionSiteType.OrderBy(m => m.ID).ToList();
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
                model.Project = db.Projects.OrderBy(m => m.ID).ToList();
                model.CS_tbConstructionSiteType = db.CS_tbConstructionSiteType.OrderBy(m => m.ID).ToList();
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
                    model.Project = db.Projects.OrderBy(m => m.ID).ToList();
                    model.CS_tbConstructionSiteType = db.CS_tbConstructionSiteType.OrderBy(m => m.ID).ToList();
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

        public void killExcel()
        {
            System.Diagnostics.Process[] PROC = System.Diagnostics.Process.GetProcessesByName("EXCEL");
            foreach (System.Diagnostics.Process PK in PROC)
            {
                if (PK.MainWindowTitle.Length == 0)
                {
                    PK.Kill();
                }
            }
        }

        [HttpPost]
        public ActionResult Interop()
        {
            //Microsoft.Office.Interop.Excel.Workbook workbook;
            Microsoft.Office.Interop.Excel._Worksheet oSheet;

            DataTable employeeTable = Load_LLTC_Excel_Report();

            //Create a DataSet with the existing DataTables
            DataSet dataSet = new DataSet("Organization");
            dataSet.Tables.Add(employeeTable);

            //Creating Object of Microsoft.Office.Interop.Excel and creating a Workbook
            var excelApp = new Excel.Application();

            //specify the file name where its actually exist  
            string filepath = Server.MapPath(@"~/Reports/Danh_sách_LLTC_theo_công_trường_ba_miền.xlsx");
            string filepathSave = Server.MapPath(@"~/Reports/");

            List<int> Section_RowNum = new List<int>();

            int current_rownum = 4;
            Excel.Workbook WB = excelApp.Workbooks.Open(filepath);
            oSheet = (Microsoft.Office.Interop.Excel._Worksheet)WB.ActiveSheet;


            DataTable SiteName_Area = dataSet.Tables[0].DefaultView.ToTable(true, "Project_Name", "Site_Area");
            DataTable JobMain = dataSet.Tables[0].DefaultView.ToTable(true, "CS_WorkTypeMain");
            DataTable Main_Name_LLTC = dataSet.Tables[0].DefaultView.ToTable(true, "Main_Name_LLTC", "Project_Name", "CS_WorkTypeMain", "Main_Status", "SubWorkType", "Main_Name_Ower", "Main_Number", "Main_Total_Number");
            DataRow[] Project_MienBac = SiteName_Area.Select("Site_Area = 'Miền Bắc'");
            DataRow[] Project_MienTrung = SiteName_Area.Select("Site_Area = 'Miền Trung'");
            DataRow[] Project_MienNam = SiteName_Area.Select("Site_Area = 'Miền Nam'");

            Excel.Worksheet workSheet = (Excel.Worksheet)excelApp.Worksheets[1]; //creating excel worksheet
            workSheet.Name = "LLTC_Export"; //name of excel file

            //A --------------------------------------------- MIỀN BẮC ------------------------------------------------------------------

            oSheet.Range["A" + current_rownum, "G" + current_rownum].Interior.Color = System.Drawing.Color.FromArgb(255, 165, 0);
            oSheet.Range["A" + current_rownum, "G" + current_rownum].Font.Bold = true;
            oSheet.Cells[current_rownum, 1] = " ";
            oSheet.Cells[current_rownum, 2] = "MIỀN BẮC";
            Section_RowNum.Add(current_rownum);
            current_rownum++;

            //*------------------pqhuy1987-------------------
            //LINQ to get Column of dataset table

            for (int i = 0, j=0; i < Project_MienBac.Length; i++)
            {
                oSheet.Range["A" + current_rownum, "G" + current_rownum].Interior.Color = System.Drawing.Color.FromArgb(255, 255, 0);
                oSheet.Range["A" + current_rownum, "G" + current_rownum].Font.Bold = true;
                oSheet.Cells[current_rownum, 1] = " ";
                oSheet.Cells[current_rownum, 2] = Project_MienBac[j][0].ToString();
                current_rownum++;
                for (int k = 0, h = 0; k < JobMain.Rows.Count; k++)
                {
                    oSheet.Range["A" + current_rownum, "G" + current_rownum].Interior.Color = System.Drawing.Color.FromArgb(0, 255, 255);
                    oSheet.Range["A" + current_rownum, "G" + current_rownum].Font.Bold = true;
                    oSheet.Cells[current_rownum, 1] = " ";
                    oSheet.Cells[current_rownum, 2] = JobMain.Rows[h][0].ToString();
                    current_rownum++;

                    DataRow[] LLTC_Row_Temp = Main_Name_LLTC.Select("Project_Name =" + String.Format("'{0}'", Project_MienBac[j][0].ToString().Replace("'", "''")) + "AND CS_WorkTypeMain =" + String.Format("'{0}'", JobMain.Rows[h][0].ToString().Replace("'", "''")));

                    for (int v = 0, u = 0; u < LLTC_Row_Temp.Length; u++)
                    {
                        oSheet.Cells[current_rownum, 1] = u+1;
                        oSheet.Cells[current_rownum, 2] = LLTC_Row_Temp[v][0].ToString();
                        oSheet.Cells[current_rownum, 3] = LLTC_Row_Temp[v][3].ToString();
                        oSheet.Cells[current_rownum, 4] = LLTC_Row_Temp[v][4].ToString();
                        oSheet.Cells[current_rownum, 5] = LLTC_Row_Temp[v][5].ToString();
                        oSheet.Cells[current_rownum, 6] = LLTC_Row_Temp[v][6].ToString();
                        oSheet.Cells[current_rownum, 7] = LLTC_Row_Temp[v][7].ToString();
                        current_rownum++;
                        v++;
                    }
                    h++;                  
                }
                j++;
            }
            //A --------------------------------------------- MIỀN BẮC ------------------------------------------------------------------

            //B --------------------------------------------- MIỀN TRUNG ------------------------------------------------------------------

            oSheet.Range["A" + current_rownum, "G" + current_rownum].Interior.Color = System.Drawing.Color.FromArgb(255, 165, 0);
            oSheet.Range["A" + current_rownum, "G" + current_rownum].Font.Bold = true;
            oSheet.Cells[current_rownum, 1] = " ";
            oSheet.Cells[current_rownum, 2] = "MIỀN TRUNG";
            Section_RowNum.Add(current_rownum);
            current_rownum++;

            //*------------------pqhuy1987-------------------
            //LINQ to get Column of dataset table

            for (int i = 0, j = 0; i < Project_MienTrung.Length; i++)
            {
                oSheet.Range["A" + current_rownum, "G" + current_rownum].Interior.Color = System.Drawing.Color.FromArgb(255, 255, 0);
                oSheet.Range["A" + current_rownum, "G" + current_rownum].Font.Bold = true;
                oSheet.Cells[current_rownum, 1] = " ";
                oSheet.Cells[current_rownum, 2] = Project_MienTrung[j][0].ToString();
                current_rownum++;
                for (int k = 0, h = 0; k < JobMain.Rows.Count; k++)
                {
                    oSheet.Range["A" + current_rownum, "G" + current_rownum].Interior.Color = System.Drawing.Color.FromArgb(0, 255, 255);
                    oSheet.Range["A" + current_rownum, "G" + current_rownum].Font.Bold = true;
                    oSheet.Cells[current_rownum, 1] = " ";
                    oSheet.Cells[current_rownum, 2] = JobMain.Rows[h][0].ToString();
                    current_rownum++;

                    DataRow[] LLTC_Row_Temp = Main_Name_LLTC.Select("Project_Name =" + String.Format("'{0}'", Project_MienTrung[j][0].ToString().Replace("'", "''")) + "AND CS_WorkTypeMain =" + String.Format("'{0}'", JobMain.Rows[h][0].ToString().Replace("'", "''")));

                    for (int v = 0, u = 0; u < LLTC_Row_Temp.Length; u++)
                    {
                        oSheet.Cells[current_rownum, 1] = u + 1;
                        oSheet.Cells[current_rownum, 2] = LLTC_Row_Temp[v][0].ToString();
                        oSheet.Cells[current_rownum, 3] = LLTC_Row_Temp[v][3].ToString();
                        oSheet.Cells[current_rownum, 4] = LLTC_Row_Temp[v][4].ToString();
                        oSheet.Cells[current_rownum, 5] = LLTC_Row_Temp[v][5].ToString();
                        oSheet.Cells[current_rownum, 6] = LLTC_Row_Temp[v][6].ToString();
                        oSheet.Cells[current_rownum, 7] = LLTC_Row_Temp[v][7].ToString();
                        current_rownum++;
                        v++;
                    }
                    h++;
                }
                j++;
            }
            //B --------------------------------------------- MIỀN TRUNG ------------------------------------------------------------------

            //C --------------------------------------------- MIỀN NAM ------------------------------------------------------------------

            oSheet.Range["A" + current_rownum, "G" + current_rownum].Interior.Color = System.Drawing.Color.FromArgb(255, 165, 0);
            oSheet.Range["A" + current_rownum, "G" + current_rownum].Font.Bold = true;
            oSheet.Cells[current_rownum, 1] = " ";
            oSheet.Cells[current_rownum, 2] = "MIỀN NAM";
            Section_RowNum.Add(current_rownum);
            current_rownum++;

            //*------------------pqhuy1987-------------------
            //LINQ to get Column of dataset table

            for (int i = 0, j = 0; i < Project_MienNam.Length; i++)
            {
                oSheet.Range["A" + current_rownum, "G" + current_rownum].Interior.Color = System.Drawing.Color.FromArgb(255, 255, 0);
                oSheet.Range["A" + current_rownum, "G" + current_rownum].Font.Bold = true;
                oSheet.Cells[current_rownum, 1] = " ";
                oSheet.Cells[current_rownum, 2] = Project_MienNam[j][0].ToString();
                current_rownum++;
                for (int k = 0, h = 0; k < JobMain.Rows.Count; k++)
                {
                    oSheet.Range["A" + current_rownum, "G" + current_rownum].Interior.Color = System.Drawing.Color.FromArgb(0, 255, 255);
                    oSheet.Range["A" + current_rownum, "G" + current_rownum].Font.Bold = true;
                    oSheet.Cells[current_rownum, 1] = " ";
                    oSheet.Cells[current_rownum, 2] = JobMain.Rows[h][0].ToString();
                    current_rownum++;

                    DataRow[] LLTC_Row_Temp = Main_Name_LLTC.Select("Project_Name =" + String.Format("'{0}'", Project_MienNam[j][0].ToString().Replace("'", "''")) + "AND CS_WorkTypeMain =" + String.Format("'{0}'", JobMain.Rows[h][0].ToString().Replace("'", "''")));

                    for (int v = 0, u = 0; u < LLTC_Row_Temp.Length; u++)
                    {
                        oSheet.Cells[current_rownum, 1] = u + 1;
                        oSheet.Cells[current_rownum, 2] = LLTC_Row_Temp[v][0].ToString();
                        oSheet.Cells[current_rownum, 3] = LLTC_Row_Temp[v][3].ToString();
                        oSheet.Cells[current_rownum, 4] = LLTC_Row_Temp[v][4].ToString();
                        oSheet.Cells[current_rownum, 5] = LLTC_Row_Temp[v][5].ToString();
                        oSheet.Cells[current_rownum, 6] = LLTC_Row_Temp[v][6].ToString();
                        oSheet.Cells[current_rownum, 7] = LLTC_Row_Temp[v][7].ToString();
                        current_rownum++;
                        v++;
                    }
                    h++;
                }
                j++;
            }
            //C --------------------------------------------- MIỀN NAM ------------------------------------------------------------------

            //Saving the excel file to “e” directory
            excelApp.DisplayAlerts = false;
            workSheet.SaveAs(filepathSave + workSheet.Name);
            //excelApp.Visible = true;
            WB.Close(0);
            excelApp.Quit();

            try
            {
                string XlsPath = Server.MapPath(@"~/Reports/LLTC_Export.xlsx");
                FileInfo fileDet = new System.IO.FileInfo(XlsPath);
                Response.Clear();
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.UTF8;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(fileDet.Name));
                Response.AddHeader("Content-Length", fileDet.Length.ToString());
                Response.ContentType = "application/ms-excel";
                Response.WriteFile(fileDet.FullName);
                Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            killExcel();
            return RedirectToAction("Index");

        }

        System.Data.DataTable Load_LLTC_Excel_Report()
        {
            DataTable result = new DataTable();
            SqlCommand cmd = null;
            SqlConnection conn = null;
            conn = new SqlConnection(string.Format("Data Source=SRBDC.FDC.LOCAL; Initial Catalog=CWD; User id=sa; Password=P@ssw0rd"));
            try
            {
                cmd = new SqlCommand("LLTC_Get_List_By_All_Project", conn);
                cmd.CommandType = CommandType.StoredProcedure;
          
                conn.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                result.Load(rd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
            return result;
        }

    }
}
