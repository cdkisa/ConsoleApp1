using ConsoleApp1.Common;

namespace ConsoleApp1.Common
{
    public interface IValidationResult
    {
        ValidationResultStatuses Status { get; set; }
        string ErrorMessage { get; set; }
        string Value { get; set; }
    }
}
