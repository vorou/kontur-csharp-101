using System;
using System.Linq;

namespace Calculate
{
    class Program
    {
        static void Main()
        {
            var result1 = Calculate("90+2");
            var result2 = Calculate("900-2");
            var result3 = Calculate("900*12");
            var result4 = Calculate("900/125");
            Console.WriteLine(result1);
            Console.WriteLine(result2);
            Console.WriteLine(result3);
            Console.WriteLine(result4);
        }

        private static int Calculate(string input)
        {

            var position = Math.Max(input.IndexOf('+'), 0) + Math.Max(input.IndexOf('-'), 0) +
                           Math.Max(input.IndexOf('*'), 0) + Math.Max(input.IndexOf('/'), 0);
            var arg1 = int.Parse(input.Substring(0, position));
            var arg2 = int.Parse(input.Substring(position + 1));

            if (input.Contains('+'))
            {
                return (arg1 + arg2);
            }

            if (input.Contains('-'))
            {
                return (arg1 - arg2);
            }

            if (input.Contains('*'))
            {
                return (arg1 * arg2);
            }

            if (input.Contains('/'))
            {
                return (arg1 / arg2);
            }

            return int.Parse(input);
        }
    }
}
