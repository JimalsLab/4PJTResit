using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoodBurger.DAL;

namespace GoodBurger.DAL
{
    public class UserService
    {
        private Context db;
        public UserService(Context context)
        {
            db = context;
        }

        public List<Users> GetAllUsers()
        {
            return db.users.ToList();
        }

        public Users VerifyLogin(string username, string password)
        {
            var temp = db.users.Where(user => user.Username == username).FirstOrDefault();
            if (temp != null && temp.Password == password)
            {
                return temp;
            }
            else
            {
                return null;
            }
        }
    }
}
