using System;
using System.Collections;
using System.Collections.Generic;
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
    private static void AmbTest()
    {
      var amb = new Flux.AmbOps.Amb();

      var set1 = amb.Choose("the", "that", "a");
      var set2 = amb.Choose("frog", "tramp", "thing");
      amb.Require(() => set1.Value.Last() == set2.Value[0]);
      var set3 = amb.Choose("walked", "hauled", "treaded", "grows");
      amb.Require(() => set2.Value.Last() == set3.Value[0]);
      var set4 = amb.Choose("slowly", "quickly");
      amb.RequireFinal(() => set3.Value.Last() == set4.Value[0]);

      System.Console.WriteLine($"{set1} {set2} {set3} {set4}");
      System.Console.Read();

      // problem from http://www.randomhacks.net/articles/2005/10/11/amb-operator
      amb = new Flux.AmbOps.Amb();

      var x = amb.Choose(1, 2, 3);
      var y = amb.Choose(4, 5, 6);
      amb.RequireFinal(() => x.Value + y.Value == 8);

      System.Console.WriteLine($"{x} + {y} = 8");
      System.Console.Read();
      System.Console.Read();
    }

    private static void TimedMain(string[] args)
    {
      var text = "ABC ABCDAB ABCDABCDABDE";
      var word = "AB";

      var l = Flux.Text.KnuthMorrisPratt.Search(word, text);

      return;

      var sarg = string.Join(' ', args);
      sarg = "--port 123 /f /a test";
      //foreach (var token in new Flux.Text.RuneTokenizer().GetTokens(sarg))
      //  System.Console.WriteLine(token);

      using var re = new Flux.Text.RuneEnumerator(new System.IO.StringReader(sarg));

      foreach (var q in re)
        System.Console.WriteLine(q);

      //var r1 = new Flux.Range<System.DateTime>(new System.DateTime(2019, 1, 1), new System.DateTime(2020, 6, 30));
      //var r2 = new Flux.Range<System.DateTime>(new System.DateTime(2021, 1, 1), new System.DateTime(2021, 12, 31));
      //var rd = Flux.Range<System.DateTime>.Difference(r1, r2);
      //var ri = Flux.Range<System.DateTime>.Intersect(r1, r2);
      //var rsd = Flux.Range<System.DateTime>.SymmetricDifference(r1, r2);
      //var ru = Flux.Range<System.DateTime>.Union(r1, r2);
      //return;

      var gc = Flux.CoordinateSystems.GeographicCoordinate.Tucson;
      var sp = gc.ToSphericalCoordinate();
      var cy = sp.ToCylindricalCoordinate();
      var ca = cy.ToCartesianCoordinate3();
      var cy2 = ca.ToCylindricalCoordinate();
      var sp2 = cy2.ToSphericalCoordinate();
      var gc2 = sp2.ToGeographicCoordinate();

      //var cc = new Flux.CartesianCoord(10, 15, 20);

      //var cccy = cc.ToCylindricalCoord();
      //var cysc = cccy.ToSphericalCoord();
      //var cycc = cccy.ToCartesianCoord();

      //var ccsc = cc.ToSphericalCoord();
      //var sccc = ccsc.ToCartesianCoord();
      //var sccy = ccsc.ToCylindricalCoord();

      return;

      var game = new Flux.Model.GameOfLife.Game(32, 32, true, 0.5);
      var cv = new Flux.Model.GameOfLife.Console(game);
      cv.Run(100);
      return;

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
