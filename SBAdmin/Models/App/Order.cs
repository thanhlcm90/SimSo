namespace SBAdmin.Models.App
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [Key]
        public int ID { get; set; }

        public int? SIM_ID { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        public int? Province { get; set; }

        [StringLength(50)]
        public string Mobile { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(850)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        [StringLength(20)]
        public string UserBussiness { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string UpdateBy { get; set; }

        public DateTime? LastUpdate { get; set; }
    }
}
