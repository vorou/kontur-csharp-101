using System;
using OpenQA.Selenium.Firefox;
using System.Threading;

namespace googletest
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Введите поисковый запрос:");
            var request = Console.ReadLine();
            
            var driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("https://google.com");
            var input = driver.FindElementById("gbqfq");
            input.SendKeys(request);
            var button = driver.FindElementById("gbqfb");
            button.Click();
            Thread.Sleep(1000);
                       
            int count = 0;
            var results = driver.FindElementsByCssSelector(".r");
            foreach (var result in results)
            {
                count++;
                Console.WriteLine("{0}. {1}", count, result.Text);
            }

            driver.Close();
            Console.ReadLine();
        }
                
    }
}
