using SimSo.Models.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimSo.Helper;
using System.Data.SqlClient;

namespace SimSo.Models.App.Repository
{
    public class SIMRepository : IDisposable
    {
        private AppDbContext context;

        public SIMRepository()
        {
            context = new AppDbContext();
        }

        // kiem tra sim (dai ly)
        public ListItem CheckSimsByNumber(string number, int pageIndex, int pageSize)
        {
            var data = (from sim in context.SIMs
                        where String.IsNullOrEmpty(number) || sim.Number.Contains(number)
                        orderby sim.ID
                        select sim);
            int total = data.Count();
            int count = (int)Math.Ceiling((double)total / (double)pageSize);
            var sims = data.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return new ListItem { PageCount = count, TotalItems = total, Items = CheckSimsByNumber(sims.ToList()) };
        }
        private IEnumerable<Object> CheckSimsByNumber(IEnumerable<SIM> data)
        {
            foreach (var sim in data)
            {
                var Supplier = context.Suppliers.Find(sim.Supplier_ID);
                float? value = 0f;
                bool isOnly = false;
                var discount = context.Discounts.Where(s => (s.SupID == sim.Supplier_ID));
                if (discount.Count() == 1 && (discount.First().PriceTo == null || discount.First().PriceTo == 0) && (discount.First().PriceFrom == null || discount.First().PriceFrom == 0))
                {
                    value = discount.First().Discounts;
                    isOnly = true;
                }
                else
                {
                    discount = discount.Where(s => (s.PriceFrom <= sim.Price) && (s.PriceTo >= sim.Price));
                    if (discount.Count() == 0) value = 0;
                    else
                    {
                        value = discount.First().Discounts;
                    }
                }
                var nw = context.NetWorks.Find(sim.NetWork_ID);
                yield return new
                {
                    Number = sim.Number,
                    SupplierID = sim.Supplier_ID,
                    SupplierName = Supplier == null ? "" : Supplier.Name,
                    SupplierMobile = Supplier == null ? "" : Supplier.Mobile,
                    SupplierAddress = Supplier == null ? "" : Supplier.Address,
                    NetworkImg = nw == null ? "" : nw.image,
                    Price = sim.Price,
                    Price_Sup=sim.Price_Sup,
                    LastUpdate = sim.LastUpdate ?? sim.CreateDate,
                    Discount = value,
                    isOnly = isOnly
                };
            }
        }

        // quan ly sim (quan ly va nhan vien)
        private IEnumerable<Object> GetPageSim(IEnumerable<SIM> data)
        {
            foreach (var sim in data)
            {
                var st = context.SimTypes.Find(sim.SimType_ID);
                var sp = context.Suppliers.Find(sim.Supplier_ID);
                var nw = context.NetWorks.Find(sim.NetWork_ID);
                yield return new
                {
                    ID = sim.ID,
                    Number = sim.Number,
                    Network = context.NetWorks.Find(sim.NetWork_ID),
                    SimType = st == null ? "" : st.Name,
                    Supplier = sp == null ? "" : sp.Name,
                    Price = sim.Price,
                    Status = sim.Status == 0 ? false : true,
                    isActive = sim.isActive,
                    isDelete = sim.isDeleted
                };
            }
        }
        public ListItem GetPageSim(int pageIndex, int pageSize, int? included)
        {
            var data = (from sim in context.SIMs where included == null || sim.Status == included select sim).OrderBy(s => s.CreateDate);
            int total = data.Count();
            int count = (int)Math.Ceiling((double)total / (double)pageSize);
            var sims = data.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return new ListItem { PageCount = count, TotalItems = total, Items = GetPageSim(sims.ToList()) };
        }

        public ListItem GetSimsFilter(SIMFilter filter)
        {
            string orderBy = "";
            switch (filter.orderBy)
            {
                case SimOrder.Price: orderBy = "0"; break;
                case SimOrder.Price_Des: orderBy = "1"; break;
                case SimOrder.Network: orderBy = "2"; break;
                case SimOrder.Network_Des: orderBy = "3"; break;
                case SimOrder.Simtype: orderBy = "4"; break;
                case SimOrder.Simtype_Des: orderBy = "5"; break;
                default: orderBy = "0"; break;
            }

            int total = context.Database.SqlQuery<int>("GetSimFilterCounter @number,@nwId,@stId,@price_min,@price_max,@orderBy,@numCount",
                new SqlParameter("@number", filter.searchStr),
                new SqlParameter("@nwId", filter.nwId),
                new SqlParameter("@stId", filter.stId),
                new SqlParameter("@price_min", filter.price_min),
                new SqlParameter("@price_max", filter.price_max),
                new SqlParameter("@orderBy", orderBy),
                new SqlParameter("@numCount", filter.numCount)
                ).First();
            int count = (int)Math.Ceiling((double)total / (double)filter.pageSize);
            var sims = context.Database.SqlQuery<SIMViewModel>("GetSimFilter @number,@nwId,@stId,@pageIndex,@pageSize,@price_min,@price_max,@orderBy,@numCount",
                new SqlParameter("@number", filter.searchStr),
                new SqlParameter("@nwId", filter.nwId),
                new SqlParameter("@stId", filter.stId),
                new SqlParameter("@pageIndex", filter.pageIndex),
                new SqlParameter("@pageSize", filter.pageSize),
                new SqlParameter("@price_min", filter.price_min),
                new SqlParameter("@price_max", filter.price_max),
                new SqlParameter("@orderBy", orderBy),
                new SqlParameter("@numCount", filter.numCount)
                ).ToList();
            return new ListItem { PageCount = count, TotalItems = total, Items = sims };
        }

        public Object GetByNumber(string number)
        {
            if (number.Length <= 11)
            {
                var sim = (from s in context.Sim_Clients
                           where s.Number == number
                           orderby s.Price
                           select s);
                if (sim.Count() > 0)
                    return sim.First();
                return DetectSim(number);
            }
            else
            {
                return null;
            }
        }

        public void DuyetPage(int pageIndex, int pageSize, string updateBy, int status, int? included)
        {
            var data = ((from sim in context.SIMs where included == null || sim.Status == included select sim)).OrderBy(s => s.CreateDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            var count = data.Count;
            for (int i = 0; i < count; i++)
            {
                context.Database.ExecuteSqlCommand("Approve_SIM @iID,@iChecked,@mPrice_ncc,@iSupplier_ID,@Number,@NetWork_ID,@SimType_ID",
                new SqlParameter("@iID", data[i].ID),
                new SqlParameter("@iChecked", status),
                new SqlParameter("@mPrice_ncc", data[i].Price_Sup),
                new SqlParameter("@iSupplier_ID", data[i].Supplier_ID),
                new SqlParameter("@Number", data[i].Number),
                new SqlParameter("@NetWork_ID", data[i].NetWork_ID),
                new SqlParameter("@SimType_ID", data[i].SimType_ID)
                );
                context.SaveChanges();
            }
        }

        public void Approve_Sim(SIM sim)
        {
            context.Database.ExecuteSqlCommand("Approve_SIM @iID,@iChecked,@mPrice_ncc,@iSupplier_ID,@Number,@NetWork_ID,@SimType_ID",
                new SqlParameter("@iID",sim.ID),
                new SqlParameter("@iChecked",sim.Status),
                new SqlParameter("@mPrice_ncc",sim.Price_Sup),
                new SqlParameter("@iSupplier_ID",sim.Supplier_ID),
                new SqlParameter("@Number",sim.Number),
                new SqlParameter("@NetWork_ID",sim.NetWork_ID),
                new SqlParameter("@SimType_ID",sim.SimType_ID)
                );
            context.SaveChanges();
        }

        private Object DetectSim(string number)
        {
            return new
            {
                Number = number,
                NetWork_ID = new NetworkRepo().GetBySimNumber(number).ID,
                SimType_ID = new SimTypeRepo().SimTypeGetTypeBySim(number),
                TrangThaiSim = "Sim đã bán hoặc chưa cập nhật lên web",
                Price = 0,
                CamKet = "",
            };
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