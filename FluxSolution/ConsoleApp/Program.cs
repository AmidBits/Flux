using Flux;
using System;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography;

namespace ConsoleApp
{
	interface INumericPolicy<T>
	{
		T One { get; }
		T Zero { get; }

		T Add(T a, T b);
		T Divide(T a, T b);
		T Multiply(T a, T b);
		T Subtract(T a, T b);
	}

	struct NumericPolicies
		: INumericPolicy<System.Numerics.BigInteger>, INumericPolicy<int>, INumericPolicy<long>
	{
		public static NumericPolicies Instance = new NumericPolicies();

		System.Numerics.BigInteger INumericPolicy<System.Numerics.BigInteger>.One => 1;
		int INumericPolicy<int>.One => 1;
		long INumericPolicy<long>.One => 1;

		System.Numerics.BigInteger INumericPolicy<System.Numerics.BigInteger>.Zero => 0;
		int INumericPolicy<int>.Zero => 0;
		long INumericPolicy<long>.Zero => 0;

		System.Numerics.BigInteger INumericPolicy<System.Numerics.BigInteger>.Add(System.Numerics.BigInteger a, System.Numerics.BigInteger b) => a + b;
		int INumericPolicy<int>.Add(int a, int b) => a + b;
		long INumericPolicy<long>.Add(long a, long b) => a + b;

		System.Numerics.BigInteger INumericPolicy<System.Numerics.BigInteger>.Divide(System.Numerics.BigInteger a, System.Numerics.BigInteger b) => a / b;
		int INumericPolicy<int>.Divide(int a, int b) => a / b;
		long INumericPolicy<long>.Divide(long a, long b) => a / b;

		System.Numerics.BigInteger INumericPolicy<System.Numerics.BigInteger>.Multiply(System.Numerics.BigInteger a, System.Numerics.BigInteger b) => a * b;
		int INumericPolicy<int>.Multiply(int a, int b) => a * b;
		long INumericPolicy<long>.Multiply(long a, long b) => a * b;

		System.Numerics.BigInteger INumericPolicy<System.Numerics.BigInteger>.Subtract(System.Numerics.BigInteger a, System.Numerics.BigInteger b) => a - b;
		int INumericPolicy<int>.Subtract(int a, int b) => a - b;
		long INumericPolicy<long>.Subtract(long a, long b) => a - b;
	}

	static class Algorithms
	{
		public static T Sum<P, T>(this P p, params T[] a)
				where P : INumericPolicy<T>
		{
			var r = p.Zero;
			foreach (var i in a)
			{
				r = p.Add(r, i);
			}
			return r;
		}
	}

	class Program
	{
		private static void TimedMain(string[] _)
		{


			var bi = System.Numerics.BigInteger.Parse("16000");

			System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => Flux.ParallelSplitFactorial.Default.ComputeProduct(bi).GetByteCount(false), 10));

			//System.Console.WriteLine($"{fas.ToString().Length} = {fas}");

			System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => Flux.Maths.Factorial(bi).GetByteCount(false), 10));

			//System.Console.WriteLine($"{fs.ToString().Length} = {fs}");

			//var lohis = new System.Collections.Generic.List<(System.Numerics.BigInteger n, System.Numerics.BigInteger m)>();
			//var tasks = new System.Collections.Generic.List<System.Threading.Tasks.Task<System.Numerics.BigInteger>>(1024);

			//var n = System.Numerics.BigInteger.Parse("1600");
			////System.Console.WriteLine($"fac:{Flux.Maths.Factorial(n)}");
			//System.Numerics.BigInteger high = n, low = n >> 1, shift = low, taskCounter = 0;

			//while ((low + 1) < high)
			//{
			//	var lohi = (low + 1, high);
			//	tasks.Add(Product(low + 1, high));
			//	//System.Console.WriteLine($"lohi: {lohi}");
			//	lohis.Add(lohi);
			//	high = low;
			//	low >>= 1;
			//	shift += low;
			//}

			////System.Console.WriteLine($"shift: {shift} ({lohis.Count})");

			//await System.Threading.Tasks.Task.WhenAll(tasks);

			//System.Numerics.BigInteger p = System.Numerics.BigInteger.One, r = System.Numerics.BigInteger.One;
			//for (var index = lohis.Count - 1; index >= 0; index--)
			//{
			//	var R = r * p;
			//	var t = p * tasks[index].Result;
			//	r = R;
			//	p = t;
			//}

			//System.Console.WriteLine((r * p) << (int)shift);

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
