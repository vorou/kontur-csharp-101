using System;

namespace Practice_01_04
{
    class Program
    {
        static void Main()
        {
            string[] keyword = { "one", "two" };
            string[] category = { "cat1", "cat2" };
            var region = "moscow";

            Console.WriteLine(GetSearchSummary(keyword, category, region));
            Console.ReadLine();

        }

        static string GetSearchSummary(string[] keyword, string[] category, string region)
        {
            foreach (string str in keyword)
                if (!String.IsNullOrWhiteSpace(str)) return str;
            foreach (string str in category)
                if (!String.IsNullOrWhiteSpace(str)) return str;
            return region;
        }
    }
}
