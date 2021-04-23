using Model.EnityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Func
{
    public class SlideDAO
    {
        FoodShopOnlineDBContext db = null;
        public SlideDAO()
        {
            db = new FoodShopOnlineDBContext();
        }
        public List<Slide> ListAllSlide()
        {
            return db.Slides.OrderBy(x => x.DisplayOrder).ToList();
        }

        public bool EditSlide(Slide entity)
        {

            try
            {
                var slide = db.Slides.Find(entity.ID);
                slide.Image = entity.Image;
                slide.DisplayOrder = entity.DisplayOrder;
                slide.Link = entity.Link;
                slide.Description = entity.Description;
                slide.ModifiedDay = DateTime.Now;
                slide.ModifiedBy = entity.ModifiedBy;
                slide.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool Create(Slide model)
        {
            try
            {
                model.CreatedDay = DateTime.Now;
                db.Slides.Add(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        //hàm xóa
        public void DeleteSlide(int id)
        {
            var slide = db.Slides.Find(id);
            db.Slides.Remove(slide);
            db.SaveChanges();
        }

        public Slide ViewDetailSlide(int id)
        {
            return db.Slides.Find(id);
        }
    }
}
