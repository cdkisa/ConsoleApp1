using ConsoleApp1.Common;

namespace ConsoleApp1.Tokens
{
    public interface ISimpleTokenValidator
    {
        IValidationResult Validate(string secret, string token);
    }
}
