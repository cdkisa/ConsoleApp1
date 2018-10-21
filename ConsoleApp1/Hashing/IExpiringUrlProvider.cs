namespace ConsoleApp1.Hashing
{
    public interface IExpiringUrlProvider
    {
        string Create(string value, string salt, int timeToLiveInMinutes = 5);
    }
}
