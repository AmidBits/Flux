using Flux;
using System;
using System.Linq;

// C# Interactive commands:
// #r "System.Runtime"
// #r "System.Runtime.Numerics"
// #r "C:\Users\Rob\source\repos\Flux\FluxSolution\BaseLibrary\bin\Debug\net5.0\BaseLibrary.dll"

namespace ConsoleApp
{
	class Program
	{
		//{
		//	int sum = targetNumber;
		//	int cipher = (int)Math.Log10(targetNumber);
		//	for (int i = 0; i <= cipher; i++)
		//	{
		//		sum += targetNumber % 10;
		//		targetNumber /= 10;
		//	}
		//	return sum;
		//}

		//static int GetGeneratorNumber(int targetNumber)
		//{
		//	int minPossipleNumber = Flux.Maths.SelfNumberLowBound(targetNumber, 10);
		//	int minCipher = (int)Math.Log10(minPossipleNumber);
		//	int minNumber = 0;
		//	for (int i = 1; i <= minCipher; i++)
		//	{
		//		minNumber += minPossipleNumber / (int)Math.Pow(10, i);
		//	}
		//	int maxCipher = (int)Math.Log10(targetNumber);
		//	int maxNumber = 0;
		//	for (int i = 1; i <= maxCipher; i++)
		//	{
		//		maxNumber += targetNumber / (int)Math.Pow(10, i);
		//	}


		//	for (int i = minNumber; i <= maxNumber; i++)
		//	{
		//		int generatorNumber = (targetNumber + (9 * i)) / 2;
		//		int generator = generatorNumber + Flux.Math.DigitSum(generatorNumber,10);
		//		if (generator == targetNumber)
		//		{
		//			return generatorNumber;
		//		}
		//	}
		//	return -1;
		//}

		private static void TimedMain(string[] _)
		{
			foreach (var type in typeof(Flux.Locale).Assembly.GetTypes())
			{
				foreach (var mi in type.GetMethods())
				{
					foreach (var pi in mi.GetParameters())
					{
						if (pi.ParameterType.IsArray)
						{
							System.Console.WriteLine($"{type.IsGenericType} {type.FullName} : {mi.Name} ({mi.IsGenericMethodDefinition}) : ({pi.Name})");
						}
					}
				}
			}

			//for (var i = 48; i < 128; i++)
			//{
			//	System.Console.Write($"'{(char)i}'");
			//	System.Console.WriteLine(',');
			//}

			//for (var i = 100; i >= 0; i--)
			////System.Linq.ParallelEnumerable.Range(-15, 32).ForAll(i =>
			//{
			//	var number = (uint)Flux.Random.NumberGenerator.Crypto.Next(4000);
			//	var sb = new System.Text.StringBuilder();
			//	sb.AppendLine($"Value: {number} (0x{number.ToString(@"X2")}, {System.Convert.ToString(number, 2)})");
			//	sb.AppendLine($"  LZC: {System.Numerics.BitOperations.LeadingZeroCount(number)} = {Flux.BitOps.LeadingZeroCount(number)}");
			//	sb.AppendLine($"  Ln2: {System.Numerics.BitOperations.Log2(number)} = {Flux.BitOps.Log2(number)}");
			//	sb.AppendLine($"  Pop: {System.Numerics.BitOperations.PopCount(number)} = {Flux.BitOps.PopCount(number)}");
			//	sb.AppendLine($"  TZC: {System.Numerics.BitOperations.TrailingZeroCount(number)} = {Flux.BitOps.TrailingZeroCount(number)}");
			//	sb.AppendLine($"   BL: {Flux.BitOps.BitLength(number)}");
			//	sb.AppendLine($" LS1B: {Flux.BitOps.LeastSignificant1Bit(number)}");
			//	sb.AppendLine($" MS1B: {Flux.BitOps.MostSignificant1Bit(number)}");
			//	System.Console.WriteLine($"{sb}");
			//	System.Console.ReadKey();
			//	System.Console.Clear();
			//}
			////);

			//RegularForLoop();
			//ParallelForLoop();
		}

		static void RegularForLoop()
		{
			var startDateTime = DateTime.Now;
			System.Console.WriteLine($"{nameof(RegularForLoop)} started at {startDateTime}.");
			for (int i = 0; i < 10; i++)
			{
				var total = ExpensiveTask();
				System.Console.WriteLine($"{nameof(ExpensiveTask)} {i} - {total}.");
			}
			var endDateTime = DateTime.Now;
			System.Console.WriteLine($"{nameof(RegularForLoop)} ended at {endDateTime}.");
			var span = endDateTime - startDateTime;
			System.Console.WriteLine($"{nameof(RegularForLoop)} executed in {span.TotalSeconds} seconds.");
			System.Console.WriteLine();
		}

		static void ParallelForLoop()
		{
			var startDateTime = DateTime.Now;
			System.Console.WriteLine($"{nameof(ParallelForLoop)} started at {startDateTime}.");
			System.Threading.Tasks.Parallel.For(0, 10, i =>
			{
				var total = ExpensiveTask();
				System.Console.WriteLine($"{nameof(ExpensiveTask)} {i} - {total}.");
			});
			var endDateTime = DateTime.Now;
			System.Console.WriteLine($"{nameof(ParallelForLoop)} ended at {endDateTime}.");
			var span = endDateTime - startDateTime;
			System.Console.WriteLine($"{nameof(ParallelForLoop)} executed in {span.TotalSeconds} seconds");
			System.Console.WriteLine();
		}

		static long ExpensiveTask()
		{
			var total = 0L;
			for (var i = 1; i < int.MaxValue; i++)
				total += i;
			return total;
		}

		static void Main(string[] args)
		{
			System.Console.InputEncoding = System.Text.Encoding.UTF8;
			System.Console.OutputEncoding = System.Text.Encoding.UTF8;

			System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => TimedMain(args), 1));

			System.Console.WriteLine($"{System.Environment.NewLine}Press any key to exit...");
			System.Console.ReadKey();
		}
	}
}
