namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LLTC")]
    public partial class LLTC
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(50)]
        public string Main_Name_LLTC { get; set; }

        [StringLength(50)]
        public string Main_Name_Ower { get; set; }

        [StringLength(50)]
        public string Main_Name_Job { get; set; }

        [StringLength(50)]
        public string Main_Number { get; set; }

        [StringLength(50)]
        public string Main_Area { get; set; }

        public int? Main_Total_Number { get; set; }

        [StringLength(50)]
        public string Main_Rate { get; set; }

        [StringLength(100)]
        public string Main_Note { get; set; }

        [StringLength(50)]
        public string Main_Status { get; set; }
    }
}
