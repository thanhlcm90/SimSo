using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimSo.Models.App
{
    [Table("Discount")]
    public class Discount
    {
        [Key]
        public int ID { get; set; }
        public int SupID { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public float? Discounts { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(50)]
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        [StringLength(50)]
        public string UpdateBy { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}