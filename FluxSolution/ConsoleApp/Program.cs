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
      var allInts = new char[] { 'a', 'b', 'f', 'd', 'd', 'a', 'z', 'z', 'b', 'z', 'd', 'a', 'd', 'b', 'd', 'h', 'a', 'b', 'd' };
      var findInts = new char[] { 'a', 'b', 'd' };

      var im = allInts.GetIndexMap(findInts, c => c);

      //foreach (var sub in GetIt(allInts, findIntts))
      //{
      //  System.Console.WriteLine(string.Join('+', sub));
      //}

      // Adjacent List.

      var al = new Flux.Collections.Generic.Graph.AdjacentMatrixTypical<int, int>();

      //al.AddVertex(1);
      //al.AddVertex(2);
      //al.AddVertex(3);
      //al.AddVertex(4);
      //al.AddVertex(5);
      //al.AddVertex(6);

      var n = 1;
      al.AddEdge(1, 2, 1221);
      al.AddEdge(1, 5, 1551);
      al.AddEdge(2, 3, 2332);
      al.AddEdge(2, 5, 2552);
      al.AddEdge(3, 4, 3443);
      al.AddEdge(4, 5, 4554);
      al.AddEdge(4, 6, 4664);

      al.AddEdge(2, 1, 1221);
      al.AddEdge(5, 1, 1551);
      al.AddEdge(3, 2, 2332);
      al.AddEdge(5, 2, 2552);
      al.AddEdge(4, 3, 3443);
      al.AddEdge(5, 4, 4554);
      al.AddEdge(6, 4, 4664);

      //al.AddEdge(6, 6, n++);

      //var al = new Flux.Collections.Generic.Graph.AdjacentMatrix<string, double>();

      //const string a = "a";
      //const string b = "b";
      //const string c = "c";
      //const string d = "d";
      //const string e = "e";

      //al.AddVertex(a);
      //al.AddVertex(b);
      //al.AddVertex(c);
      //al.AddVertex(d);
      //al.AddVertex(e);

      //al.AddDirectedEdge(a, b, 1);
      //al.AddDirectedEdge(a, c, 1);
      //al.AddDirectedEdge(a, d, 1);
      //al.AddDirectedEdge(a, e, 1);
      //al.AddDirectedEdge(b, a, 1);
      //al.AddDirectedEdge(b, c, 1);
      //al.AddDirectedEdge(b, d, 1);
      //al.AddDirectedEdge(b, e, 1);
      //al.AddDirectedEdge(c, a, 1);
      //al.AddDirectedEdge(c, b, 1);
      //al.AddDirectedEdge(c, d, 1);
      //al.AddDirectedEdge(c, e, 1);
      //al.AddDirectedEdge(d, a, 1);
      //al.AddDirectedEdge(d, b, 1);
      //al.AddDirectedEdge(d, c, 1);
      //al.AddDirectedEdge(d, e, 1);
      //al.AddDirectedEdge(e, a, 1);
      //al.AddDirectedEdge(e, b, 1);
      //al.AddDirectedEdge(e, c, 1);
      //al.AddDirectedEdge(e, d, 1);

      //al.AddDirectedEdge(a, b, 2.1);
      //al.AddDirectedEdge(a, b, 5.4);
      //al.AddDirectedEdge(b, a, 2);
      //al.AddUndirectedEdge(a, c, 1);
      //al.AddUndirectedEdge(b, c, 3);
      //al.AddDirectedEdge(c, d, 6);
      //al.AddUndirectedEdge(a, a, 7);
      //System.Console.WriteLine(al.ToConsoleString(w => w));
      ////al.RemoveUndirectedEdge(c, b, 1);
      //al.RemoveDirectedEdge(c, d, 6);
      //System.Console.WriteLine(al.ToConsoleString(w => w));
      //al.RemoveDirectedEdge(a, b, 2);
      //System.Console.WriteLine(al.ToConsoleString(w => w));

      //System.Console.WriteLine($"Matrix:");
      //System.Console.WriteLine(al.ToConsoleString(w => w));

      System.Console.WriteLine($"Vertices:");
      var index = 1;
      foreach (var vertex in al.GetVertices())
        System.Console.WriteLine($"{index++}: {vertex}");
      System.Console.WriteLine($"Edges:");
      index = 1;
      foreach (var edge in al.GetEdges().OrderBy(e => e.source).ThenBy(e => e.target))
        System.Console.WriteLine($"{index++}: {edge}");

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
