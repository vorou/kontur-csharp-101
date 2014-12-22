using System;

namespace arrays
{
	class Program
	{
		public static int[] EvenNumbArray = new int[0];
		public static int[] GetFirstEvenNumbers(int count)
		{
			if (count % 2 == 0 && count > 0)
			{
				Array.Resize(ref EvenNumbArray, EvenNumbArray.Length + 1);
				EvenNumbArray[EvenNumbArray.Length - 1] = count;
				Array.Sort(EvenNumbArray);
			}
			return EvenNumbArray;
		}
		static void Main(string[] args)
		{
			GetFirstEvenNumbers(-1);
			GetFirstEvenNumbers(2);
			GetFirstEvenNumbers(4);
			GetFirstEvenNumbers(6);
			GetFirstEvenNumbers(8);
			GetFirstEvenNumbers(0);
			GetFirstEvenNumbers(2);
			GetFirstEvenNumbers(7);
			GetFirstEvenNumbers(-1);
			GetFirstEvenNumbers(16);

			foreach (var i in EvenNumbArray)
			{
				Console.WriteLine(i);
			}
		}
	}
}
