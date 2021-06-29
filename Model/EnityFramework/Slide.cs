namespace Model.EnityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slide")]
    public partial class Slide
    {
        public int ID { get; set; }

        [StringLength(250)]
        [Display(Name = "HinhAnh")]
        public string Image { get; set; }

        [Display(Name = "ThuTuHienThi")]
        public int? DisplayOrder { get; set; }

        [StringLength(250)]
        [Display(Name = "URL")]
        public string Link { get; set; }

        [StringLength(250)]
        [Display(Name = "MoTa")]
        public string Description { get; set; }

        [Display(Name = "NgayTao")]
        public DateTime? CreatedDay { get; set; }

        [StringLength(50)]
        [Display(Name = "NguoiTao")]
        public string CreatedBy { get; set; }

        [Display(Name = "NgayChinhSua")]
        public DateTime? ModifiedDay { get; set; }

        [StringLength(50)]
        [Display(Name = "NguoiChinhSua")]
        public string ModifiedBy { get; set; }

        [Display(Name = "TrangThai")]
        public bool? Status { get; set; }
    }
}
