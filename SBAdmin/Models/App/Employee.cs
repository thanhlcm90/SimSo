namespace SBAdmin.Models.App
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        public int ID { get; set; }

        [StringLength(256)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string FullName { get; set; }

        [StringLength(5)]
        public string Gender { get; set; }

        public DateTime? BirthDay { get; set; }

        [StringLength(150)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Mobile { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(200)]
        public string images { get; set; }

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
