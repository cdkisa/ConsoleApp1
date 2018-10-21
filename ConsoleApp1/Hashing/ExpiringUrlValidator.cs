using ConsoleApp1.Common;
using System;
using System.Web;

namespace ConsoleApp1.Hashing
{
    public class ExpiringUrlValidator : IExpiringUrlValidator
    {
        private bool ValidateHash(long expiry, string salt, string originalHash) => ExpiringUrl.CreateHash(expiry, salt) == originalHash;

        public IValidationResult Validate(string url, string salt)
        {
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url));
            if (string.IsNullOrWhiteSpace(salt)) throw new ArgumentNullException(nameof(salt));

            try
            {
                var query = new Uri(url).Query;

                if (string.IsNullOrWhiteSpace(query)) return ValidationResult.Fail("Invalid query string parameters");

                var queryParams = HttpUtility.ParseQueryString(query);

                if (string.IsNullOrWhiteSpace(queryParams[Constants.ExpiresParam])) return ValidationResult.Fail("Invalid query string parameters");
                if (string.IsNullOrWhiteSpace(queryParams[Constants.HashParam])) return ValidationResult.Fail("Invalid query string parameters");

                var expiry = long.Parse(queryParams[Constants.ExpiresParam]);
                var hash = queryParams[Constants.HashParam];

                if (!ValidateHash(expiry, salt, hash)) return ValidationResult.Fail("Invalid query string parameters");

                if (!expiry.IsExpired()) return ValidationResult.Success("");

                return ValidationResult.Expired("Url has expired");

            }
            catch (Exception ex)
            {
                return ValidationResult.Fail(ex.Message);
            }
        }
    }
}
