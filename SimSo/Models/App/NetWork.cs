namespace SimSo.Models.App
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NetWork")]
    public partial class NetWork
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Number { get; set; }

        [StringLength(200)]
        public string image { get; set; }

        public bool? isActive { get; set; }

        public bool? isDeleted { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string UpdateBy { get; set; }

        public DateTime? LastUpdate { get; set; }

        [StringLength(200)]
        public string Title { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        [StringLength(1000)]
        public string Tags { get; set; }
        [StringLength(1000)]
        public string Keyword { get; set; }
    }
}
