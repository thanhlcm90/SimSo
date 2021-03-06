﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimSo.Models.App
{
    public class OrderViewModel
    {
        public int ID { get; set; }

        public int? SIM_ID { get; set; }

        public string Number { get; set; }

        public Supplier Supplier { get; set; }

        public string Network { get; set; }

        public string FullName { get; set; }

        public string Address { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public int OrderCount { get; set; }

        public string Description { get; set; }

        public string Note { get; set; }

        public decimal Price { get; set; }

        public int Status { get; set; }

        public string UserBusiness { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateDate { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? LastUpdate { get; set; }
    }
}