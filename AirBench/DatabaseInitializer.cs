using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AirBench.Models;
using AirBench.Data;

namespace AirBench
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<Context>
    {
        protected override void Seed(Context context)
        {
            context.Users.Add(new User()
            {
                Name = "Omer Latif",
                UserName = "omerltf@gmail.com",
                HashedPassword = "$2a$10$5VRNPvBIY9iz/u26rHGFEen3KcWs2rMu1zI9wjqzXNAHpF6UGmQc."
            });
        }
    }
}