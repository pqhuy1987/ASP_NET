using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Framework;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Models
{
    public class CS_tbWorkCountViewModels
    {
        public List<CS_tbWorkCount> CS_tbWorkCount { get; set; }
        public CS_tbWorkCount CS_tbWorkCount_Select { get; set; }

        public List<Project> Project { get; set; }
        //public List<CS_tbWorkType> CS_tbWorkType { get; set; }
        //public List<CS_tbWorkTypeMain> CS_tbWorkTypeMain { get; set; }

        public List<SelectListItem> Project_Name_All { get; set; }
        //public List<SelectListItem> WorkTypeDetails_All { get; set; }
        //public List<SelectListItem> WorkTypeMain_All { get; set; }

    }
}
