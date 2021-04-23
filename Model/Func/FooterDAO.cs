using Model.EnityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Func
{
    public class FooterDAO
    {
        FoodShopOnlineDBContext db = null;
        public FooterDAO()
        {
            db = new FoodShopOnlineDBContext();
        }
        public Footer GetFooter()
        {
            return db.Footers.SingleOrDefault(x => x.Status == true);
        }
    }
}
