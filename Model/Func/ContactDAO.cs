using Model.EnityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Model.Func
{
    public class ContactDAO
    {
        FoodShopOnlineDBContext db = null;
        public ContactDAO()
        {
            db = new FoodShopOnlineDBContext();
        }
        public Contact GetActiveContact()
        {
            return db.Contacts.Single(x => x.Status == true);
        }

        public int InsertFeeBack(FeedBack feedBack)
        {
            db.FeedBacks.Add(feedBack);
            db.SaveChanges();
            return feedBack.ID;
        }
        public List<FeedBack> GetFeedBack()
        {
            return db.FeedBacks.Where(x => x.Status == true).ToList();
        }

        public void Delete(int id)
        {
            var feedBack = db.FeedBacks.Find(id);
            db.FeedBacks.Remove(feedBack);
            db.SaveChanges();
        }
    }
}
