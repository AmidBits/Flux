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
		private static void TimedMain(string[] _)
		{
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
