namespace Model.EnityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderNoLogin")]
    public partial class OrderNoLogin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderNoLogin()
        {
            OrderDetailsNoLogins = new HashSet<OrderDetailsNoLogin>();
        }

        public int ID { get; set; }

        [StringLength(250)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(250)]
        public string CustomerAddress { get; set; }

        [Required]
        [StringLength(250)]
        public string CustomerEmail { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerMobile { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public decimal Total { get; set; }

        [StringLength(250)]
        public string PaymentMethod { get; set; }

        [StringLength(50)]
        public string PaymentStatus { get; set; }

        [StringLength(100)]
        public string OrderMethod { get; set; }

        public bool Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetailsNoLogin> OrderDetailsNoLogins { get; set; }
    }
}
