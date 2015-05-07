using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimSo.Models.App
{
    [Table("New")]
    public class New
    {
        public int ID { get; set; }
        public int? IDMenu { get; set; }
        [StringLength(250)]
        public string Title { get; set; }
        [StringLength(350)]
        public string ShortDes { get; set; }
        [Column(TypeName = "ntext")]
        [MaxLength]
        public string Content { get; set; }
        [StringLength(200)]
        public string image { get; set; }
        public bool? isActive { get; set; }
        public bool? isDeleted { get; set; }
        [StringLength(30)]
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        [StringLength(30)]
        public string UpdateBy { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
