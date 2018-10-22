using ConsoleApp1.Common;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static AppSettings appSettings = new AppSettings();
        const int TimeToLive = 1;
        const string ImageName = "fd4b0232-4206-4651-bd11-169dbc747254.png";
        const string URL = "http://path/to/protected/resource/" + ImageName;

        static void Main(string[] args)
        {
            WriteHeading("Using Cipher");
            UsingCipher();
            WriteEnding();

            WriteHeading("Using Cipher With Expiry");
            UsingCipherWithExpiry();
            WriteEnding();

            WriteHeading("Using Hash");
            UsingHash();
            WriteEnding();

            WriteHeading("Using Hash With Expiry");
            UsingHashWithExpiry();
            WriteEnding();

            WriteHeading("Using JWT");
            UsingJWT();
            WriteEnding();

            WriteHeading("Using JWT With Expiry");
            UsingJWTWithExpiry();
            WriteEnding();

            Console.Read();
        }
        
        static void UsingCipher()
        {
            Cipher.ICipherProvider provider = new Cipher.CipherProvider();

            var securedValue = provider.Create(ImageName, appSettings.SuperSecret, TimeToLive);

            Console.WriteLine($"SECURED VALUE: {ImageName}");

            WriteGenerated(securedValue);

            Cipher.ICipherValidator validator = new Cipher.CipherValidator();

            var validationResult = validator.Validate(securedValue, appSettings.SuperSecret);

            WriteValidationResult(validationResult);
        }

        static void UsingCipherWithExpiry()
        {
            Cipher.ICipherProvider provider = new Cipher.CipherProvider();

            var securedValue = provider.Create(ImageName, appSettings.SuperSecret, TimeToLive);

            Console.WriteLine($"SECURED VALUE: {ImageName}");

            WriteGenerated(securedValue);

            NapTime(1);

            Cipher.ICipherValidator validator = new Cipher.CipherValidator();

            var validationResult = validator.Validate(securedValue, appSettings.SuperSecret);

            WriteValidationResult(validationResult);
        }

        public static void UsingHash()
        {
            Hashing.IExpiringUrlProvider provider = new Hashing.ExpiringUrlProvider();

            var securedValue = provider.Create(URL, appSettings.SuperSecret, TimeToLive);

            Console.WriteLine($"SECURED VALUE: {ImageName}");

            WriteGenerated(securedValue);
            
            Hashing.IExpiringUrlValidator validator = new Hashing.ExpiringUrlValidator();

            var validationResult = validator.Validate(securedValue, appSettings.SuperSecret);

            WriteValidationResult(validationResult);
        }

        public static void UsingHashWithExpiry()
        {
            Hashing.IExpiringUrlProvider provider = new Hashing.ExpiringUrlProvider();

            var securedValue = provider.Create(URL, appSettings.SuperSecret, TimeToLive);

            Console.WriteLine($"SECURED VALUE: {URL}");

            WriteGenerated(securedValue);

            NapTime(1);

            Hashing.IExpiringUrlValidator validator = new Hashing.ExpiringUrlValidator();

            var validationResult = validator.Validate(securedValue, appSettings.SuperSecret);

            WriteValidationResult(validationResult);
        }

        public static void UsingJWT()
        {
            Tokens.ISimpleTokenProvider provider = new Tokens.SimpleTokenProvider();

            var securedValue = provider.Create(ImageName, appSettings.SuperSecret, TimeToLive);

            Console.WriteLine($"SECURED VALUE: {ImageName}");

            WriteGenerated(securedValue);
            
            Tokens.ISimpleTokenValidator validator = new Tokens.SimpleTokenValidator();

            var validationResult = validator.Validate(securedValue, appSettings.SuperSecret);

            WriteValidationResult(validationResult);
        }

        public static void UsingJWTWithExpiry()
        { 
            Tokens.ISimpleTokenProvider provider = new Tokens.SimpleTokenProvider();

            var securedValue = provider.Create(ImageName, appSettings.SuperSecret, TimeToLive);

            Console.WriteLine($"SECURED VALUE: {ImageName}");

            WriteGenerated(securedValue);

            NapTime(1);

            Tokens.ISimpleTokenValidator validator = new Tokens.SimpleTokenValidator();

            var validationResult = validator.Validate(securedValue, appSettings.SuperSecret);

            WriteValidationResult(validationResult);
        }

        #region Console 
        
        static void NapTime(int howLong)
        {
            System.Threading.Thread.Sleep(TimeSpan.FromMinutes(TimeToLive + howLong));
        }

        static void WriteValidationResult(IValidationResult validationResult)
        {
            switch (validationResult.Status)
            {
                case ValidationResultStatuses.Valid:
                    Console.Write("SUCCESS: ");
                    Console.WriteLine(validationResult.Value);
                    break;
                case ValidationResultStatuses.Error:
                    Console.Write("ERROR: ");
                    Console.WriteLine(validationResult.ErrorMessage);
                    break;
                case ValidationResultStatuses.Expired:
                    Console.Write("EXPIRED: ");
                    Console.WriteLine(validationResult.ErrorMessage);
                    break;
                default:
                    Console.Write("UNKNOWN: ");
                    break;
            }
        }

        static void WriteHeading(string heading)
        {
            Console.WriteLine("========================================");
            Console.WriteLine(heading);
            Console.WriteLine("========================================");
            Console.WriteLine("");
        }

        static void WriteEnding()
        {
            Console.WriteLine("****************************************");
            Console.WriteLine("");
            Console.WriteLine("");
        }

        static void WriteGenerated(string generated)
        {
            Console.WriteLine("GENERATED VALUE:");
            Console.WriteLine(generated);
            Console.WriteLine("");
            Console.WriteLine("========================================");
            Console.WriteLine("");
            Console.WriteLine("VALIDATION RESULTS:");
        }

        #endregion
    }
}
