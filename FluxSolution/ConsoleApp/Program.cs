using System;
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
    private static void TimedMain(string[] args)
    {
      var p = new Flux.Geometry.Point2(4, 3);

      for (var r = 0; r < 8; r++)
      {
        for (var c = 0; c < 8; c++)
        {
          System.Console.SetCursorPosition(c, r);
          System.Console.Write(c == p.X && r == p.Y ? '=' : '-');
        }
      }

      foreach (var m in Flux.Model.Chess.GetMovesOfKnight(p))
      {
        System.Console.SetCursorPosition(m.X, m.Y);
        System.Console.Write('+');
      }

      System.Console.SetCursorPosition(0, 10);

      return;
      //var seq = new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29 };

      //System.Console.WriteLine(seq.Extrema(seq => seq));
      //System.Console.WriteLine(seq.ExtremaClosestToKey(seq => seq, 13));

      //return;

      //var text = "ABC ABCDAB ABCDABCDABDE";
      //var word = "AB";

      //var l = Flux.Text.KnuthMorrisPratt.Search(word, text);

      //return;

      //var sarg = string.Join(' ', args);
      //sarg = "--port 123 /f /a test";
      ////foreach (var token in new Flux.Text.RuneTokenizer().GetTokens(sarg))
      ////  System.Console.WriteLine(token);

      //using var re = new Flux.Text.RuneEnumerator(new System.IO.StringReader(sarg));

      //foreach (var q in re)
      //  System.Console.WriteLine(q);

      //var r1 = new Flux.Range<System.DateTime>(new System.DateTime(2019, 1, 1), new System.DateTime(2020, 6, 30));
      //var r2 = new Flux.Range<System.DateTime>(new System.DateTime(2021, 1, 1), new System.DateTime(2021, 12, 31));
      //var rd = Flux.Range<System.DateTime>.Difference(r1, r2);
      //var ri = Flux.Range<System.DateTime>.Intersect(r1, r2);
      //var rsd = Flux.Range<System.DateTime>.SymmetricDifference(r1, r2);
      //var ru = Flux.Range<System.DateTime>.Union(r1, r2);
      //return;

      //var gc = Flux.CoordinateSystems.GeographicCoordinate.Tucson;
      //var sp = gc.ToSphericalCoordinate();
      //var cy = sp.ToCylindricalCoordinate();
      //var ca = cy.ToCartesianCoordinate3();
      //var cy2 = ca.ToCylindricalCoordinate();
      //var sp2 = cy2.ToSphericalCoordinate();
      //var gc2 = sp2.ToGeographicCoordinate();

      //return;

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
