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
            System.Diagnostics.Debug.WriteLine(Guid.NewGuid().ToString());

            //WriteHeading("Using Cipher");
            //UsingCipher();
            //WriteEnding();

            //WriteHeading("Using Hash");
            //UsingHash();
            //WriteEnding();

            //WriteHeading("Using JWT");
            //UsingJWT();
            //WriteEnding();

            Console.Read();
        }
        
        static void UsingCipher()
        {
            string password = appSettings.ImageTokenSuperSecret;
            string plaintext = DateTime.Now.Ticks.ToString();
            Console.WriteLine($"Expected decrypted result is: {plaintext}");
            Console.WriteLine("");
            Console.WriteLine("Your encrypted string is:");
            string encryptedstring = Cipher.StringCipher.Encrypt(plaintext, password);
            Console.WriteLine(encryptedstring);
            Console.WriteLine("");

            Console.WriteLine("Your decrypted string is:");
            string decryptedstring = Cipher.StringCipher.Decrypt(encryptedstring, password);
            Console.WriteLine(decryptedstring);

        }

        public static void UsingHash()
        {
            Hashing.IExpiringUrlProvider hashProvider = new Hashing.ExpiringUrlProvider();

            var urlWithHash = hashProvider.Create(URL, appSettings.ImageTokenSuperSecret, TimeToLive);

            WriteGenerated(urlWithHash);
                        
            System.Threading.Thread.Sleep(TimeSpan.FromMinutes(TimeToLive + 1));

            Hashing.IExpiringUrlValidator hashValidator = new Hashing.ExpiringUrlValidator();
            IValidationResult validationResult = hashValidator.Validate(urlWithHash, appSettings.ImageTokenSuperSecret);

            if (validationResult.Status == ValidationResultStatuses.Valid)
            {
                Console.WriteLine("SUCCESS!");
            }
            else
            {
                Console.Write("FAIL: ");
                Console.WriteLine(validationResult.ErrorMessage);
            }
        }

        public static void UsingJWT()
        { 
            Tokens.ISimpleTokenProvider tokenProvider = new Tokens.SimpleTokenProvider();

            var tokenString = tokenProvider.Create(ImageName, appSettings.ImageTokenSuperSecret, TimeToLive);

            WriteGenerated(tokenString);
            
            System.Threading.Thread.Sleep(TimeSpan.FromMinutes(TimeToLive + 1));

            Tokens.ISimpleTokenValidator tokenValidator = new Tokens.SimpleTokenValidator();
            IValidationResult validationResult = tokenValidator.Validate(appSettings.ImageTokenSuperSecret, tokenString, TimeToLive);
                        
            if (validationResult.Status == Common.ValidationResultStatuses.Valid)
            {
                Console.Write("SUCCESS: ");
                Console.WriteLine(validationResult.Value);
            }
            else
            {
                Console.Write("FAIL: ");
                Console.WriteLine(validationResult.ErrorMessage);
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
            Console.WriteLine("");
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
    }
}
