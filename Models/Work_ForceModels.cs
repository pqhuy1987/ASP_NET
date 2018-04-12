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
    public class Work_ForceViewModel
    {
        public List<Work_Force> Work_Force { get; set; }
        public List<Catelory> Catelory { get; set; }
        public Work_Force SelectedWork_Force { get; set; }

        public string DisplayMode { get; set; }
    }

    class Work_ForceModels
    {
        private OnlineShopDbContext context = null;

        public Work_ForceModels()
        {
            context = new OnlineShopDbContext();
        }
    }
}
