using Model.EnityFramework;
using Model.ViewModel;
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

        //public List<Menu> ListMenu()
        //{
        //    return db.Menus.OrderBy(x => x.ID).ToList();
        //}
        public List<MenuViewModel> ListMenu()
        {
            var data = (from a in db.Menus
                        join b in db.MenuGroups on a.GroupID equals b.ID
                        select new MenuViewModel()
                        {
                            Menu = a,
                            MenuGroup = b
                        });
            return data.OrderBy(x=>x.Menu.ID).ToList();
        }

        public bool EditMenu(Menu entity)
        {
            try
            {
                var menu = db.Menus.Find(entity.ID);
                menu.Name = entity.Name;
                menu.URL = entity.URL;
                menu.Icon = entity.Icon;
                menu.DisplayOrder = entity.DisplayOrder;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Menu ViewDetailMenu(int id)
        {
            return db.Menus.Find(id);
        }
        public void DeleteMenu(int id)
        {
            var menu = db.Menus.Find(id);
            menu.Status = !menu.Status;
            db.SaveChanges();
        }

    }
}
