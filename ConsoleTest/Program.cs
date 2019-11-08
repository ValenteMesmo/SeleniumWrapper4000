using ValenteMesmo.SeleniumWrapper;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sut = new SeleniumWrapper(
                false
                , 100000
                , BrowserType.InternetExplorer))
            {
                sut.GoToUrl("http://google.com");

                int x = 50;
                sut.Wait(() => x-- == 0);
            }
        }
    }
}
