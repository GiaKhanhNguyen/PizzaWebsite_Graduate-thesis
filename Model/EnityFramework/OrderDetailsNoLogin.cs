namespace Model.EnityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetailsNoLogin")]
    public partial class OrderDetailsNoLogin
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductID { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
        public string Dough { get; set; }
        public string Size { get; set; }

        public virtual OrderNoLogin OrderNoLogin { get; set; }

        public virtual Product Product { get; set; }
    }
}
