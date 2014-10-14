using System;

namespace Practice_01_02
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(CountOccurences("adfasdf", 'a'));
            Console.ReadLine();
        }
        static int CountOccurences(string input, char x)
        {
            int count = 0;

            // Вариант 1. Посимвольный перебор
            for (int i = 0; i < input.Length; i++)
            {
                if (Char.Parse(input.Substring(i, 1)) == x)
                    count++;
            }

            // Вариант 2. Вырезаем символы и считаем разницу в длинах строк
            count = input.Length-input.Replace(x.ToString(),"").Length;

            // Вариант 3. Через поиск подстроки в строке
            count = 0;
            var j = input.IndexOf(x);
            while (j != -1)
            {
                j++;
                count++;
                j = input.IndexOf(x, j);
            }

            // Вариант 4. Через стандартное разбиение строки разделителем
            count = input.Split(x).Length - 1;

            return count;

        }
    }
}
