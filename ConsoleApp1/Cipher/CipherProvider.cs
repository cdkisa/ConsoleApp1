using System;

namespace ConsoleApp1.Cipher
{
    public class CipherProvider : ICipherProvider
    {
        public string Create(string value, string salt, int timeToLiveInMinutes)
        {
            var expiry = DateTime.Now.AddMinutes(timeToLiveInMinutes).Ticks;
            var toBeCiphered = $"{value}{Constants.ValueSeparator}{expiry}";

            return StringCipher.Encrypt(toBeCiphered, salt);
        }
    }
}
