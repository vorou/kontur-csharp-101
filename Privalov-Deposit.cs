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
            double sum;
            double percent;
            int amountMonth;

            //Input initial data
            sum = Convert.ToDouble(InputData("Введите сумму вклада"));
            percent = Convert.ToDouble(InputData("Введите процентную ставку"));
            amountMonth = Convert.ToInt32(InputData("Введите срок вклада"));

            //Calculate sum
            sum = CalcSum(sum, percent, amountMonth);

            //Output result
            Console.WriteLine();
            Console.Write("Сумма вклада составит: " + Math.Round(sum, 2) + " руб.");
            Console.ReadKey();
        }


        static string InputData(string text)
        {
            Console.WriteLine(text);
            return Console.ReadLine();
        }


        static double CalcSum(Double sum, Double percent, int amountMonth)
        {

            for (int i = 1; i <= amountMonth; i++)
            {
                //sum = sum + sum * (1 / 12) * (percent / 100); // не хочет считать -(
                sum = sum + sum * (percent / 100 / 12);
            }
            return sum;
        }
    }
}