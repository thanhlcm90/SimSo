using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SBAdmin.Models.App.ViewModels
{
    public class ListSimViewModel
    {
        public IEnumerable<Object> ListSim { get; set; }
        public int PageCount { get; set; }
        public int TotalSims { get; set; }
    }
}