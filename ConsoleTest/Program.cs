using System;
using ValenteMesmo.SeleniumWrapper;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var driver = new SeleniumWrapper(false, 2000))
            {
                driver.GoToUrl("https://www.youtube.com/watch?v=hD1qJ8Gbons");
            }
        }
    }
}
