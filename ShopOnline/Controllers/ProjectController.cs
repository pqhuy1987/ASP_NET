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
                model.Project                       = db.Projects.OrderBy(m => m.ID).ToList();
                model.CS_tbConstructionSiteType     = db.CS_tbConstructionSiteType.OrderBy(m => m.ID).ToList();
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
                model.CS_tbLLTCTypeSub  = db.CS_tbLLTCTypeSub.Where(m => m.CS_tbLLTCNameSiteID == model.SelectedProject.ID).OrderBy(m => m.ID).ToList();
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
                model.CS_tbLLTCTypeSub = db.CS_tbLLTCTypeSub.Where(m => m.CS_tbLLTCNameSiteID == model.SelectedProject.ID).OrderBy(m => m.ID).ToList();
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
                model.CS_tbLLTCTypeSub = db.CS_tbLLTCTypeSub.Where(m => m.CS_tbLLTCNameSiteID == model.SelectedProject.ID).OrderBy(m => m.ID).ToList();
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
                model.CS_tbLLTCTypeSub = db.CS_tbLLTCTypeSub.Where(m => m.CS_tbLLTCNameSiteID == model.SelectedProject.ID).OrderBy(m => m.ID).ToList();
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
                    model.CS_tbLLTCTypeSub = db.CS_tbLLTCTypeSub.Where(m => m.CS_tbLLTCNameSiteID == model.SelectedProject.ID).OrderBy(m => m.ID).ToList();
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
                    model.CS_tbLLTCTypeSub = db.CS_tbLLTCTypeSub.Where(m => m.CS_tbLLTCNameSiteID == model.SelectedProject.ID).OrderBy(m => m.ID).ToList();
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
                    model.CS_tbLLTCTypeSub = db.CS_tbLLTCTypeSub.Where(m => m.CS_tbLLTCNameSiteID == model.SelectedProject.ID).OrderBy(m => m.ID).ToList();
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
                    model.CS_tbLLTCTypeSub = db.CS_tbLLTCTypeSub.Where(m => m.CS_tbLLTCNameSiteID == model.SelectedProject.ID).OrderBy(m => m.ID).ToList();
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
    }
}
