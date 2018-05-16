namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LLTC")]
    public partial class LLTC
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Không được để trống nội dung này")]
        [StringLength(50, ErrorMessage = "Nội dung nhập vào không quá 50 ký tự")]
        public string Main_Name_LLTC { get; set; }

        [Required(ErrorMessage = "Không được để trống nội dung này")]
        [StringLength(50, ErrorMessage = "Nội dung nhập vào không quá 50 ký tự")]
        public string Main_Name_Ower { get; set; }

        [Required(ErrorMessage = "Không được để trống nội dung này")]
        [StringLength(50, ErrorMessage = "Nội dung nhập vào không quá 50 ký tự")]
        public string Main_Name_Job { get; set; }

        [Required(ErrorMessage = "Không được để trống nội dung này")]
        [StringLength(50, ErrorMessage = "Nội dung nhập vào không quá 50 ký tự")]
        public string Main_Number { get; set; }

        [Required(ErrorMessage = "Không được để trống nội dung này")]
        [StringLength(50, ErrorMessage = "Nội dung nhập vào không quá 50 ký tự")]
        public string Main_Area { get; set; }

        [Required(ErrorMessage = "Không được để trống nội dung này")]
        [StringLength(50, ErrorMessage = "Nội dung nhập vào không quá 50 ký tự")]
        public string Main_Total_Number { get; set; }

        [Required(ErrorMessage = "Không được để trống nội dung này")]
        [StringLength(50, ErrorMessage = "Nội dung nhập vào không quá 50 ký tự")]
        public string Main_Rate { get; set; }

        [Required(ErrorMessage = "Không được để trống nội dung này")]
        [StringLength(100, ErrorMessage = "Nội dung nhập vào không quá 50 ký tự")]
        public string Main_Note { get; set; }

        [Required(ErrorMessage = "Không được để trống nội dung này")]
        [StringLength(50, ErrorMessage = "Nội dung nhập vào không quá 50 ký tự")]
        public string Main_Status { get; set; }

        [StringLength(50)]
        public string stSiteName_1 { get; set; }

        public int? stSiteRegisterNumber_1 { get; set; }

        [StringLength(50)]
        public string stSiteManager_1 { get; set; }

        [StringLength(50)]
        public string stSiteManagerPhone_1 { get; set; }

        [StringLength(50)]
        public string stSiteJob_1 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? stSiteStartDate_1 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? stSiteEndDate_1 { get; set; }

        [StringLength(50)]
        public string stSiteStatus_1 { get; set; }

        [StringLength(50)]
        public string stSiteName_2 { get; set; }

        public int? stSiteRegisterNumber_2 { get; set; }

        [StringLength(50)]
        public string stSiteManager_2 { get; set; }

        [StringLength(50)]
        public string stSiteManagerPhone_2 { get; set; }

        [StringLength(50)]
        public string stSiteJob_2 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? stSiteStartDate_2 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? stSiteEndDate_2 { get; set; }

        [StringLength(50)]
        public string stSiteStatus_2 { get; set; }

        [StringLength(50)]
        public string stSiteName_3 { get; set; }

        public int? stSiteRegisterNumber_3 { get; set; }

        [StringLength(50)]
        public string stSiteManager_3 { get; set; }

        [StringLength(50)]
        public string stSiteManagerPhone_3 { get; set; }

        [StringLength(50)]
        public string stSiteJob_3 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? stSiteStartDate_3 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? stSiteEndDate_3 { get; set; }

        [StringLength(50)]
        public string stSiteStatus_3 { get; set; }

        [StringLength(50)]
        public string stSiteName_4 { get; set; }

        public int? stSiteRegisterNumber_4 { get; set; }

        [StringLength(50)]
        public string stSiteManager_4 { get; set; }

        [StringLength(50)]
        public string stSiteManagerPhone_4 { get; set; }

        [StringLength(50)]
        public string stSiteJob_4 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? stSiteStartDate_4 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? stSiteEndDate_4 { get; set; }

        [StringLength(50)]
        public string stSiteStatus_4 { get; set; }

        [StringLength(50)]
        public string stSiteName_5 { get; set; }

        public int? stSiteRegisterNumber_5 { get; set; }

        [StringLength(50)]
        public string stSiteManager_5 { get; set; }

        [StringLength(50)]
        public string stSiteManagerPhone_5 { get; set; }

        [StringLength(50)]
        public string stSiteJob_5 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? stSiteStartDate_5 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? stSiteEndDate_5 { get; set; }

        [StringLength(50)]
        public string stSiteStatus_5 { get; set; }
    }
}
