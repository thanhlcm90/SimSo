using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimSo.Models.App
{
    [Table("Config")]
    public class Config
    {
        public int ID { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}