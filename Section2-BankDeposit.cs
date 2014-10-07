using System;

namespace BankDeposit

{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please input the deposit amount:");
            double depositAmmount = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Please input an interest rate:");
            double interestRate = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Please input time of deposit in months:");
            int timeOfDeposit = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Accumulated sum is:");
            CalculationOfAccumulatedSum(depositAmmount, interestRate, timeOfDeposit);
            return;
        }


        static void CalculationOfAccumulatedSum(double depositAmmount, double interestRate, int timeOfDeposit)
        {
            double acumulatedSum = depositAmmount*Math.Pow((1 + interestRate/1200), timeOfDeposit);
            Console.WriteLine(acumulatedSum.ToString());
            return;
        }
    }
}
