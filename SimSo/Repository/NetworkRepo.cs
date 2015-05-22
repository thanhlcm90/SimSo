using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimSo.Models.App.Repository
{
    public class NetworkRepo : IDisposable
    {
        private AppDbContext entities;
        public NetworkRepo()
        {
            entities = new AppDbContext();
        }
        public int? GetIdByNumber(string number)
        {
            using (var entities = new AppDbContext())
            {
                var data = from nw in entities.NetWorks
                           where nw.isActive == true && (nw.Number.Contains(number.Substring(0, 3)) || nw.Number.Contains(number.Substring(0, 4)))
                           select nw;
                if (data.Count() > 0)
                {
                    return data.First().ID;
                }
                return 0;
            }
        }

        public NetWork GetBySimNumber(string number)
        {
            using (var entities = new AppDbContext())
            {
                var data = from nw in entities.NetWorks
                           where nw.isActive == true && (nw.Number.Contains(number.Substring(0, 3)) || nw.Number.Contains(number.Substring(0, 4)))
                           select nw;
                if (data.Count() > 0)
                {
                    return data.First();
                }
                return null;
            }
        }

        public IEnumerable<string> GetTagsById(int? id)
        {
            ICollection<string> result = new List<string>();
            var data = entities.NetWorks.Where(n => n.isDeleted == false && (id == null || n.ID == id));
            foreach (var item in data)
            {
                if (item.Tags != null)
                    foreach (var tag in item.Tags.Split(','))
                    {
                        result.Add(tag.Trim());
                    }
            }
            return result;
        }

        public void Dispose()
        {
            if (entities != null)
            {
                entities.Dispose();
                entities = null;
            }
        }
    }
}