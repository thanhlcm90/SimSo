using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimSo.Models.App
{
    public class Statistic
    {
        public int Id { get; set; }

        public int Count { get; set; }

        public DateTime CreateDate { get; set; }

        public Statistic(int count, DateTime date)
        {
            this.Count = count;
            this.CreateDate = date;
        }

        public Statistic()
        {

        }
    }
}