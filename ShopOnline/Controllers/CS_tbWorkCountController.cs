using System;
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
    public class CS_tbWorkCountController : Controller
    {
        public ActionResult Index()
        {
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                CS_tbWorkCountViewModels model = new CS_tbWorkCountViewModels();
                model.CS_tbWorkCount = db.CS_tbWorkCount.OrderBy(m => m.ID).ToList();

                //--------Add Dropdown for ProjectName-------------------//
                model.Project = db.Projects.OrderBy(m => m.ID).ToList();
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
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                CS_tbWorkCountViewModels model = new CS_tbWorkCountViewModels();
                //--------Select ID trả kết quả về View-----------//

                model.CS_tbWorkCount_Select = db.CS_tbWorkCount.Find(id);
                model.CS_tbWorkCount_Sub = db.CS_tbWorkCount_Sub.Where(m => m.CS_tbWorkCount_ID == id).ToList();
                model.CS_tbLLTCTypeSub      = new List<CS_tbLLTCTypeSub>();
                model.LLTC_temp             = new List<LLTC>();
                model.CS_tbWorkType_temp    = new List<CS_tbWorkType>();

                int j = 0;
                foreach (var CS_tbWorkCount_Sub in model.CS_tbWorkCount_Sub)
                {
                    CS_tbLLTCTypeSub obj_temp = db.CS_tbLLTCTypeSub.Find(CS_tbWorkCount_Sub.CS_tbLLTCTypeSub_ID);
                    model.CS_tbLLTCTypeSub.Add(obj_temp);
                    LLTC obj_temp_2 = db.LLTCs.Find(CS_tbWorkCount_Sub.CS_LLTC_ID);
                    model.LLTC_temp.Add(obj_temp_2);
                    CS_tbWorkType obj_temp_3 = db.CS_tbWorkType.Find(model.CS_tbLLTCTypeSub[j].CS_tbLLTCNameJobDetailsSub);
                    model.CS_tbWorkType_temp.Add(obj_temp_3);
                    j++;                   
                }

                return View("Details", model);
            }
        }

        [HttpPost]
        public ActionResult DetailsEditGet(int id, CS_tbWorkCountViewModels collection)
        {
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                CS_tbWorkCountViewModels model = new CS_tbWorkCountViewModels();

                foreach (var CS_tbWorkCount_Sub_Temp in collection.CS_tbWorkCount_Sub)
                {
                    CS_tbWorkCount_Sub obj = db.CS_tbWorkCount_Sub.Find(CS_tbWorkCount_Sub_Temp.ID);
                    obj.CS_tbNumberDailyCount = CS_tbWorkCount_Sub_Temp.CS_tbNumberDailyCount;
                    db.SaveChanges();
                }

                //--------Select ID trả kết quả về View-----------//
                model.CS_tbWorkCount_Select = db.CS_tbWorkCount.Find(id);
                model.CS_tbWorkCount_Sub = db.CS_tbWorkCount_Sub.Where(m => m.CS_tbWorkCount_ID == id).ToList();

                int mTotalCount = 0;
                foreach (var CS_tbWorkCount_Sub in model.CS_tbWorkCount_Sub)
                {
                    mTotalCount = mTotalCount + (int)CS_tbWorkCount_Sub.CS_tbNumberDailyCount; 
                }
                CS_tbWorkCount objTotalCount = db.CS_tbWorkCount.Find(id);
                objTotalCount.tb_mTotalCount = mTotalCount;
                db.SaveChanges();
                model.CS_tbLLTCTypeSub = new List<CS_tbLLTCTypeSub>();
                model.LLTC_temp = new List<LLTC>();
                model.CS_tbWorkType_temp = new List<CS_tbWorkType>();

                int j = 0;
                model.CS_tbWorkCount_Sub = db.CS_tbWorkCount_Sub.Where(m => m.CS_tbWorkCount_ID == id).ToList();
                foreach (var CS_tbWorkCount_Sub in model.CS_tbWorkCount_Sub)
                {
                    CS_tbLLTCTypeSub obj_temp = db.CS_tbLLTCTypeSub.Find(CS_tbWorkCount_Sub.CS_tbLLTCTypeSub_ID);
                    model.CS_tbLLTCTypeSub.Add(obj_temp);
                    LLTC obj_temp_2 = db.LLTCs.Find(CS_tbWorkCount_Sub.CS_LLTC_ID);
                    model.LLTC_temp.Add(obj_temp_2);
                    CS_tbWorkType obj_temp_3 = db.CS_tbWorkType.Find(model.CS_tbLLTCTypeSub[j].CS_tbLLTCNameJobDetailsSub);
                    model.CS_tbWorkType_temp.Add(obj_temp_3);
                    j++;
                }

                return View("Details", model);
            }
        }
        //
        // GET: /CS_tbWorkCount/Create

        public ActionResult Create()
        {
            using (OnlineShopDbContext db = new OnlineShopDbContext())
            {
                CS_tbWorkCountViewModels model = new CS_tbWorkCountViewModels();
                model.CS_tbWorkCount = db.CS_tbWorkCount.OrderBy(m => m.ID).ToList();

                //--------Add Dropdown for ProjectName-------------------//
                model.Project = db.Projects.OrderBy(m => m.ID).ToList();
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
                    CS_tbWorkCountViewModels model = new CS_tbWorkCountViewModels();
                    var existingStatus = db.CS_tbWorkCount.FirstOrDefault(s => s.tb_WorkCountProject_ID == collection.CS_tbWorkCount_Select.tb_WorkCountProject_ID && s.tb_WorkCountForDate == collection.CS_tbWorkCount_Select.tb_WorkCountForDate);

                    if (existingStatus == null)
                    {
                        CS_tbWorkCount obj = new CS_tbWorkCount();
                        obj.tb_WorkCountProject_ID = collection.CS_tbWorkCount_Select.tb_WorkCountProject_ID;
                        obj.tb_WorkCountForDate = collection.CS_tbWorkCount_Select.tb_WorkCountForDate;
                        obj.tb_WorkCountName_Report = collection.CS_tbWorkCount_Select.tb_WorkCountName_Report;
                        obj.tb_WorkCountDateTime_Report = DateTime.Today;
                        obj.tb_mTotalCount = 0;
                        db.CS_tbWorkCount.Add(obj);
                        db.SaveChanges();
                        int id = obj.ID;
                        //--------Tạo Bảng Công Chi Tiết-------------------//
                        model.CS_tbLLTCTypeSub = db.CS_tbLLTCTypeSub.Where(m => m.CS_tbLLTCNameSiteID == collection.CS_tbWorkCount_Select.tb_WorkCountProject_ID).ToList();
                        foreach (var CS_LLTCTyleSub in model.CS_tbLLTCTypeSub)
                        {
                            CS_tbWorkCount_Sub obj_temp = new CS_tbWorkCount_Sub();
                            obj_temp.CS_tbWorkCount_ID = id;
                            obj_temp.CS_tbLLTCTypeSub_ID = CS_LLTCTyleSub.ID;
                            obj_temp.CS_LLTC_ID = CS_LLTCTyleSub.CS_tbLLTC_ID;
                            obj_temp.CS_tbNumberDailyCount = 0;
                            db.CS_tbWorkCount_Sub.Add(obj_temp);
                            db.SaveChanges();
                        }
                        model.ValidStatus = "Valid";
                        //--------Tạo Bảng Công Chi Tiết-------------------//
                    }
                    else
                    {
                        // set the status back to existing
                        model.ValidStatus = "Invalid";
                    }
                    //--------Add Dropdown for ProjectName-------------------//
                    model.CS_tbWorkCount = db.CS_tbWorkCount.OrderBy(m => m.ID).ToList();
                    model.Project = db.Projects.OrderBy(m => m.ID).ToList();
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
                    model.CS_tbWorkCount = db.CS_tbWorkCount.OrderBy(m => m.ID).ToList();

                    //--------Add Dropdown for ProjectName-------------------//
                    model.Project = db.Projects.OrderBy(m => m.ID).ToList();
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
                model.CS_tbWorkCount = db.CS_tbWorkCount.OrderBy(m => m.ID).ToList();

                //--------Add Dropdown for ProjectName-------------------//
                model.Project = db.Projects.OrderBy(m => m.ID).ToList();
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
                    model.CS_tbWorkCount = db.CS_tbWorkCount.OrderBy(m => m.ID).ToList();

                    //--------Add Dropdown for ProjectName-------------------//
                    model.Project = db.Projects.OrderBy(m => m.ID).ToList();
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
                    model.CS_tbWorkCount = db.CS_tbWorkCount.OrderBy(m => m.ID).ToList();

                    //--------Add Dropdown for ProjectName-------------------//
                    model.Project = db.Projects.OrderBy(m => m.ID).ToList();
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
                model.CS_tbWorkCount = db.CS_tbWorkCount.OrderBy(m => m.ID).ToList();

                //--------Add Dropdown for ProjectName-------------------//
                model.Project = db.Projects.OrderBy(m => m.ID).ToList();
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
                    model.CS_tbWorkCount            = db.CS_tbWorkCount.OrderBy(m => m.ID).ToList();

                    //--------Add Dropdown for ProjectName-------------------//
                    model.Project = db.Projects.OrderBy(m => m.ID).ToList();
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
                    model.CS_tbWorkCount = db.CS_tbWorkCount.OrderBy(m => m.ID).ToList();

                    //--------Add Dropdown for ProjectName-------------------//
                    model.Project = db.Projects.OrderBy(m => m.ID).ToList();
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
        public ActionResult Interop_WorkCount(CS_tbWorkCountViewModels collection)
        {
            //Microsoft.Office.Interop.Excel.Workbook workbook;
            Microsoft.Office.Interop.Excel._Worksheet oSheet;

            DataTable employeeTable = Load_LLTC_Excel_Report_WorkCount(collection.Excel_Date);

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
            DataTable Main_Name_LLTC = dataSet.Tables[0].DefaultView.ToTable(true, "Main_Name_LLTC", "Project_Name", "CS_WorkTypeMain", "Main_Status", "SubWorkType", "Main_Name_Ower", "Main_Number", "tb_mTotalCount", "CS_tbNumberDailyCount");
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

            for (int i = 0, j = 0; i < Project_MienBac.Length; i++)
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
                        oSheet.Cells[current_rownum, 1] = u + 1;
                        oSheet.Cells[current_rownum, 2] = LLTC_Row_Temp[v][0].ToString();
                        oSheet.Cells[current_rownum, 3] = LLTC_Row_Temp[v][3].ToString();
                        oSheet.Cells[current_rownum, 4] = LLTC_Row_Temp[v][4].ToString();
                        oSheet.Cells[current_rownum, 5] = LLTC_Row_Temp[v][5].ToString();
                        oSheet.Cells[current_rownum, 6] = LLTC_Row_Temp[v][6].ToString();
                        oSheet.Cells[current_rownum, 7] = LLTC_Row_Temp[v][8].ToString();
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
                        oSheet.Cells[current_rownum, 7] = LLTC_Row_Temp[v][8].ToString();
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
                        oSheet.Cells[current_rownum, 7] = LLTC_Row_Temp[v][8].ToString();
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

        System.Data.DataTable Load_LLTC_Excel_Report_WorkCount(DateTime WorkCountForDate)
        {
            DataTable result = new DataTable();
            SqlCommand cmd = null;
            SqlConnection conn = null;

            string dateWithFormat = WorkCountForDate.ToString("yyyy-MM-dd");
            conn = new SqlConnection(string.Format("Data Source=SRBDC.FDC.LOCAL; Initial Catalog=CWD; User id=sa; Password=P@ssw0rd"));
            try
            {
                cmd = new SqlCommand("LLTC_Get_List_By_All_Project_And_By_Date", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@WorkCountForDate", dateWithFormat);
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
