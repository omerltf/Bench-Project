using AirBench.Data;
using AirBench.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirBench.Repository
{
    public class LoginRepository
    {
        private Context context;

        public LoginRepository(Context context)
        {
            this.context = context;
        }

        public void Insert (User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public User GetByUserName(string userName)
        {
            return  context.Users
                    .Where(u => u.UserName == userName)
                    .SingleOrDefault();
        }

        public User GetById (int id)
        {
            return context.Users
                    .Where(u => u.Id == id)
                    .SingleOrDefault();
        }

    }
}