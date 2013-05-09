using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenCat.Data;
using OpenCat.Models;

namespace OpenCat
{
    public static class DataConfig
    {
        public static void Initialize()
        {
            var users = new UserRepository();

            if (!users.Get().Any(u => u.email == "user@gmail.com"))
            {
                users.Create(new User
                {
                    email = "user@gmail.com",
                    name = "Authenticated User"
                }, "correct");
            }
        }
    }
}