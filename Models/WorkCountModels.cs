using System;
using System.Data.SqlClient; //add for SqlParameter pqhuy1987
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Framework;
using System.Web.Mvc;

namespace Models
{
    public class WorkCountViewModel
    {
        public List<DateTime> SelectDate;
        public List<Catelory> Catelory { get; set; }
        public List<Project> Project { get; set; }
        public Catelory SelectedProject { get; set; }

        public List<Catelory> Catelory_Project { get; set; }
        public List<SelectListItem> ProjectAll { get; set; }

        public string DisplayMode { get; set; }
        public DateTime StartDate  { get; set; }
        public DateTime EndDate  { get; set; }
    }

    class WorkCountModels
    {

    }
}
