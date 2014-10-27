using System;

namespace Divisor
{
    class Program
    {
        static void Main()
        {
            int num = GetNumber();
           
            Console.WriteLine(string.Format("Все делители числа {0}:", num));
            for (int i = 1; i <= num; i++)
            {
                if (num % i == 0)
                {
                    if (i == 1)
                    {
                        Console.WriteLine(string.Format("Делитель {0}", i));
                    }

                    else
                    {
                        int maxPow = 0;
                        for (int j = 1; Math.Pow(i, j) <= num; j++)
                        {
                            if (num % Math.Pow(i, j) == 0)
                            {
                                if (maxPow < j) maxPow = j;
                            }
                        }
                        Console.WriteLine(string.Format("Делитель {0}, его максимальная степень {1}", i, maxPow));
                    }
                    
                }
            }
        }

        static int GetNumber()
        {
            Console.WriteLine("Введите число");
            var numStr = Console.ReadLine();
            return int.Parse(numStr);
        }

    }
}
