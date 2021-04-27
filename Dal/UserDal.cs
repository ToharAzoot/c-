using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;
namespace Dal
{
    public class UserDal
    {
        public static bool Register(Users u)
        {
            using (kindergardenEntities db = new kindergardenEntities())
            {
                db.Users.Add(u);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public static bool UpdateUser(Users us,string v)
        {
            using (kindergardenEntities db = new kindergardenEntities())
            {
                int? i=0;
                string c = db.empGetUserIdSp(v).FirstOrDefault();
                i = db.empGetPasswordSp(c).FirstOrDefault();
                if (i != 0)
                {
                    Users d=null;
                    d = db.Users.Find(i);
                    if (d!=null)
                    {
                        db.Users.Find(i).Password=us.Password;
                        db.Users.Find(i).UserName=us.UserName;
                        Connections so;
                        so=db.Connections.Find(c,v);
                        db.Connections.Remove(so);
                        Connections f=new Connections();
                        f.ChildId = v;
                        f.UserId = us.Password;
                        db.Connections.Add(f);
                        db.SaveChanges();
                    }
                }
                return true;
            }
            return true;
        }
        public static Users Getallpropertyd(string id) 
        {
            using (kindergardenEntities db = new kindergardenEntities())
            {
                string c = db.empGetUserIdSp(id).FirstOrDefault();
                int? i=null;
                i= db.empGetPasswordSp(c).FirstOrDefault();
                if (i!=null)
                {
                    Users d = db.Users.Find(i);
                    return d;
                  }
                return null;
            }
        }
        public static int AddUser(Users ch)
        {
            using (kindergardenEntities db = new kindergardenEntities())
            {
                ch.UserId= (db.Users.Max(i => i.UserId))+1;
                db.Users.Add(ch);
                db.SaveChanges();
                return db.Users.Max(i => i.UserId);
            }
            return 0;
        }
    }
}
