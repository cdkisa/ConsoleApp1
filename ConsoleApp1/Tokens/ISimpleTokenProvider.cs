namespace ConsoleApp1.Tokens
{
    public interface ISimpleTokenProvider
    {
        string Create(string value, string salt, int timeToLiveInMinutes = 5);
    }
}
