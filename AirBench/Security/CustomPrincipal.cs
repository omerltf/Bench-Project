using AirBench.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace AirBench.Security
{
    public class CustomPrincipal : IPrincipal
    {
        private CustomIdentity identity;
        private User user;

        public CustomPrincipal(CustomIdentity identity, User user)
        {
            this.identity = identity;
            this.user = user;
        }

        public IIdentity Identity
        {
            get
            {
                return identity;
            }
        }

        public User User
        {
            get
            {
                return user;
            }
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}