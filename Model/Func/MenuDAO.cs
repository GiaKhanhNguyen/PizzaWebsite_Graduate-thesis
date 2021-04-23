using Model.EnityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Func
{
    public class MenuDAO
    {
        FoodShopOnlineDBContext db = null;
        public MenuDAO()
        {
            db = new FoodShopOnlineDBContext();
        }
        public List<Menu> ListByGroupID(int groupId)
        {
            return db.Menus.Where(x => x.GroupID == groupId && x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }

        public List<MenuGroup> ListMenuGroup()
        {
            return db.MenuGroups.OrderBy(x => x.ID).ToList();
        }

        public List<Menu> ListMenu()
        {
            return db.Menus.OrderBy(x => x.ID).ToList();
        }
    }
}
