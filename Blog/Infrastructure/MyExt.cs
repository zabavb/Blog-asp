using System.Security.Cryptography;
using System.Text;

namespace Blog.Infrastructure
{
    public static class MyExt
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                string hashedPassword = Convert.ToBase64String(hashBytes);

                return hashedPassword;
            }
        }
    }
}
