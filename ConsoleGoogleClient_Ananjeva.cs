using System;
using System.Threading;
using OpenQA.Selenium.Chrome;

namespace GoogleConsoleClient
{
    class ConsoleGoogleClient
    {
        const string GoogleWebDriverPath = "C:\\Users\\Ирина\\Downloads\\chromedriver_win32";

        private static void Main()
        {
            Console.WriteLine("Input request: ");
            var userRequest = Console.ReadLine();
            
            var driver = new ChromeDriver(GoogleWebDriverPath);
            driver.Navigate().GoToUrl("https://google.com");
            var requestString = driver.FindElementById("gbqfq");
            requestString.SendKeys(userRequest);
            var googleFindButton = driver.FindElementById("gbqfbw");
            googleFindButton.Click();
            Thread.Sleep(1000);
            var result = driver.FindElementsByCssSelector(".r a");
            foreach (var element in result)
            {
                Console.WriteLine(result.IndexOf(element)+1 + ". " +element.Text);
            }
            driver.Close();
        }
    }
}
