using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            char x;

            input = InputData("Введите строку");
            x = Convert.ToChar(InputData("Введите искомый символ"));

            Console.Write("Количество символов " +  x + " - " + CountOccurences(input, x));
            Console.ReadKey();
        }

        static string InputData(string text)
        {
            Console.WriteLine(text);
            return Console.ReadLine();
        }

        static int CountOccurences(string input, char x)
        {

            int amount = 0;

            //1. Поиском по строке
            int i = 0;
            while (input.IndexOf(x, i + 1) > -1)
            {
                amount++;
                i = input.IndexOf(x, i + 1);
            }


/*
            //2. Перебором символов строки
            for (int i = 0; i <= input.Length - 1 ; i++)
            {
                if ( Convert.ToChar(input.Substring(i,1)) == x) amount++;
            }
*/

/*
            //3. Перебором элементов массива
            char[] s = input.ToCharArray();
            for (int i = 1; i <= s.GetUpperBound(0); i++)
            {
                if (s[i] == x) amount++;
            }
*/
            return amount;
        }
    }
}