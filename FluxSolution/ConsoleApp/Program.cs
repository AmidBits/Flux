using Flux;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Threading;

// C# Interactive commands:
// #r "System.Runtime"
// #r "System.Runtime.Numerics"
// #r "C:\Users\Rob\source\repos\Flux\FluxSolution\BaseLibrary\bin\Debug\net5.0\BaseLibrary.dll"

namespace ConsoleApp
{
	public class User
	{
		public int Age { get; set; }
		public string Name { get; set; }
		public double TotalAge { get; set; }

		public override string ToString()
		{
			return $"<{nameof(User)}: {Name}, {Age} ({TotalAge})>";
		}
	}

	class Program
	{
		private static void TimedMain(string[] _)
		{
			var rules = new Flux.Model.RuleCollection<User>();
			rules.Add(new Flux.Model.Rule("Age", "GreaterThan", "20"));
			rules.Add(new Flux.Model.Rule("Name", "Equal", "John"));
			rules.Add(new Flux.Model.Rule("TotalAge", "Equal", "32.90000000000"));

			foreach (var rule in rules.m_rules)
				System.Console.WriteLine(rule);

			var user1 = new User()
			{
				Age = 43,
				Name = "royi",
				TotalAge = 43.1
			};
			var user2 = new User
			{
				Age = 33,
				Name = "John",
				TotalAge = 32.9
			};
			var user3 = new User
			{
				Age = 19,
				Name = "John",
				TotalAge = 19
			};

			System.Console.WriteLine(rules.Evaluate(user1));
			System.Console.WriteLine(rules.Evaluate(user2));
			System.Console.WriteLine(rules.Evaluate(user3));

			//;
			//var rule = new Flux.Model.Rule("Age", "GreaterThan", "20");
			//Func<User, bool> compiledRule = Flux.Model.RulesEngine.CompileRule<User>(rule);
			//rules.ForEach(r => r(user1).Dump());

			//for (var i = 100; i >= 0; i--)
			////System.Linq.ParallelEnumerable.Range(-15, 32).ForAll(i =>
			//{
			//	var number = (uint)Flux.Random.NumberGenerator.Crypto.Next();
			//	var sb = new System.Text.StringBuilder();
			//	sb.AppendLine($"             Decimal: {number.ToBigInteger().ToGroupString()}");
			//	sb.AppendLine($"                 Hex: {number.ToString(@"X8")}");
			//	sb.AppendLine($"              Binary: {System.Convert.ToString(number, 2).PadLeft(32, '0')}");
			//	sb.AppendLine($"           BitLength: {Flux.BitOps.BitLength(number)}");
			//	sb.AppendLine($"              FoldHi: {System.Convert.ToString(Flux.BitOps.FoldHigh(number), 2).PadLeft(32, '0')}");
			//	sb.AppendLine($"              FoldLo: {System.Convert.ToString(Flux.BitOps.FoldLow(number), 2).PadLeft(32, '0')}");
			//	sb.AppendLine($"    LeadingZeroCount: {System.Numerics.BitOperations.LeadingZeroCount(number)} = {Flux.BitOps.LeadingZeroCount(number)}");
			//	sb.AppendLine($"                Log2: {System.Numerics.BitOperations.Log2(number)} = {Flux.BitOps.Log2(number)}");
			//	sb.AppendLine($"            PopCount: {System.Numerics.BitOperations.PopCount(number)} = {Flux.BitOps.PopCount(number)}");
			//	sb.AppendLine($"LeastSignificant1Bit: {System.Convert.ToString(Flux.BitOps.LeastSignificant1Bit(number), 2).PadLeft(32, '0')}");
			//	sb.AppendLine($" MostSignificant1Bit: {System.Convert.ToString(Flux.BitOps.MostSignificant1Bit(number), 2).PadLeft(32, '0')}");
			//	sb.AppendLine($"   TrailingZeroCount: {System.Numerics.BitOperations.TrailingZeroCount(number)} = {Flux.BitOps.TrailingZeroCount(number)}");
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
