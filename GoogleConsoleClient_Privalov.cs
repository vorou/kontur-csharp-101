using System;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace GoogleConsoleClient
{
    class Program
    {
        static void Main()
        {
            var pageAmount = 5;
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://google.com");
            var input = driver.FindElementById("gbqfq");
            input.SendKeys("котятки");
 
            var searchButton = driver.FindElementById("gbqfb");
            searchButton.Click();
 
            Console.Clear();
            for (int i = 0; i < pageAmount; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("----- Page" + (int)(i + 1) + " -----");
                var serchgroup = driver.FindElementsByCssSelector(".r a");
                foreach (var element in serchgroup)
                {
                    Console.WriteLine(element.Text);
                }
 
                if (i < pageAmount - 1)
                {
                    var nextpage = driver.FindElementsById("pnnext");
                    driver.Navigate().GoToUrl(nextpage[0].GetAttribute("href"));
                }
            }
            Console.ReadLine();
            driver.Close();
        }
    }
}
