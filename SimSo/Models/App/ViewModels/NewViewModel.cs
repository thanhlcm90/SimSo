using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimSo.Models.App.ViewModels
{
    public class NewViewModel
    {
        public int ID { get; set; }
        public Menu Menu { get; set; }
        public string Title { get; set; }
        public string ShortDes { get; set; }
        public string Content { get; set; }
        public string image { get; set; }
        public bool? isActive { get; set; }
        public bool? isDeleted { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}