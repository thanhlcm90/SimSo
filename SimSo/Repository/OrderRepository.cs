using SimSo.Models.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimSo.Models.App.Repository
{
    public class OrderRepository : IDisposable
    {
        private AppDbContext context = null;
        public OrderRepository()
        {
            context = new AppDbContext();
        }
        IEnumerable<OrderViewModel> ToViewModels(IEnumerable<Order> orders)
        {
            foreach (var order in orders)
            {
               // var sim = context.Sim_Clients.Find(order.Number);
                var nw = context.NetWorks.Find(order.Network_ID);
                yield return new OrderViewModel
                {
                    ID = order.ID,
                    Number = order.Number,
                    Price = order.Price ?? 0,
                    FullName = order.FullName,
                    Address = order.Address,
                    Mobile = order.Mobile,
                    Email = order.Email,
                    OrderCount = context.Orders.Where(o => o.Mobile == order.Mobile).Count(),
                    Description = order.Description,
                    UserBusiness = order.UserBusiness,
                    Note = order.Note,
                    CreateBy = order.CreateBy,
                    CreateDate = order.CreateDate,
                    UpdateBy = order.UpdateBy,
                    LastUpdate = order.LastUpdate,
                    Network = nw == null ? "" : nw.Name,
                    Supplier = context.Suppliers.Find(order.Sup_ID),
                    Status = order.Status ?? 0
                };
            }
        }

        public ListItem GetAll(int pageIndex, int pageSize, int? status, string number, string userBusiness)
        {
            var data = context.Orders.Where(s => (status == null || s.Status == status) && (userBusiness == "admin" || s.UserBusiness == userBusiness) && (string.IsNullOrEmpty(number) || s.Number.Contains(number)));
            int total = data.Count();
            int pageCount = (int)Math.Ceiling((double)total / (double)pageSize);
            var orders = data.OrderBy(n => n.CreateDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return new ListItem { Items = ToViewModels(orders.ToList()), TotalItems = total, PageCount = pageCount };
        }


        public OrderViewModel Get(int id)
        {
            var order = context.Orders.Find(id);
            if (order != null)
            {
                return new OrderViewModel
                       {
                           ID = order.ID,
                           Number = order.Number,
                           Price = order.Price ?? 0,
                           FullName = order.FullName,
                           Address = order.Address,
                           Mobile = order.Mobile,
                           Email = order.Email,
                           Description = order.Description,
                           UserBusiness = order.UserBusiness,
                           CreateBy = order.CreateBy,
                           CreateDate = order.CreateDate,
                           UpdateBy = order.UpdateBy,
                           LastUpdate = order.LastUpdate
                       };
            }
            return null;
        }

        public IEnumerable<int> GetCountByStatus(string userBusiness)
        {
            for (int i = 1; i <= 6; i++)
            {
                yield return context.Orders.Where(x => x.Status == i && (userBusiness == "admin" || x.UserBusiness == userBusiness)).Count();
            }
        }

        public IEnumerable<OrderViewModel> GetCustomerOrders(string mobile)
        {
            var data = context.Orders.Where(o => o.Mobile == mobile).OrderByDescending(o => o.CreateDate);
            return ToViewModels(data.ToList());
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