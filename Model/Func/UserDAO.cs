using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Model.EnityFramework;
using PagedList;
using System.Data.SqlClient;
namespace Model.Func
{
    //data access object
    public class UserDAO
    {
        FoodShopOnlineDBContext db = null;
        public UserDAO()
        {
            db = new FoodShopOnlineDBContext();
        }
        //CÁC THAO TÁC HÀM THÊM, XÓA, SỬA TRANG USER SỬ DỤNG STORED PROCEDUCE
        public int insert(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return user.ID;

        }
        public int insertForFacebook(User entity)
        {
            var user = db.Users.SingleOrDefault(x => x.UserName == entity.UserName);
            if(user == null)
            {
                db.Users.Add(entity);
                db.SaveChanges();
                return entity.ID;
            }
            else
            {
                return user.ID;
            }
        }
        //phương thức tìm kiếm lấy ra các bản ghi, sử dụng để hiện tất cả các danh sách người dùng
        //chức năng dành riêng cho admin
        public IEnumerable<User> ListAllPaging(string searchString, int page, int pagesize)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString) || x.GroupID.Contains(searchString));

            }
            return model.OrderBy(x => x.ID).ToPagedList(page, pagesize);
        }
        //hàm Update bản ghi User trong database, sử dụng Store Procedure
        public bool UpdateUser(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.Name =  entity.Name;
                if(!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.GroupID =  entity.GroupID;
                user.Address =  entity.Address;
                user.Email =  entity.Email;
                user.ModifiedBy = "ADMIN";
                user.ModifiedDate = DateTime.Now;
                user.Phone = entity.Phone;
                user.Gender = entity.Gender;
                user.Status =  entity.Status;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //Hàm xóa thông tin User của id được truyền vào, sử dụng Store Procedure
        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                user.DeleteStatus = !user.DeleteStatus;
                db.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        //hàm chi tiết bản ghi user được update, truyền vào một ID user
        public User ViewDetailUser(int id)
        {
            return db.Users.Find(id);
        }
        //hàm trả về bản ghi user để thực hiện tính năng đăng nhập, Xét UserName được nhập vào thỏa mãn điều kiện nào của tính năng đăng nhập, sử dụng Stored Procedure
        public User GetUserByUserName(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }
        //
        public int Login(string userName, string passWord, bool IsLoginAdmin = false)
        {
            // biểu thức lamda expression, x là đại diện cho mỗi phần tử của user lặp qua
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0; //tài khoản không tồn tại
            }
            else
            {
                if(IsLoginAdmin == true)
                {
                    if(result.DeleteStatus == true)
                    {
                        if ((result.GroupID == CommonConstant.ADMIN_GROUP) || (result.GroupID == CommonConstant.MOD_GROUP))
                        {
                            if (result.Status == false)
                            {
                                return -1; //tài khoản đang bị khóa
                            }
                            else
                            {
                                if (result.Password == passWord)
                                {
                                    return 1; //đăng nhập thành công
                                }
                                else
                                {
                                    return -2; //nhập sai password
                                }
                            }
                        }
                        else
                        {
                            return -3;  //tài khoản không có quyền đăng nhập
                        }
                    }
                    else
                    {
                        return 0; // tài khoản không tồn tại
                    }
                }
                else
                {
                    if(result.DeleteStatus == true)
                    {
                        if (result.Status == false)
                        {
                            return -1; //tài khoản đang bị khóa
                        }
                        else if ((result.GroupID == CommonConstant.ADMIN_GROUP))
                        {
                            if (result.Password == passWord)
                            {
                                return 1; //đăng nhập thành công
                            }
                            else
                            {
                                return -2; //nhập sai password
                            }
                        }
                        else
                        {
                            if (result.Password == passWord)
                            {
                                return 1; //đăng nhập thành công
                            }
                            else
                            {
                                return -2; //nhập sai password
                            }
                        }
                    }
                    else
                    {
                        return 0; //tài khoản không tồn tại
                    }
                }
            }
        }
        public List<string> GetListCredential(string userName)
        {
            var user = db.Users.Single(x => x.UserName == userName);
            var data = (from a in db.Credentials
                        join b in db.UserGroups on a.UserGroupID equals b.ID
                        join c in db.Roles on a.RoleID equals c.ID
                        where b.ID == user.GroupID
                        select new
                        {
                            RoleID = a.RoleID,
                            UserGroupID = a.UserGroupID
                        }).AsEnumerable().Select(x => new Credential()
                        {
                            RoleID = x.RoleID,
                            UserGroupID = x.UserGroupID
                        });
            return data.Select(x => x.RoleID).ToList();
        }
        ///////////////////////////////////////////////////////
        public bool ChangeStatus(int id)
        {
            var user = db.Users.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }

        //////////////////////////////////////////
        
        ///////////////////////////////////////////
        //Hàm check UserName đã tồn tại hay chưa khi đăng kí tài khoản mới
        public bool CheckUserName(string userName)
        {
            return db.Users.Count(x => x.UserName == userName) > 0;
        }
        //Hàm check Email đã tồn tại hay chưa khi đăng kí tài khoản mới
        public bool CheckEmail(string email)
        {
            return db.Users.Count(x => x.Email == email) > 0;
        }
        ////hiển thị dữ liệu nhóm người dùng sử dụng Store Procedure
        //public List<UserGroup> ListUserGroup()
        //{
        //    var list = db.Database.SqlQuery<UserGroup>("SP_UserGroup").ToList();
        //    return list;
        //}

        public void NewPassword(User enity)
        {
            var user = db.Users.Find(enity.ID);
            user.Password = enity.Password;
            db.SaveChanges();
        }

        public bool UpdateAccount(User entity, int id)
        {
            try
            {
                var user = db.Users.Find(id);
                user.Name = entity.Name;
                user.Phone = entity.Phone;
                if(entity.Avatar != null)
                {
                    user.Avatar = entity.Avatar;
                }
                user.Gender = entity.Gender;
                user.Address = entity.Address;
                user.ModifiedBy = "USER";
                user.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ChangePassword(string passWord, int id)
        {
            var user = db.Users.Find(id);
            try
            {
                user.Password = passWord;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<User> ListUserChat()
        {
            return db.Users.OrderBy(x => x.ID).ToList();
        }
    }
}
