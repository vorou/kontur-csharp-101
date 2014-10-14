using System;

namespace Practice_01_03
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(Calculate("23+34"));
            Console.WriteLine(Calculate("23-34"));
            Console.WriteLine(Calculate("23*34"));
            Console.WriteLine(Calculate("23/34"));
            Console.ReadLine();
        }
        static double Calculate(string input)
        {

            string[] split = input.Split(new Char[] { '+', '-', '*', '/' });

            // проверок по условиям задачи делать не нужно

            var left = double.Parse(split[0]);
            var rigth = double.Parse(split[1]);
            var symbol = input.Substring(split[0].Length,1);

            if (symbol == "+") return left + rigth;
            if (symbol == "-") return left - rigth;
            if (symbol == "*") return left * rigth;
            if (symbol == "/") return left / rigth;

            return 0;

        }
    }
}