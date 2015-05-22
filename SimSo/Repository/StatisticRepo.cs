using SimSo.Models.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimSo.Repository
{
    public class StatisticRepo
    {
        private AppDbContext context;
        private static int CurrentCount = 0;

        public StatisticRepo()
        {
            context = new AppDbContext();
        }

        public void InsertOrUpdate()
        {
            var data = context.Statistics.Where(s => s.CreateDate == DateTime.Today);
            if (data.Count() == 0)
            {
                Statistic entity = new Statistic(1, DateTime.Today);
                context.Statistics.Add(entity);
                context.SaveChanges();
            }
            else
            {
                var statis = data.First();
                statis.Count++;
                context.Entry(statis).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public int GetCurrentCount()
        {
            return CurrentCount;
        }

        public void SetCurrentCount(int count)
        {
            CurrentCount += count;
        }

        public int GetTotalCount()
        {
            return context.Statistics.Sum(s => s.Count);
        }

        public int GetTodayCount()
        {
            var data = context.Statistics.Where(x => x.CreateDate == DateTime.Today);
            if (data.Count() > 0)
                return data.First().Count;
            return 0;
        }

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
                context = null;
            }
        }
    }
}