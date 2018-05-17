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
    public class LLTCViewModel
    {
        public List<LLTC>                   LLTC                { get; set; }
        public LLTC                         SelectedLLTC        { get; set; }

        public List<CS_tbWorkTypeViewModel> CS_tbWorkTypeViewModel { get; set; }

        public string                       DisplayMode         { get; set; }
        public List<Project>                Project             { get; set; }
        public List<SelectListItem>         Project_Name_All    { get; set; }
    }

    class LLTCModels
    {
        private OnlineShopDbContext context = null;

        public LLTCModels()
        {
            context = new OnlineShopDbContext();
        }
    }
}