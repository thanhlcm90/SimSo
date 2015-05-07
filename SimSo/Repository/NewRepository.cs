using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimSo.Models.App.Repository
{
    public class NewRepository
    {
        private AppDbContext context;
        public NewRepository()
        {
            context = new AppDbContext();
        }
        public Object GetNewsByMenu(int? menuID, int pageIndex, int pageSize)
        {
            var data = context.News.Where(n => n.isDeleted == false && (menuID == 0 || n.IDMenu == menuID));
            int total = data.Count();
            int pageCount = (int)Math.Ceiling((double)total / (double)pageSize);
            var news = data.OrderBy(n => n.CreateDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return new { ListNew = ToViewModel(news), TotalNews = total, PageCount = pageCount };
        }

        IEnumerable<Object> ToViewModel(IEnumerable<New> news)
        {
            foreach (var item in news)
            {
                yield return new
                {
                    ID = item.ID,
                    Menu = context.Menus.Find(item.IDMenu),
                    Title = item.Title,
                    ShortDes = item.ShortDes,
                    Content = item.Content,
                    image = item.image,
                    isActive = item.isActive,
                    CreateBy = item.CreateBy,
                    CreateDate = item.CreateDate,
                    UpdateBy = item.UpdateBy,
                    LastUpdate = item.LastUpdate
                };
            }
        }
    }
}