using System;

namespace Console_Application
{
    class Program
    {
        static void Main()
        {
            decimal sum;
            double percent;
            int countMonths;
            sum = GetSum();
            percent = GetPercent();
            countMonths = GetCountMonths();
            var result = CountDeposit(sum, percent, countMonths);
            Console.WriteLine("Сумма на конец периода: " + result);
        }

        static decimal GetSum()
        {
            Console.WriteLine("Введите сумму вклада");
            string sumStr = Console.ReadLine();
            return decimal.Parse(sumStr);
        }

        static double GetPercent()
        {
            Console.WriteLine("Введите процентную ставку (%)");
            string percentStr = Console.ReadLine();
            return double.Parse(percentStr);
        }

        static int GetCountMonths()
        {
            Console.WriteLine("Введите срок вклада (количество месяцев)");
            string countMonthsStr = Console.ReadLine();
            return int.Parse(countMonthsStr);
        }

        static decimal CountDeposit(decimal sum, double percent, int countMonths)
        {
            var totalSum = (double)sum;

            for (int i = 0; i < countMonths; i++)
            {
                totalSum += (double)sum*(percent/12)/100;
            }
            return (decimal)totalSum;
        }
    }
}
