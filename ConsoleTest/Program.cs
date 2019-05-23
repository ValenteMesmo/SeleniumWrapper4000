namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sut = new SeleniumWrapper4000.SeleniumWrapper(false))
            {
                sut.GoToUrl("http://google.com");
            }

        }
    }
}
