using System;

namespace Practice_01_01
{
    class Program
    {
        static void Main()
        {
            var sum = 0.0m;
            var percent = 0.0;
            var month = 0;

            if (!InputValue(out sum, "Введите начальную сумму:")) return;
            if (!InputValue(out percent, "Укажите процент:")) return;
            if (!InputValue(out month, "Введите период в месяцах:")) return;

            Console.WriteLine("Итоговая сумма: ");
            Console.WriteLine(Profit(sum, percent, month));
            Console.ReadLine();
        }
        static decimal Profit(decimal sum, double percent, int month)
        {
            var p = (double)sum;
            var percentMonth = (percent / 12) / 100;

            for (int i = 1; i <= month; i++) p = p + p * percentMonth;

            return (decimal)Math.Round(p, 2);
        }
        static bool InputValue(out decimal value,string text)
        {
            Console.WriteLine(text);
            if (!decimal.TryParse(Console.ReadLine(), out value)) return false;
            return true;
        }
        static bool InputValue(out double value, string text)
        {
            Console.WriteLine(text);
            if (!double.TryParse(Console.ReadLine(), out value)) return false;
            return true;
        }
        static bool InputValue(out int value, string text)
        {
            Console.WriteLine(text);
            if (!int.TryParse(Console.ReadLine(), out value)) return false;
            return true;
        }
    }
}
