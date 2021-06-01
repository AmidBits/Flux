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

      // Adjacent List.

      var al = new Flux.Collections.Generic.Graph.AdjacentMatrix<string, double>();

      const string a = "a";
      const string b = "b";
      const string c = "c";
      const string d = "d";
      const string e = "e";

      al.AddVertex(a);
      al.AddVertex(b);
      al.AddVertex(c);
      al.AddVertex(d);
      al.AddVertex(e);

      //g.AddDirectedEdge(a, b, 1);
      //g.AddDirectedEdge(a, c, 1);
      //g.AddDirectedEdge(b, a, 1);
      //g.AddDirectedEdge(b, c, 1);
      //g.AddDirectedEdge(c, a, 1);
      //g.AddDirectedEdge(c, b, 1);
      //g.AddDirectedEdge(c, d, 1);
      //g.AddDirectedEdge(d, c, 1);

      al.AddDirectedEdge(a, b, 2.1);
      al.AddDirectedEdge(a, b, 5.4);
      al.AddDirectedEdge(b, a, 2);
      al.AddUndirectedEdge(a, c, 1);
      al.AddUndirectedEdge(b, c, 3);
      al.AddDirectedEdge(c, d, 6);
      al.AddUndirectedEdge(a, a, 7);
      //System.Console.WriteLine(al.ToConsoleString(w => w));
      ////al.RemoveUndirectedEdge(c, b, 1);
      //al.RemoveDirectedEdge(c, d, 6);
      //System.Console.WriteLine(al.ToConsoleString(w => w));
      //al.RemoveDirectedEdge(a, b, 2);
      //System.Console.WriteLine(al.ToConsoleString(w => w));

      // System.Console.WriteLine(al.ToConsoleString(w => w));
      //System.Console.WriteLine(al.ToString());

      foreach (var vertex in al.GetVertices())
        System.Console.WriteLine(vertex);
      foreach (var edge in al.GetEdges().OrderBy(e => e.Source.Value).ThenBy(e => e.Target.Value))
        System.Console.WriteLine(edge);

      //var outer = 6 * 16;
      //var inner = outer / 6;

      //var array = new int[outer][];

      //for (var o = array.GetLength(0) - 1; o >= 0; o--)
      //{
      //  array[o] = new int[1024 * 1024 * 1024 / inner];

      //  var factorial = Flux.Maths.Factorial(Flux.Random.NumberGenerator.Crypto.Next() & 0x3FFFFFF);

      //  for (var i = array[o].GetLength(0) - 1; i >= 0; i--)
      //    array[o][i] = factorial;
      //}

      //System.Threading.Thread.Sleep(1000);

      //return;

      //var set = new Flux.Numerics.BigDecimal[] { 1, 2, 2, 3, 5 };

      //foreach (var value in set)
      //  System.Console.WriteLine($"{value:G2}");

      //System.Console.WriteLine("Histogram:");
      //var histogram = set.Histogram(out var sumOfFrequencies);
      //System.Console.WriteLine(histogram.ToConsoleString());

      //System.Console.WriteLine("PMF:");
      //var pmf = histogram.ProbabilityMassFunction(sumOfFrequencies);
      //System.Console.WriteLine(pmf.ToConsoleString());

      //System.Console.WriteLine("CMF(CDF):");
      //var cdf = histogram.CumulativeMassFunction(sumOfFrequencies);
      //System.Console.WriteLine(cdf.ToConsoleString());

      //System.Console.WriteLine("PercentileRank:");
      //var plr = histogram.PercentileRank(sumOfFrequencies);
      //System.Console.WriteLine(plr.ToConsoleString());

      //System.Console.WriteLine("PercentRank:");
      //var pr = set.PercentRank();
      //System.Console.WriteLine(string.Join(System.Environment.NewLine, pr));

      //var count = set.Length;
      //var percentile = 50;

      //System.Console.WriteLine();
      //System.Console.WriteLine($"NearestRank: {Maths.PercentileOrdinalNearest(percentile, count)}, LerpRank: {Maths.PercentileOrdinalLerp(percentile, count)}");


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
