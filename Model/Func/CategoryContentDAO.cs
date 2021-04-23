using Model.EnityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Func
{
    public class CategoryContentDAO
    {
        FoodShopOnlineDBContext db = null;
        public CategoryContentDAO()
        {
            db = new FoodShopOnlineDBContext();
        }
        public List<CategoryContent> ListAll()
        {
            return db.CategoryContents.Where(x => x.Status == true).ToList();
        }
    }
    
}
