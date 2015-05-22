using SimSo.Models.App.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimSo.Models.App.Repository
{
    public class NewRepository : IDisposable
    {
        private AppDbContext context;
        public NewRepository()
        {
            context = new AppDbContext();
        }
        public ListItem GetNewsByMenu(int? menuID, int pageIndex, int pageSize)
        {
            var data = context.News.Where(n => n.isDeleted == false && (menuID == 0 || n.IDMenu == menuID));
            int total = data.Count();
            int pageCount = (int)Math.Ceiling((double)total / (double)pageSize);
            var news = data.OrderBy(n => n.CreateDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return new ListItem { Items = ToViewModel(news.ToList()), TotalItems = total, PageCount = pageCount };
        }

        IEnumerable<Object> ToViewModel(IEnumerable<New> news)
        {
            foreach (var item in news)
            {
                yield return new NewViewModel
                {
                    ID = item.ID,
                    Menu = context.Menus.Find(item.IDMenu),
                    Title = item.Title,
                    ShortDes = item.ShortDes,
                    Content = item.Content,
                    image = item.image,
                    isActive = item.isActive,
                    isDeleted = item.isDeleted,
                    CreateBy = item.CreateBy,
                    CreateDate = item.CreateDate,
                    UpdateBy = item.UpdateBy,
                    LastUpdate = item.LastUpdate
                };
            }
        }

        public IEnumerable<NewViewModel> GetTopNews()
        {
            var data = from n in context.News
                       join m in context.Menus
                       on n.IDMenu equals m.ID
                       where m.isShow == 1
                       orderby n.CreateDate descending
                       select n;
            foreach (var item in data.Take(5).ToList())
            {
                yield return new NewViewModel
                {
                    ID = item.ID,
                    Menu = context.Menus.Find(item.IDMenu),
                    Title = item.Title,
                    CreateDate = item.CreateDate,
                };
            }
        }

        public IEnumerable<NewViewModel> GetRelativeNews(int menuId, int? id)
        {
            var data = from n in context.News
                       join m in context.Menus
                       on n.IDMenu equals m.ID
                       where m.ID == menuId && (id != null && n.ID != id)
                       orderby n.CreateDate descending
                       select n;
            foreach (var item in data.Take(10).ToList())
            {
                yield return new NewViewModel
                {
                    ID = item.ID,
                    Menu = context.Menus.Find(item.IDMenu),
                    Title = item.Title,
                    CreateDate = item.CreateDate,
                };
            }
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