using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp1.Hashing
{
    internal class ExpiringUrl
    {
        public static string CreateHash(long expiry, string salt)
        {
            var bytes = Encoding.UTF8.GetBytes($"{salt}{expiry}");

            using (var sha = SHA1.Create())
                return string.Concat(sha.ComputeHash(bytes).Select(b => b.ToString("x2")));
        }
    }
}
