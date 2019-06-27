namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sut = new SeleniumWrapper4000.SeleniumWrapper(false,100000))
            {
                sut.GoToUrl("http://google.com");

                int x = 5;
                sut.Wait(() => x-- == 0);
            }

        }
    }
}
