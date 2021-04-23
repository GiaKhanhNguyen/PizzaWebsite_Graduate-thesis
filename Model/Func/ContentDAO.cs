using Model.EnityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Func
{
    public class ContentDAO
    {
        FoodShopOnlineDBContext db = null;
        public ContentDAO()
        {
            db = new FoodShopOnlineDBContext();
        }
        public Content GetByIDContent(int id)
        {
            return db.Contents.Find(id);
        }
    }
}
