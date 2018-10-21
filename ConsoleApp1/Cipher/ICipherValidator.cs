using ConsoleApp1.Common;

namespace ConsoleApp1.Cipher
{
    public interface ICipherValidator
    {
        IValidationResult Validate(string value, string salt);
    }
}
