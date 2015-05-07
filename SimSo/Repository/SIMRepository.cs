using SimSo.Models.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimSo.Models.App.Repository
{
    public class SIMRepository
    {
        private AppDbContext context;

        public SIMRepository()
        {
            context = new AppDbContext();
        }

        // kiem tra sim (dai ly)
        public ListSimViewModel CheckSimsByNumber(string number, int pageIndex, int pageSize)
        {
            var data = (from sim in context.SIMs
                        where String.IsNullOrEmpty(number) || sim.Number.Contains(number)
                        orderby sim.Number
                        select sim);
            int total = data.Count();
            int count = (int)Math.Ceiling((double)total / (double)pageSize);
            var sims = data.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return new ListSimViewModel { PageCount = count, TotalSims = total, ListSim = CheckSimsByNumber(sims) };
        }
        private IEnumerable<Object> CheckSimsByNumber(IQueryable<SIM> data)
        {
            foreach (var sim in data)
            {
                yield return new
                {
                    Number = sim.Number,
                    SupplierName = context.Suppliers.Find(sim.Supplier_ID).Name,
                    SupplierMobile = context.Suppliers.Find(sim.Supplier_ID).Mobile,
                    Price = sim.Price,
                };
            }
        }

        // quan ly sim (quan ly va nhan vien)
        private IEnumerable<SIMViewModel> GetPageSim(IQueryable<SIM> data)
        {
            foreach (var sim in data)
            {
                yield return new SIMViewModel
                {
                    ID = sim.ID,
                    Number = sim.Number,
                    Network = context.NetWorks.Find(sim.NetWork_ID),
                    SimType = context.SimTypes.Find(sim.SimType_ID).Name,
                    Supplier = context.Suppliers.Find(sim.Supplier_ID).Name,
                    Price = sim.Price,
                    Status = sim.Status == 1 ? "Đã duyệt" : "Chưa duyệt",
                    isActive = sim.isActive,
                    isDelete = sim.isDeleted
                };
            }
        }
        public ListSimViewModel GetPageSim(int pageIndex, int pageSize)
        {
            var data = (from sim in context.SIMs select sim).OrderBy(s => s.Number);
            int total = data.Count();
            int count = (int)Math.Ceiling((double)total / (double)pageSize);
            var sims = data.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return new ListSimViewModel { PageCount = count, TotalSims = total, ListSim = GetPageSim(sims) };
        }

        private IEnumerable<Object> GetSimsFilter(IQueryable<SIM> data)
        {
            foreach (var sim in data)
            {
                yield return new SIMViewModel
                {
                    ID = sim.ID,
                    Number = sim.Number,
                    Network = context.NetWorks.Find(sim.NetWork_ID),
                    SimType = context.SimTypes.Find(sim.SimType_ID).Name,
                    Price = sim.Price,
                };
            }
        }
        public ListSimViewModel GetSimsFilter(SIMFilter filter)
        {
            var data = from sim in context.SIMs
                       where (filter.nwId == null || sim.NetWork_ID == filter.nwId)
                       && (filter.stId == null || sim.SimType_ID == filter.stId)
                       && (filter.price_min == null || sim.Price >= filter.price_min)
                       && (filter.price_max == null || sim.Price <= filter.price_max)
                       && (String.IsNullOrEmpty(filter.year) || sim.Number.Substring(sim.Number.Length - 4).Equals(filter.year))
                       select sim;
            if (!String.IsNullOrEmpty(filter.searchStr))
            {
                if (filter.searchStr.StartsWith("*"))
                {
                    data = data.Where(s => s.Number.EndsWith(filter.searchStr.Substring(1)));
                }
                else if (filter.searchStr.EndsWith("*"))
                {
                    int len = filter.searchStr.IndexOf('*');
                    data = data.Where(s => s.Number.StartsWith(filter.searchStr.Substring(0, len)));
                }
                else if (filter.searchStr.Contains("*"))
                {
                    int len = filter.searchStr.IndexOf('*');
                    data = data.Where(s => s.Number.StartsWith(filter.searchStr.Substring(0, len)) && s.Number.EndsWith(filter.searchStr.Substring(len + 1)));
                }
                else
                {
                    data = data.Where(s => s.Number.Contains(filter.searchStr));
                }
            }

            switch (filter.orderBy)
            {
                case SimOrder.Network: data=data.OrderBy(s => s.NetWork_ID); break;
                case SimOrder.Price: data=data.OrderBy(s => s.Price); break;
                case SimOrder.Simtype: data=data.OrderBy(s => s.SimType_ID); break;
                case SimOrder.Network_Des: data=data.OrderByDescending(s => s.NetWork_ID); break;
                case SimOrder.Price_Des: data=data.OrderByDescending(s => s.NetWork_ID); break;
                case SimOrder.Simtype_Des: data=data.OrderByDescending(s => s.NetWork_ID); break;
                default: data=data.OrderBy(s => s.Price); break;
            }

            int total = data.Count();
            int count = (int)Math.Ceiling((double)total / (double)filter.pageSize);
            var sims = data.Skip((filter.pageIndex - 1) * filter.pageSize).Take(filter.pageSize);
            return new ListSimViewModel { PageCount = count, TotalSims = total, ListSim = GetSimsFilter(sims) };
        }


        public SIM GetByNumber(string number)
        {
            var sim = from s in context.SIMs
                      where s.Number == number &&
                      s.isActive == true &&
                      s.Status == 1
                      orderby s.Price
                      select s;
            if (sim.Count() > 0)
                return sim.First();
            return null;
        }
    }
}