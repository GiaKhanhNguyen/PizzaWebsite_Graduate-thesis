namespace Model.EnityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        [Display(Name = "TenDangNhap")]
        public string UserName { get; set; }

        [StringLength(50)]
        [Display(Name = "MatKhau")]
        public string Password { get; set; }

        [StringLength(20)]
        [Display(Name = "QuyenTaiKhoan")]
        public string GroupID { get; set; }

        [StringLength(50)]
        [Display(Name = "TenNguoiDung")]
        public string Name { get; set; }

        [StringLength(50)]
        [Display(Name = "DiaChi")]
        public string Address { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(10)]
        [Display(Name = "GioiTinh")]
        public string Gender { get; set; }

        [StringLength(500)]
        public string Avatar { get; set; }

        [StringLength(50)]
        [Display(Name = "DienThoai")]
        public string Phone { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [Display(Name = "TrangThai")]
        public bool Status { get; set; }

        public bool DeleteStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        public virtual UserGroup UserGroup { get; set; }
    }
}
