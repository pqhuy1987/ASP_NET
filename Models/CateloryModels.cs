using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Framework;
using System.Data.SqlClient;

namespace Models
{
    public class CateloryViewModel
    {
        public List<Catelory> Catelory  { get; set; }
        public List<Project>  Project   { get; set; }

        public Catelory SelectedCatelory { get; set; }
        public string DisplayMode { get; set; }
    }

    public class CateloryModels
    {
        private OnlineShopDbContext context = null;

        public CateloryModels()
        {
            context = new OnlineShopDbContext();
        }
    }
}
