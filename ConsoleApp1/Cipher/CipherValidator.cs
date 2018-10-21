using System;
using ConsoleApp1.Common;

namespace ConsoleApp1.Cipher
{
    public class CipherValidator : ICipherValidator
    {
        public IValidationResult Validate(string cipher, string salt)
        {
            try
            {
                var decryptedResult = StringCipher.Decrypt(cipher, salt);
                var values = decryptedResult.Split(Constants.ValueSeparator.ToCharArray());
                var expiry = long.Parse(values[0]);
                var value = values[1];

                if (!expiry.IsExpired()) return ValidationResult.Success(value);

                return ValidationResult.Expired("Expired");
            }
            catch (Exception ex)
            {
                return ValidationResult.Fail(ex.Message);
            }
        }
    }
}
