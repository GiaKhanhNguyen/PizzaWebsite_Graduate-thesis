namespace Model.EnityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductCategory()
        {
            Products = new HashSet<Product>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name ="TenDanhMuc")]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Alias")]
        public string Alias { get; set; }

        [Display(Name = "ThuTuHienThi")]
        public int? DisplayOrder { get; set; }

        [StringLength(250)]
        [Display(Name = "Icon")]
        public string Icon { get; set; }

        [StringLength(250)]
        [Display(Name = "TieuDeSeo")]
        public string SeoTitile { get; set; }

        [Display(Name = "NgayChinhSua")]
        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        [Display(Name = "NguoiChinhSua")]
        public string ModifiedBy { get; set; }

        [Display(Name = "NgayTao")]
        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        [Display(Name = "NguoiTao")]
        public string CreatedBy { get; set; }

        [Display(Name = "TrangThai")]
        public bool Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
