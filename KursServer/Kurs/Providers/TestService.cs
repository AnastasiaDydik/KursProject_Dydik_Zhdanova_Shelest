namespace Kurs.Providers
{
    public class TestService : ITestService
    {
        public string GetString(string val)
        {
            return val;
        }
    }
}