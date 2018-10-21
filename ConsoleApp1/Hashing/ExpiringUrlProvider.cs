using System;

namespace ConsoleApp1.Hashing
{
    public class ExpiringUrlProvider : IExpiringUrlProvider
    {
        public string Create(string url, string salt, int timeToLiveInMinutes = 5)
        {
            var expires = DateTime.Now.AddMinutes(timeToLiveInMinutes).Ticks;
            var hash = ExpiringUrl.CreateHash(expires, salt);

            return $"{url}?{Constants.ExpiresParam}={expires}&{Constants.HashParam}={hash}";
        }

    }
}
