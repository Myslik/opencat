namespace OpenCat.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;

    public static class UserExtensions
    {
        const int RND_PASS_LENGTH = 20;

        public static void GeneratePassword(this User user)
        {
            var cryptoRandomDataGenerator = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[RND_PASS_LENGTH];
            cryptoRandomDataGenerator.GetBytes(buffer);
            string password = Convert.ToBase64String(buffer);

            user.password = BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static void StorePassword(this User user, string password)
        {
            user.password = BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static void VerifyPassword(this User user, string password)
        {
            BCrypt.Net.BCrypt.Verify(password, user.password);
        }

        public static void ComputeGravatar(this User user)
        {
            byte[] byteEmail = Encoding.UTF8.GetBytes(user.email.Trim().ToLower());

            using (MD5 md5 = MD5.Create())
            {
                byte[] byteHashedEmail = md5.ComputeHash(byteEmail);

                user.gravatar = string.Concat(byteHashedEmail.Select(b => b.ToString("x2")).ToArray());
            }
        }
    }
}