using Flux;
using System;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography;

namespace ConsoleApp
{
	class Program
	{
		private static void TimedMain(string[] _)
		{
			var dt = System.DateTime.Now;

			System.Console.WriteLine($"TimeZone: {dt.ToStringKind()}");
			System.Console.WriteLine($"TimeZone: {dt.ToStringSql()}");
			System.Console.WriteLine($"ISO8601OrdinalDate: {dt.ToStringISO8601OrdinalDate()}");
			System.Console.WriteLine($"ISO8601WeekDate: {dt.ToStringISO8601WeekDate()}");

			do
			{
				var ts = Flux.Random.NumberGenerator.Crypto.NextTimeSpan(new System.TimeSpan(-15, 0, 0, 0), new System.TimeSpan(15, 0, 0, 0));

				System.Console.WriteLine($"Of:{ts.ToStringOf()}");

				System.Console.WriteLine($"Xsd:{ts.ToStringXsd()}");
				System.Console.WriteLine($"XsdB:{ts.ToStringXsdBasic()}");
				System.Console.WriteLine($"XsdBE:{ts.ToStringXsdBasicExtended()}");

				//System.Console.WriteLine($" T:{ts.Ticks / 10}");
				//System.Console.WriteLine($" S:{ts.TotalSeconds}");
				//System.Console.WriteLine($"MS:{ts.TotalMilliseconds}");
				//System.Console.WriteLine($"1:{ts.ToStringOf()}");
				//System.Console.WriteLine($"2:{ts.ToStringOfSeconds()}");
				//System.Console.WriteLine($"ms:{ts.GetTotalMicroseconds()}");
				//System.Console.WriteLine($"ns:{ts.GetTotalNanoseconds():N20}".Replace(@",", string.Empty));

				System.Console.WriteLine();
			}
			while (System.Console.ReadKey().Key != System.ConsoleKey.A);

			System.Console.WriteLine();
			var sb = new System.Text.StringBuilder(@"Hello");
			sb.Append(' ');
			sb.Append(@"World");
			System.Console.WriteLine(sb.ToString());
			sb.ReplaceAll((c) => c == ' ' ? @"-wide-" : string.Empty);
			System.Console.WriteLine(sb.ToString());
			sb.ReplaceAll((c) => c == '-' ? @"**" : string.Empty);
			System.Console.WriteLine(sb.ToString());
			sb.ReplaceAll((c) => c == '*' ? @"++" : string.Empty);
			System.Console.WriteLine(sb.ToString());
			System.Console.WriteLine();

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
