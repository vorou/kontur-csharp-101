using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceCalc
{
	class Program
	{
		public static int monthCount;
		public static double depositRate;
		public static double depositSum;

		public static void GetDepositParams()
		{
			Console.WriteLine("Введите сумму вклада:");
			try
			{
				depositSum = Double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
			}
			catch
			{
				Console.WriteLine("Ошибка: сумма вклада должна быть числом.");
				return;
			}
			Console.WriteLine("Введите количество месяцев - срок вклада:");
			try
			{
				monthCount = Convert.ToInt32(Console.ReadLine());
			}
			catch
			{
				Console.WriteLine("Ошибка: количество должно быть целым числом.");
				return;
			}
			Console.WriteLine("Введите годовую процентную ставку:");
			try
			{
				depositRate = Double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
			}
			catch (Exception)
			{
				Console.WriteLine("Ошибка: ставка должна быть числом.");
				return;
			}
		}

		static double CalcDepositBalance(int monthCount, double depositRate, double depositSum)
		{
			const int monthsInYear = 12;
			double depositBalance = depositSum * Math.Pow((1 + depositRate / (monthsInYear * 100)), monthCount);
			return depositBalance;

		}
		static void Main(string[] args)
		{
			GetDepositParams();
			double depositBalance = CalcDepositBalance(monthCount, depositRate, depositSum);
			Console.WriteLine("Текущий остаток "+Math.Round(depositBalance, 2) + " руб");
		}
	}
}
