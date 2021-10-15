using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

using Flux;

// C# Interactive commands:
// #r "System.Runtime"
// #r "System.Runtime.Numerics"
// #r "C:\Users\Rob\source\repos\Flux\FluxSolution\BaseLibrary\bin\Debug\net5.0\BaseLibrary.dll"

namespace ConsoleApp
{
  class Program
  {
    #region Amb-testing
    static System.Numerics.BigInteger[] a = Flux.Numerics.PrimeNumber.GetAscendingPrimes(2).Take(100).ToArray(); // Primes.
    static int[] b = System.Linq.Enumerable.Range(0, 100).ToArray(); // Rational.
    static int[] c = System.Linq.Enumerable.Range(1, 200).Where(i => (i & 1) == 0).ToArray(); // Even.
    static int[] d = System.Linq.Enumerable.Range(1, 200).Where(i => (i & 1) != 0).ToArray(); // Odd.

    private static void AmbTesting()
    {
      for (var i = 0; i < 3; i++)
      {
        AmbTestingImpl();

        System.Console.WriteLine();
      }

      static void AmbTestingImpl()
      {
        var amb = new Flux.AmbOps.Amb();

        #region Flow & Measurements
        a.Shuffle();
        b.Shuffle();
        c.Shuffle();
        d.Shuffle();

        //var l = a.Length + b.Length + c.Length + d.Length;
        //System.Console.WriteLine($"Length: {l}");
        #endregion

        var x = amb.Choose(a);
        var y = amb.Choose(b);
        var z = amb.Choose(c);
        var w = amb.Choose(d);
        var answer = 29;

        amb.RequireFinal(() => x.Value + y.Value + z.Value + w.Value == answer);

        //System.Console.WriteLine($"{nameof(amb.Disambiguate)}: {amb.Disambiguate()}");

        System.Console.WriteLine($"{x} + {y} + {z} + {w} == {answer}");
      }
    }
    #endregion

    private static void TimedMain(string[] args)
    {
      //AmbTesting();

      //var golg = new Flux.Model.GameOfLife.Game(new Flux.Geometry.Size2(30, 30), true, 0.2);
      //golg.RunInConsole(100);

      //return;

      //var m = new int[,] {
      //  { 1, 2, 3, 4 },
      //  { 5, 6, 7, 8 },
      //  { 9, 10, 11, 12 },
      //};
      //System.Console.WriteLine(nameof(m));
      //System.Console.WriteLine(m.ToConsoleBlock(uniformWidth: true));
      //System.Console.WriteLine();

      //var mrotatec = m.RotateClockwise();
      //System.Console.WriteLine(nameof(mrotatec));
      //System.Console.WriteLine(mrotatec.ToConsoleBlock(uniformWidth: true));
      //System.Console.WriteLine();

      //var mrotatecc = m.RotateCounterClockwise();
      //System.Console.WriteLine(nameof(mrotatecc));
      //System.Console.WriteLine(mrotatecc.ToConsoleBlock(uniformWidth: true));
      //System.Console.WriteLine();

      //var mtranspose = m.Transpose();
      //System.Console.WriteLine(nameof(mtranspose));
      //System.Console.WriteLine(mtranspose.ToConsoleBlock(uniformWidth: true));
      //System.Console.WriteLine();

      //var minsert = m.Insert(1, 1, 4, 0);
      //System.Console.WriteLine(nameof(minsert));
      //System.Console.WriteLine(minsert.ToConsoleBlock(uniformWidth: true));
      //System.Console.WriteLine();
      //minsert.Fill(0, 1, 3, 4, 9);
      //System.Console.WriteLine(nameof(minsert.Fill));
      //System.Console.WriteLine(minsert.ToConsoleBlock(uniformWidth: true));
      //System.Console.WriteLine();

      //var mflip0 = m.Flip(0);
      //System.Console.WriteLine(nameof(mflip0));
      //System.Console.WriteLine(mflip0.ToConsoleBlock(uniformWidth: true));
      //System.Console.WriteLine();

      //var mflip1 = m.Flip(1);
      //System.Console.WriteLine(nameof(mflip1));
      //System.Console.WriteLine(mflip1.ToConsoleBlock(uniformWidth: true));
      //System.Console.WriteLine();

      //var mremove = m.Remove(1, 1);
      //System.Console.WriteLine(nameof(mremove));
      //System.Console.WriteLine(mremove.ToConsoleBlock(uniformWidth: true));
      //System.Console.WriteLine();

      //return;

      //var gam = new Flux.DataStructures.Graphs.AdjacencyList();

      //gam.AddVertex(0);
      //gam.AddVertex(1);
      //gam.AddVertex(2);
      //gam.AddVertex(3);
      //gam.AddVertex(4);

      //// 6, 8
      //gam.AddEdge(0, 1, (3, 1));
      //gam.AddEdge(0, 2, (1, 0));
      //gam.AddEdge(0, 4, (3, 2));
      //gam.AddEdge(1, 2, (2, 0));
      //gam.AddEdge(1, 3, (0, 3));
      //gam.AddEdge(2, 3, (1, 0));
      //gam.AddEdge(2, 4, (6, 0));
      //gam.AddEdge(3, 4, (2, 1));

      //// 10, 1
      ////gam.AddEdge(0, 1, (3, 1));
      ////gam.AddEdge(0, 2, (4, 0));
      ////gam.AddEdge(0, 3, (5, 0));
      ////gam.AddEdge(1, 2, (2, 0));
      ////gam.AddEdge(2, 3, (4, 0));
      ////gam.AddEdge(2, 4, (1, 0));
      ////gam.AddEdge(3, 4, (10, 0));

      //System.Console.WriteLine(gam.ToConsoleString());

      ////var mcmf = gam.GetBellmanFordMaxFlowMinCost(0, 4, o => o is null ? 0 : ((System.ValueTuple<int, int>)o).Item1, o => o is null ? 0 : ((System.ValueTuple<int, int>)o).Item2);
      ////System.Console.WriteLine($"BellmanFord Min-Cost-Max-Flow: {mcmf}");
      //System.Console.WriteLine();

      //var cap101 = new double[,] {
      //  { 0, 3, 4, 5, 0 },
      //  { 0, 0, 2, 0, 0 },
      //  { 0, 0, 0, 4, 1 },
      //  { 0, 0, 0, 0, 10 },
      //  { 0, 0, 0, 0, 0 }
      //};
      //var cost101 = new double[,] {
      //  { 0, 1, 0, 0, 0 },
      //  { 0, 0, 0, 0, 0 },
      //  { 0, 0, 0, 0, 0 },
      //  { 0, 0, 0, 0, 0 },
      //  { 0, 0, 0, 0, 0 }
      //};

      //return;

      //var am = new Flux.DataStructures.Graphs.AdjacencyMatrix();

      //am.AddVertex(0, 'a');
      //am.AddVertex(1, 'b');
      //am.AddVertex(2, 'c');
      //am.AddVertex(3, 'd');
      //am.AddVertex(4, 'e');
      //am.AddVertex(5, 'f');
      //am.AddVertex(6, 'g');
      //am.AddVertex(7, 'h');
      //am.AddVertex(8, 'i');

      //am.AddEdge(0, 1, 4);
      //am.AddEdge(1, 0, 4);

      //am.AddEdge(0, 7, 8);

      //am.AddEdge(1, 2, 8);

      //am.AddEdge(2, 1, 8);
      //am.AddEdge(2, 3, 7);
      //am.AddEdge(2, 5, 4);
      //am.AddEdge(2, 8, 2);

      //am.AddEdge(3, 2, 7);
      //am.AddEdge(3, 5, 14);
      //am.AddEdge(3, 3, 13);
      //am.AddEdge(3, 4, 9);

      //am.AddEdge(4, 3, 9);
      //am.AddEdge(4, 5, 10);

      //am.AddEdge(5, 2, 4);
      //am.AddEdge(5, 3, 14);
      //am.AddEdge(5, 4, 10);
      //am.AddEdge(5, 6, 2);

      //am.AddEdge(6, 5, 2);
      //am.AddEdge(6, 7, 1);
      //am.AddEdge(6, 8, 6);

      //am.AddEdge(7, 0, 8);
      //am.AddEdge(7, 1, 11);
      //am.AddEdge(7, 6, 1);
      //am.AddEdge(7, 8, 7);

      //am.AddEdge(8, 2, 2);
      //am.AddEdge(8, 6, 6);
      //am.AddEdge(8, 7, 7);

      //System.Console.WriteLine(am.ToConsoleString());

      //System.Console.WriteLine(@"Dijkstra 'Shortest Path Tree' (a.k.a. SPT) from vertex 0 (destination, distance):");
      //System.Console.WriteLine($"{string.Join(System.Environment.NewLine, am.GetDijkstraShortestPathTree(0, o => System.Convert.ToDouble(o)))}");

      //return;

      var os1 = new Flux.DataStructures.OrderedSet<int>() { 1, 2, 3, 4, 5, 6 };
      var os2 = new Flux.DataStructures.OrderedSet<int>() { 4, 5, 6, 7, 8, 9 };

      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(os1)}"); foreach (var item in os1) System.Console.WriteLine(item);
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(os2)}"); foreach (var item in os2) System.Console.WriteLine(item);
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.SetSourceDifference)}"); foreach (var item in Flux.SetOps.SetSourceDifference(os1, os2)) System.Console.WriteLine(item);
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.SetSymmetricDifference)}"); foreach (var item in Flux.SetOps.SetSymmetricDifference(os1, os2)) System.Console.WriteLine(item);
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.SetTargetDifference)}"); foreach (var item in Flux.SetOps.SetTargetDifference(os1, os2)) System.Console.WriteLine(item);
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.SetIntersection)}"); foreach (var item in Flux.SetOps.SetIntersection(os1, os2)) System.Console.WriteLine(item);
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.SetUnion)}"); foreach (var item in Flux.SetOps.SetUnion(os1, os2)) System.Console.WriteLine(item);
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.SetUnionAll)}"); foreach (var item in Flux.SetOps.SetUnionAll(os1, os2)) System.Console.WriteLine(item);

      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.SetEquality)} = {Flux.SetOps.SetEquality(os1, os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.IsSetOverlapping)} = {Flux.SetOps.IsSetOverlapping(os1, os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.IsSetSubsetOf)} = {Flux.SetOps.IsSetSubsetOf(os1, os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.IsSetProperSubsetOf)} = {Flux.SetOps.IsSetProperSubsetOf(os1, os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.IsSetSupersetOf)} = {Flux.SetOps.IsSetSupersetOf(os1, os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.IsSetProperSupersetOf)} = {Flux.SetOps.IsSetProperSupersetOf(os1, os2)}");

      System.Console.WriteLine();

      return;

      //var game = new Flux.Model.GameOfLife.Game(32, 32, true, 0.5);
      //game.RunInConsole(200);

      //return;

      //System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => RegularForLoop(10, 0.1), 1));
      //System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => ParallelForLoop(10, 0.1), 1));
    }

    #region Serial vs. Parallel loops
    //static void RegularForLoop(int taskCount = 10, double taskLoad = 1)
    //{
    //  //var startDateTime = DateTime.Now;
    //  //System.Console.WriteLine($"{nameof(RegularForLoop)} started at {startDateTime}.");
    //  for (int i = 0; i < taskCount; i++)
    //  {
    //    ExpensiveTask(taskLoad);
    //    //var total = ExpensiveTask(taskLoad);
    //    //System.Console.WriteLine($"{nameof(ExpensiveTask)} {i} - {total}.");
    //  }
    //  //var endDateTime = DateTime.Now;
    //  //System.Console.WriteLine($"{nameof(RegularForLoop)} ended at {endDateTime}.");
    //  //var span = endDateTime - startDateTime;
    //  //System.Console.WriteLine($"{nameof(RegularForLoop)} executed in {span.TotalSeconds} seconds.");
    //  //System.Console.WriteLine();
    //}
    //static void ParallelForLoop(int taskCount = 10, double taskLoad = 1)
    //{
    //  //var startDateTime = DateTime.Now;
    //  System.Threading.Tasks.Parallel.For(0, taskCount, i =>
    //  {
    //    ExpensiveTask(taskLoad);
    //    //var total = ExpensiveTask(taskLoad);
    //    //System.Console.WriteLine($"{nameof(ExpensiveTask)} {i} - {total}.");
    //  });
    //  //var endDateTime = DateTime.Now;
    //  //System.Console.WriteLine($"{nameof(ParallelForLoop)} ended at {endDateTime}.");
    //  //var span = endDateTime - startDateTime;
    //  //System.Console.WriteLine($"{nameof(ParallelForLoop)} executed in {span.TotalSeconds} seconds");
    //  //System.Console.WriteLine();
    //}
    //static long ExpensiveTask(double taskLoad = 1)
    //{
    //  var total = 0L;
    //  for (var i = 1; i < int.MaxValue * taskLoad; i++)
    //    total += i;
    //  return total;
    //}
    #endregion Serial vs. Parallel loops

    #region Main method
    static void Main(string[] args)
    {
      var originalOutputEncoding = System.Console.OutputEncoding;

      try
      {
        System.Console.OutputEncoding = new System.Text.UnicodeEncoding(!System.BitConverter.IsLittleEndian, false);
        System.Console.WriteLine("Output encoding set to UTF-16");
      }
      catch
      {
        System.Console.OutputEncoding = System.Text.UnicodeEncoding.UTF8;
        System.Console.WriteLine("Output encoding set to UTF-8");
      }

      System.Console.WriteLine($"The console encoding is {System.Console.OutputEncoding.EncodingName} (code page {System.Console.OutputEncoding.CodePage})");
      System.Console.WriteLine();

      System.Console.WriteLine(Flux.Services.Performance.Measure(() => TimedMain(args), 1));

      Console.OutputEncoding = originalOutputEncoding;

      System.Console.WriteLine($"{System.Environment.NewLine}Press any key to exit...");
      System.Console.ReadKey();
    }
    #endregion Main method
  }
}
