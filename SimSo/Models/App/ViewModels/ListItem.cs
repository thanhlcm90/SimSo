using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimSo.Models.App.ViewModels
{
    public class ListItem
    {
        public IEnumerable<Object> Items { get; set; }
        public int PageCount { get; set; }
        public int TotalItems { get; set; }
    }
}