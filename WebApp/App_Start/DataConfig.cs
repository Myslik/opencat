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
            // Users
            var users = new UserRepository();

            if (!users.Get().Any(u => u.email == "user@gmail.com"))
            {
                users.Create(new User
                {
                    email = "user@gmail.com",
                    name = "Authenticated User"
                }, "correct");
            }

            // Jobs
            var jobs = new JobRepository();

            if (!jobs.Get().Any())
            {
                jobs.Create(new Job
                {
                    name = "Honeybadger",
                    description = "It is primarily a carnivorous species and has few natural predators because of its thick skin and ferocious defensive abilities.",
                    words = 365
                });
            }
        }
    }
}