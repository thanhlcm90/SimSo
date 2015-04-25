using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SBAdmin.Models.App
{
    [Table("Menu")]
    public partial class Menu
    {
        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        public int? Stand { get; set; }
        public bool? isActive { get; set; }

        public int Type { get; set; }

        public bool? isDeleted { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string UpdateBy { get; set; }

        public DateTime? LastUpdate { get; set; }
    }
}