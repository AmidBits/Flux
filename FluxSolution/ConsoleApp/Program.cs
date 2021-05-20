using System;
using System.Buffers;
using System.Linq;
using Flux;

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
      //var set = new int[] { 40, 45, 55, 67, 77, 85, 89, 88, 81, 69, 56, 43 };
      var set = new int[] { 55, 66, 77, 88, 99 };
      //var set = new int[] { 1, 2, 2, 3, 5 };

      System.Console.WriteLine("Histogram:");
      var histogram = set.Histogram(out var sumOfFrequencies);
      foreach (var kvp in histogram) System.Console.WriteLine($"{kvp.Key} = '{kvp.Value}'");

      System.Console.WriteLine("PMF:");
      var pmf = histogram.ProbabilityMassFunction(sumOfFrequencies);
      foreach (var kvp in pmf) System.Console.WriteLine($"{kvp.Key} = '{kvp.Value}'");

      System.Console.WriteLine("CDF:");
      var cdf = histogram.CumulativeMassFunction(sumOfFrequencies);
      foreach (var kvp in cdf) System.Console.WriteLine($"{kvp.Key} = '{kvp.Value}'");

      System.Console.WriteLine("PercentileRank:");
      var plr = histogram.PercentileRank(sumOfFrequencies);
      foreach (var kvp in plr) System.Console.WriteLine($"{kvp.Key} = '{kvp.Value}'");

      System.Console.WriteLine("PercentRank:");
      var pr = set.PercentRank();
      foreach (var kvp in pr) System.Console.WriteLine($"{kvp}'");

      var count = set.Length;
      var percentile = 50;

      System.Console.WriteLine();
      System.Console.WriteLine($"NearestRank: {Maths.PercentileOrdinalNearest(percentile, count)}, LerpRank: {Maths.PercentileOrdinalLerp(percentile, count)}");


      //System.Console.WriteLine($"{set.ProbabilityMassFunction(2/*, out var x, out var y*/)}");
      //System.Console.WriteLine($"{set.CumulativeMassFunction(2)}");

      //System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => RegularForLoop(10, 0.1), 1));
      //System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => ParallelForLoop(10, 0.1), 1));
    }

    static void RegularForLoop(int taskCount = 10, double taskLoad = 1)
    {
      //var startDateTime = DateTime.Now;
      //System.Console.WriteLine($"{nameof(RegularForLoop)} started at {startDateTime}.");
      for (int i = 0; i < taskCount; i++)
      {
        ExpensiveTask(taskLoad);
        //var total = ExpensiveTask(taskLoad);
        //System.Console.WriteLine($"{nameof(ExpensiveTask)} {i} - {total}.");
      }
      //var endDateTime = DateTime.Now;
      //System.Console.WriteLine($"{nameof(RegularForLoop)} ended at {endDateTime}.");
      //var span = endDateTime - startDateTime;
      //System.Console.WriteLine($"{nameof(RegularForLoop)} executed in {span.TotalSeconds} seconds.");
      //System.Console.WriteLine();
    }

    static void ParallelForLoop(int taskCount = 10, double taskLoad = 1)
    {
      //var startDateTime = DateTime.Now;
      System.Threading.Tasks.Parallel.For(0, taskCount, i =>
      {
        ExpensiveTask(taskLoad);
        //var total = ExpensiveTask(taskLoad);
        //System.Console.WriteLine($"{nameof(ExpensiveTask)} {i} - {total}.");
      });
      //var endDateTime = DateTime.Now;
      //System.Console.WriteLine($"{nameof(ParallelForLoop)} ended at {endDateTime}.");
      //var span = endDateTime - startDateTime;
      //System.Console.WriteLine($"{nameof(ParallelForLoop)} executed in {span.TotalSeconds} seconds");
      //System.Console.WriteLine();
    }

    static long ExpensiveTask(double taskLoad = 1)
    {
      var total = 0L;
      for (var i = 1; i < int.MaxValue * taskLoad; i++)
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
