using System;
using System.Linq;


namespace names
{
	class NamesTasks
	{
		const int DaysInYear = 365;
		private const int MonthInYear = 12;
		public static void ShowBirthDayMonthStatistics(NameData[] names)
		{
			Console.WriteLine("Статистика рождаемости по датам");
			int countFirstDayBirth = 0;
			int countLastDayBirth = 0;
			int countMiddleDayBirth = 0;
			for (int i = 0; i < names.Length; i++)
			{
				if (names[i].BirthDate.Day == 1)
					countFirstDayBirth++;
				else if (DateTime.DaysInMonth(names[i].BirthDate.Year, names[i].BirthDate.Month) == names[i].BirthDate.Day)
					countLastDayBirth++;
				else
					countMiddleDayBirth++;
			}
			Console.WriteLine("кол-во родившихся в первый день месяца: {0},\n" +
							  "кол-во родившихся в последний день месяца: {1},\n" +
							  "кол-во родившихся где-то посередине: {2}\n",
							  countFirstDayBirth, countLastDayBirth, countMiddleDayBirth / (DaysInYear - MonthInYear * 2));
			/*
			Выведите на консоль количество людей, рожденных в первый день месяца, в последний день месяца, 
			и усредненное по всем остальным дням месяца количество рожденных людей
			(то есть количество людей, рожденных не первого и не последнего числа, 
			деленное на количество не первых и не последних чисел в году).
			Все три величины должны быть вычислены в одном цикле.
			Количество дней в году можно считать равным 365.
			
			Попробуйте объяснить результат.

			Можно обращаться к свойствам NameData так names[0].Name 
			Полностью аналогично тому, как вы обращаетесь к свойствам строки s.Length
			*/
		}

		public static void ShowBirthYearsStatisticsHistogram(NameData[] names)
		{
			var yearsArr = new int[names.Length];
			int z = 0;
			foreach (var item in names)
			{
				yearsArr[z++] = item.BirthDate.Year;
			}
			var years = yearsArr.Distinct().OrderBy(y => y).ToArray();
			var usersToYear = yearsArr.OrderBy(y => y).GroupBy(y => y).Select(y => y.Count()).ToArray();
			Console.WriteLine("Статистика рождаемости по годам");
			Histogram.Show("Рождаемость по годам", years, usersToYear);
			/*
			Напишите код, готовящий данные для построения гистограммы 
			— количества людей в выборке в зависимости от года их рождения.
			Если вас пугает незнакомое слово гистограмма — вам как обычно в википедию! 
			http://ru.wikipedia.org/wiki/%D0%93%D0%B8%D1%81%D1%82%D0%BE%D0%B3%D1%80%D0%B0%D0%BC%D0%BC%D0%B0
			*/
		}

		public static void ShowBirthDayMonthStatisticsForName(NameData[] names, string name)
		{
			Console.WriteLine("Статистика рождаемости имени {0}", name);
			var birthDateArr = new DateTime[1];
			int arrIndexer = 0;
			foreach (var item in names)
			{
				if (item.Name == name)
				{
					birthDateArr[arrIndexer++] = item.BirthDate;
					Array.Resize(ref birthDateArr, (birthDateArr.Length + 1));
				}
			}
			var userToDate =
				from cust in birthDateArr
				group cust by cust.Date
					into custGroup
					orderby custGroup.Count() descending
					select new { CustomCount = custGroup.Count(), CustomDate = custGroup.Key };

			var userToDayMonth =
				from cust in birthDateArr
				group cust by new { cust.Day, cust.Month }
					into custGroup
					orderby custGroup.Count() descending
					select new { CustomCount = custGroup.Count(), CustomDate = custGroup.Key };

			var MaxCountDate = userToDayMonth.ToArray().First().CustomDate;
			var MaxCount = userToDayMonth.ToArray().First().CustomCount;
			Console.WriteLine("число: {0}, месяц: {1}", MaxCountDate.Day, System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.MonthNames[MaxCountDate.Month]);
			int countUsersToDate = 0;
			for (int i = 0; i < names.Length; i++)
			{
				if (names[i].BirthDate.Day == MaxCountDate.Day && names[i].BirthDate.Month == MaxCountDate.Month)
				{
					countUsersToDate++;
				}
			}
			Console.WriteLine(MaxCount * 100 / countUsersToDate + "% от общего числа родившихся на эту дату");

			//http://stackoverflow.com/questions/847066/group-by-multiple-columns
			/*
			Выведите на консоль:
				1. Дату без года (только день и месяц) с максимальной рождаемостью людей с именем name.
				2. Процент людей с заданным именем, рожденных в найденную дату из предыдущего пункта.
				Проинтерпретируйте результат этой функции на именах Виктория, Юрий, Илья, Владимир.
				Сильно ли выше среднего рождаемость в самые "плодотворные" дни?
			 */
		}
	}
}
