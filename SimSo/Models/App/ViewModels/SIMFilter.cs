using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimSo.Models.App
{
    public class SIMFilter
    {
        public string searchStr { get; set; }
        public int? nwId { get; set; }
        public int? stId { get; set; }
        public int? spId { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public decimal? price_min { get; set; }
        public decimal? price_max { get; set; }
        public string year { get; set; }
        public string orderBy { get; set; }
        public int? numCount { get; set; }
    }
    public static class SimOrder
    {
        public const string Price = "price";
        public const string Network = "network";
        public const string Simtype = "simtype";

        public const string Price_Des = "price-des";
        public const string Network_Des = "network-des";
        public const string Simtype_Des = "simtype-des";
    }
}