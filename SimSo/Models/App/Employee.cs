namespace SimSo.Models.App
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
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [StringLength(50)]
        [Required]
        public string FullName { get; set; }

        [StringLength(5)]
        [Required]
        public string Gender { get; set; }

        public DateTime? BirthDay { get; set; }

        [StringLength(150)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Mobile { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

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

        public bool? WorkingStatus { get; set; }

        public int? STT { get; set; }

        [StringLength(100)]
        public string Yahoo { get; set; }
        [StringLength(100)]
        public string Skype { get; set; }
    }
}
