using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace CountOccurences
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Введите строку:");
            var input = Console.ReadLine();
            char sign = 'y';
            var count = CountOccurences(input, sign);
            var result = string.Format("Символ '{0}' встречается {1} раз", sign, count);
            Console.WriteLine(result);

        }

        static int CountOccurences(string input, char x)
        {
            int count = 0;
            
            //for (int i = 0; i < input.Length; i++)
            //{
            //    if (input[i] == x)  count++;
            //}

            char[] chars = input.ToCharArray();
            foreach (char ch in chars)
            {
                if (ch == x) count++;
            }


            return count;
        }
    }
}
