﻿namespace OpenCat.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using OpenCat.Models;

    public class UserRepository : Repository<User>
    {
        public override User Create(User entity)
        {
            entity.GeneratePassword();
            entity.ComputeGravatar();
            return base.Create(entity);
        }

        public User Create(User entity, String password)
        {
            entity.StorePassword(password);
            entity.ComputeGravatar();
            return base.Create(entity);
        }

        public bool Verify(string email, string password)
        {
            var user = Get().Where(u => u.email == email).SingleOrDefault();

            if (user == null) return false;

            return user.VerifyPassword(password);
        }
    }
}