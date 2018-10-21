namespace ConsoleApp1.Cipher
{
    public interface ICipherProvider
    {
        string Create(string value, string salt, int timeToLiveInMinutes = 5);
    }
}
