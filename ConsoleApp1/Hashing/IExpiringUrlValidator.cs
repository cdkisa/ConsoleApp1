using ConsoleApp1.Common;

namespace ConsoleApp1.Hashing
{
    public interface IExpiringUrlValidator
    {
        IValidationResult Validate(string value, string salt);
    }
}
