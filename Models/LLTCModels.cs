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
        public List<LLTC>   LLTC            { get; set; }
        public List<Catelory> Catelory      { get; set; }
        public LLTC         SelectedLLTC    { get; set; }

        public string       DisplayMode     { get; set; }
    }
}