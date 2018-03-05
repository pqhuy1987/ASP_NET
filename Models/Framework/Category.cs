namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel; // Need for DisplayName;

    [Table("Category")]
    public partial class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(10,ErrorMessage="Số ký tự tối đa là 10")]
        [DisplayName("TÊN ĐỘI THI CÔNG")]
        [Required(ErrorMessage="Bạn chưa nhập tên danh mục")]
        public string Name { get; set; }

        [StringLength(50)]
        [DisplayName("THUỘC CÔNG TRƯỜNG")]
        public string Alias { get; set; }

        [DisplayName("LOẠI ĐỘI")]
        public int? ParentID { get; set; }

        [DisplayName("NGƯỜI ĐẠI DIỆN")]
        public DateTime? CreateData { get; set; }

        [DisplayName("ĐIỆN THOẠI")]
        [Range(0,Int32.MaxValue,ErrorMessage="Bạn phải nhập số")]
        public int? Order { get; set; }

        [DisplayName("QUÂN SỐ")]
        public bool? Status { get; set; }
    }
}
