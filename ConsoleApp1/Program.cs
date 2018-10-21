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

            WriteHeading("Using Hash");
            UsingHash();
            WriteEnding();

            WriteHeading("Using JWT");
            UsingJWT();
            WriteEnding();

            Console.Read();
        }
        
        static void UsingCipher()
        {
            Cipher.ICipherProvider provider = new Cipher.CipherProvider();

            var securedValue = provider.Create(ImageName, appSettings.SuperSecret, 1);

            WriteGenerated(securedValue);

            System.Threading.Thread.Sleep(TimeSpan.FromMinutes(TimeToLive + 1));

            Cipher.ICipherValidator validator = new Cipher.CipherValidator();

            var validationResult = validator.Validate(securedValue, appSettings.SuperSecret);

            WriteValidationResult(validationResult);
        }

        public static void UsingHash()
        {
            Hashing.IExpiringUrlProvider provider = new Hashing.ExpiringUrlProvider();

            var urlWithHash = provider.Create(URL, appSettings.SuperSecret, TimeToLive);

            WriteGenerated(urlWithHash);
                        
            System.Threading.Thread.Sleep(TimeSpan.FromMinutes(TimeToLive + 1));

            Hashing.IExpiringUrlValidator validator = new Hashing.ExpiringUrlValidator();

            var validationResult = validator.Validate(urlWithHash, appSettings.SuperSecret);

            WriteValidationResult(validationResult);
        }

        public static void UsingJWT()
        { 
            Tokens.ISimpleTokenProvider provider = new Tokens.SimpleTokenProvider();

            var tokenString = provider.Create(ImageName, appSettings.SuperSecret, TimeToLive);

            WriteGenerated(tokenString);
            
            System.Threading.Thread.Sleep(TimeSpan.FromMinutes(TimeToLive + 1));

            Tokens.ISimpleTokenValidator validator = new Tokens.SimpleTokenValidator();

            var validationResult = validator.Validate(tokenString, appSettings.SuperSecret, TimeToLive);

            WriteValidationResult(validationResult);
        }

        #region Console 
        
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
            Console.WriteLine("GENERATED URL:");
            Console.WriteLine(generated);
            Console.WriteLine("");
            Console.WriteLine("========================================");
            Console.WriteLine("");
            Console.WriteLine("VALIDATED URL:");
        }

        #endregion
    }
}
