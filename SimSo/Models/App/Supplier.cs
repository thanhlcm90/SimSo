namespace SimSo.Models.App
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Supplier")]
    public partial class Supplier
    {
        public int ID { get; set; }

        [StringLength(50)]
        [Required]
        public string UserName { get; set; }

        [StringLength(100, MinimumLength = 6)]
        [Required]
        public string Password { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(20)]
        public string Mobile { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        public bool? isActive { get; set; }

        public bool? isDeleted { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string UpdateBy { get; set; }

        public DateTime? LastUpdate { get; set; }
    }
}
