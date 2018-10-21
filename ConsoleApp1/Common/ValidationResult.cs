using ConsoleApp1.Common;

namespace ConsoleApp1.Common
{
    public class ValidationResult : IValidationResult
    {
        public ValidationResultStatuses Status { get; set; }
        public string ErrorMessage { get; set; }
        public string Value { get; set; }

        public static IValidationResult Success(string value) => new ValidationResult { Status = ValidationResultStatuses.Valid, Value = value };
        public static IValidationResult Fail(string message) => new ValidationResult { Status = ValidationResultStatuses.Error, ErrorMessage = message };
        public static IValidationResult Expired(string message) => new ValidationResult { Status = ValidationResultStatuses.Expired, ErrorMessage = message };
    }

    
}
