using System;
using System.Data.SqlClient; //add for SqlParameter pqhuy1987
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Framework;

namespace Models
{
    public class WorkCountViewModel
    {
        public List<DateTime> SelectDate;
        public string DisplayMode { get; set; }
        public DateTime StartDate  { get; set; }
        public DateTime EndDate  { get; set; }
    }

    class WorkCountModels
    {

    }
}
