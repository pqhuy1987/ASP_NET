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
    public class ProjectViewModel
    {
        public List<Project>            Project                 { get; set; }
        public Project                  SelectedProject         { get; set; }
        public List<Catelory>           Catelory                { get; set; }
        public List<Catelory>           Catelory_Project        { get; set; }
        public string DisplayMode                               { get; set; }

        public List<CS_tbConstructionSiteType>  CS_tbConstructionSiteType   { get; set; }
        public List<SelectListItem>             Project_Type_All            { get; set; }
    }

    public class ProjectModels
    {
        private OnlineShopDbContext context = null;

        public ProjectModels()
        {
            context = new OnlineShopDbContext();
        }

        public List<Project> ListAll()
        {
            var list = context.Database.SqlQuery<Project>("Sp_Project_ListAll").ToList();
            return list;
        }

        public int Create(string ProjectName)
        {
            object[] parameters =
            {
                new SqlParameter ("@ProjectName",ProjectName),

            };
            int res = context.Database.ExecuteSqlCommand("Sp_Project_Insert @ProjectName", parameters);
            return res;
        }
    }
}
