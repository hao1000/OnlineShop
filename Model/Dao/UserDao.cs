using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

using System.Configuration;
using System.Data.Common;
using PagedList;

namespace Model.Dao
{
    public class UserDao
    {
        OnlineShopDBContext db = null;
        public UserDao()
        {
            db = new OnlineShopDBContext();
        }
        public IEnumerable<User> ListAllPaging(int page,int pageSize)
        {
            return db.User.OrderByDescending(x=>x.UserName).ToPagedList(page,pageSize);
        }
        public long Insert(User entity)
        {
            db.User.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public long InsertForFacebook(User entity)
        {
            var user = db.User.SingleOrDefault(x => x.UserName == entity.UserName);
            if (user == null)
            {
                db.User.Add(entity);
                db.SaveChanges();
                return entity.ID;
            }
            else
            {
                return user.ID;
            }

        }
        public bool Update(User entity)
        {
            try
            {
                var user = db.User.Find(entity.ID);
                user.Name = entity.Name;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.Address = entity.Address;
                user.Email = entity.Email;
                user.ModifiedBy = entity.ModifiedBy;
                user.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }

        public User GetById(string userName)
        {
            return db.User.SingleOrDefault(x => x.UserName == userName);
        }
        public User ViewDetail(int id)
        {
            return db.User.Find(id);
        }
        public List<string> GetListCredential(string userName)
        {
            var user = db.User.Single(x => x.UserName == userName);
            var data = (from a in db.Credential
                        join b in db.UserGroup on a.UserGroupID equals b.ID
                        join c in db.Role on a.RoleID equals c.ID
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
        public int Login(string userName, string passWord, bool isLoginAdmin = false)
        {
            // var result = db.User.Count(x => x.UserName == userName && x.Password == passWord);
            var result = db.User.SingleOrDefault(x => x.UserName == userName);
            if (result ==null)
            {
                return 0;
            }
            else
            {
                if(result.Status==false)
                return -1;
                else
                {
                    if (result.Password == passWord)
                        return 1;
                    else
                        return -2;
                }
            }
        }

        public bool ChangeStatus(long id)
        {
            var user = db.User.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }
        public bool Delete(int id)
        {
            try
            {
                var user = db.User.Find(id);
                db.User.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool CheckUserName(string userName)
        {
            return db.User.Count(x => x.UserName == userName) > 0;
        }
        public bool CheckEmail(string email)
        {
            return db.User.Count(x => x.Email == email) > 0;
        }
    }
}
