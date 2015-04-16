namespace SBAdmin.Models.App
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SIM")]
    public partial class SIM
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Number { get; set; }

        public int? NetWork_ID { get; set; }

        public int? SimType_ID { get; set; }

        public int? Supplier_ID { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public bool? Status { get; set; }

        public bool? isActive { get; set; }

        public bool? isDeleted { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string UpdateBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastUpdate { get; set; }
    }
}
