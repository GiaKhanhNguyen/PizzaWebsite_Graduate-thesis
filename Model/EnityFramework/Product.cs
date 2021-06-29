namespace Model.EnityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            OrderDetailsNoLogins = new HashSet<OrderDetailsNoLogin>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "TenSanPham")]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Alias")]
        public string Alias { get; set; }

        [Display(Name = "MaDanhMuc")]
        public int CategoryID { get; set; }

        [StringLength(500)]
        [Display(Name = "HinhAnh")]
        public string Image { get; set; }

        [Display(Name = "Gia")]
        public decimal Price { get; set; }

        [Display(Name = "GiaGoc")]
        public decimal? OriginalPrice { get; set; }

        [StringLength(500)]
        [Display(Name = "ThanhPhan")]
        public string Description { get; set; }

        [Display(Name = "NgayTao")]
        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        [Display(Name = "NguoiTao")]
        public string CreatedBy { get; set; }

        [Display(Name = "NgayChinhSua")]
        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        [Display(Name = "NguoiChinhSua")]
        public string ModifiedBy { get; set; }

        [Display(Name = "TrangThai")]
        public bool Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetailsNoLogin> OrderDetailsNoLogins { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
    }
}
