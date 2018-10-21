namespace ConsoleApp1
{
    public class AppSettings : IAppSettings
    {
        private string Get(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }
        public string ImageTokenSuperSecret => Get("ImageTokenSuperSecret");
    }
}
