using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using MongoDB.Driver;
using OpenCat.Models;
using OpenCat.Services;

namespace OpenCat
{
    public static class DataConfig
    {
        public static void Initialize()
        {
            var dbName = ConfigurationManager.AppSettings["dbName"];
            var database = new MongoClient().GetServer().GetDatabase(dbName);

            // Users
            var users = new UserService(database);

            if (!users.Read().Any(u => u.email == "user@gmail.com"))
            {
                users.Create(new User
                {
                    email = "user@gmail.com",
                    name = "Authenticated User"
                }, "correct");
            }

            // Jobs
            var jobs = new JobService(database);

            if (!jobs.Read().Any())
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