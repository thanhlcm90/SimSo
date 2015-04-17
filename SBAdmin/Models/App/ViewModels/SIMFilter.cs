using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SBAdmin.Models.App
{
    public class SIMFilter
    {
        public string number { get; set; }
        public int nwId { get; set; }
        public int stId { get; set; }
        public int spId { get; set; }
        public int pageCount { get; set; }
        public int itemsPerPage { get; set; }
        public decimal price { get; set; }
        public bool status { get; set; }
        public bool activeIncluded { get; set; }
        public bool deletedIncluded { get; set; }

    }
}