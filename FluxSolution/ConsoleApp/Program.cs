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
    private static void TimedMain(string[] args)
    {
      //const string csF = "F";
      //const string csP = "P";
      //const string csT = "T";
      //const string csY = "Y";

      //var ls = new System.Collections.Generic.List<string>();
      //ls.Add(csF);
      //ls.Add(csP);
      //ls.Add(csT);
      //ls.Add(csY);

      //var ds = new System.Collections.Generic.Dictionary<System.Collections.Generic.KeyValuePair<string, string>, double>();
      //ds.Add(new KeyValuePair<string, string>(csF, csT), 257);
      //ds.Add(new KeyValuePair<string, string>(csT, csF), 257);
      //ds.Add(new KeyValuePair<string, string>(csP, csF), 145);
      //ds.Add(new KeyValuePair<string, string>(csF, csP), 145);
      //ds.Add(new KeyValuePair<string, string>(csT, csP), 113);
      //ds.Add(new KeyValuePair<string, string>(csP, csT), 113);
      //ds.Add(new KeyValuePair<string, string>(csT, csY), 238);
      //ds.Add(new KeyValuePair<string, string>(csY, csT), 238);
      //ds.Add(new KeyValuePair<string, string>(csP, csY), 185);
      //ds.Add(new KeyValuePair<string, string>(csY, csP), 185);
      //ds.Add(new KeyValuePair<string, string>(csF, csY), 318);
      //ds.Add(new KeyValuePair<string, string>(csY, csF), 318);

      //static IEnumerable<System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>> Tsp(string l, System.Collections.Generic.List<string> v, System.Collections.Generic.List<string> ls, System.Collections.Generic.Dictionary<System.Collections.Generic.KeyValuePair<string, string>, double> ds)
      //{
      //  var v1 = v.ToList();
      //  v1.Add(l);

      //  var r = ls.Where(l2 => !v1.Contains(l2)).ToList();

      //  if (r.Any())
      //  {
      //    System.Console.WriteLine(r.Count);
      //    var last = string.Empty;
      //    foreach (var l1 in r)
      //    {
      //      var rs = new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>();
      //      System.Console.WriteLine(new KeyValuePair<string, string>(l, l1));
      //      foreach (var dics in Tsp(l1, v1, ls, ds))
      //        foreach (var kvp in dics)
      //          System.Console.WriteLine(new KeyValuePair<string, string>(kvp.Key, kvp.Value));
      //      yield return rs;
      //      last = l1;
      //    }
      //    //         if (r.Count == 1)
      //    //         yield return new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>() { new KeyValuePair<string, string>(last, v1.First()) };
      //  }
      //  else
      //    yield return new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>();
      //}

      //foreach (var list in Tsp(csT, new System.Collections.Generic.List<string>(), ls, ds))
      //{
      //  foreach (var kvp in list)
      //    System.Console.WriteLine(kvp);

      //  System.Console.WriteLine();
      //}

      //var ints = new int[] { 0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7, 15 };

      //var lis = new Flux.Metrical.LongestIncreasingSubsequence<int>().GetLongestIncreasingSubsequence(ints.ToReadOnlySpan());

      //string X = "ABCBDAB", Y = "BDCABA";

      //var scs = new Flux.Metrical.ShortestCommonSupersequence<char>();

      //var l = scs.GetList(X, Y, out var m);
      //return;
      //var r1 = new Flux.Range<System.DateTime>(new System.DateTime(2021, 1, 1), new System.DateTime(2021, 8, 31));
      //var r2 = new Flux.Range<System.DateTime>(new System.DateTime(2021, 4, 1), new System.DateTime(2021, 12, 31));
      //var rd = Flux.Range<System.DateTime>.Difference(r1, r2);
      //var ri = Flux.Range<System.DateTime>.Intersect(r1, r2);
      //var rsd = Flux.Range<System.DateTime>.SymmetricDifference(r1, r2);
      //var ru = Flux.Range<System.DateTime>.Union(r1, r2);
      //return;

      var gc = Flux.CoordinateSystems.GeographicCoordinate.Tucson;
      System.Console.WriteLine(gc);
      var sp = gc.ToSphericalCoordinate();
      System.Console.WriteLine(sp);
      var cy = sp.ToCylindricalCoordinate();
      System.Console.WriteLine(cy);
      var ca = cy.ToCartesianCoordinate3();
      System.Console.WriteLine(ca);
      var cy2 = ca.ToCylindricalCoordinate();
      System.Console.WriteLine(cy2);
      var sp2 = cy2.ToSphericalCoordinate();
      System.Console.WriteLine(sp2);
      var gc2 = sp2.ToGeographicCoordinate();
      System.Console.WriteLine(gc2);
      return;

      //var game = new Flux.Model.GameOfLife.Game(32, 32, true, 0.5);
      //var cv = new Flux.Model.GameOfLife.Console(game);
      //cv.Run(100);
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
