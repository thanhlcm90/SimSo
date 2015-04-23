using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SBAdmin.Models.App.Repository
{
    public class OrderRepository
    {
        private AppDbContext context = null;
        public OrderRepository()
        {
            context = new AppDbContext();
        }
        public IEnumerable<OrderViewModel> GetAll()
        {
            var orders = from order in context.Orders select order;
            foreach (var order in orders)
            {
                yield return new OrderViewModel
                {
                    ID = order.ID,
                    SIM_ID = order.SIM_ID,
                    Number = context.SIMs.Find(order.SIM_ID).Number,
                    Price = order.Price,
                    FullName = order.FullName,
                    Address = order.Address,
                    Mobile = order.Mobile,
                    Email = order.Email,
                    Description = order.Description,
                    UserBussiness = order.UserBussiness,
                    CreateBy = order.CreateBy,
                    CreateDate = order.CreateDate,
                    UpdateBy = order.UpdateBy,
                    LastUpdate = order.LastUpdate
                };
            }
        }

        public OrderViewModel Get(int id)
        {
            var order = context.Orders.Find(id);
            if (order != null)
            {
                return new OrderViewModel
                       {
                           ID = order.ID,
                           SIM_ID = order.SIM_ID,
                           Number = context.SIMs.Find(order.SIM_ID).Number,
                           Price = order.Price,
                           FullName = order.FullName,
                           Address = order.Address,
                           Mobile = order.Mobile,
                           Email = order.Email,
                           Description = order.Description,
                           UserBussiness = order.UserBussiness,
                           CreateBy = order.CreateBy,
                           CreateDate = order.CreateDate,
                           UpdateBy = order.UpdateBy,
                           LastUpdate = order.LastUpdate
                       };
            }
            return null;
        }
    }
}