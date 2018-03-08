namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Catelory")]
    public partial class Catelory
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string Prj_Name { get; set; }

        [StringLength(100)]
        public string Unit_Name { get; set; }

        [StringLength(100)]
        public string Owner_Name { get; set; }

        public int? Phone_Number { get; set; }

        public int? Person_Number { get; set; }

        public DateTime? Create_Date { get; set; }

        public bool? Status { get; set; }
    }
}
