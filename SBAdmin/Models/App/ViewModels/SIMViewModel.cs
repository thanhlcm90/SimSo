using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SBAdmin.Models.App
{
    public class SIMViewModel
    {
        public int ID { get; set; }
        public string Number { get; set; }
        public NetWork Network { get; set; }
        public string SimType { get; set; }
        public string Supplier { get; set; }
        public decimal? Price { get; set; }
        public string Status { get; set; }
        public bool? isActive { get; set; }
        public bool? isDelete { get; set; }
    }
}