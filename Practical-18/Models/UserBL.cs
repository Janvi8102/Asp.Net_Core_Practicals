using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practical_18.Models
{
    public class UserBL
    {
        public List<User> GetUsers()
        {
            
            List<User> userList = new List<User>();
            userList.Add(new User()
            {
                ID = 101,
                UserName = "Admin",
                Password = "MyAdmin",
                Roles = "Admin",
                Email = "Admin@a.com"
            });
            userList.Add(new User()
            {
                ID = 102,
                UserName = "User",
                Password = "MyUser",
                Roles = "Admin,Superadmin",
                Email = "BothUser@a.com"
            });
            userList.Add(new User()
            {
                ID = 103,
                UserName = "SuperadminUser",
                Password = "Password@123",
                Roles = "Superadmin",
                Email = "Superadmin@a.com"
            });
            return userList;
        }
    }
}