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
      var rng = new System.Random();

      var bi = new Flux.Numerics.FibonacciNumber().GetNumberSequence().ElementAt(300);
      var bip = bi + 1;

      var value = rng.NextBigInteger(bip);

      while (value >= 0 && value <= bi)
      {
        if (value == bi || value <= 1000)
          System.Console.WriteLine(value);

        value = rng.NextBigInteger(bip);
      }


      Draw(Flux.CoordinateSystems.GeographicCoordinate.TucsonAzUsa);
      Draw(Flux.CoordinateSystems.GeographicCoordinate.MadridSpain);
      Draw(Flux.CoordinateSystems.GeographicCoordinate.PhoenixAzUsa);
      Draw(Flux.CoordinateSystems.GeographicCoordinate.TakapauNewZealand);

      static void Draw(Flux.CoordinateSystems.GeographicCoordinate coord)
      {
        var gc1 = coord;
        System.Console.WriteLine(gc1);
        var sp1 = gc1.ToSphericalCoordinate();
        System.Console.WriteLine(sp1);
        var cy1 = sp1.ToCylindricalCoordinate();
        System.Console.WriteLine(cy1);
        var ca1 = cy1.ToCartesianCoordinate3();
        System.Console.WriteLine(ca1);
        var cy2 = ca1.ToCylindricalCoordinate();
        System.Console.WriteLine(cy2);
        var sp2 = cy2.ToSphericalCoordinate();
        System.Console.WriteLine(sp2);
        var gc2 = sp2.ToGeographicCoordinate();
        System.Console.WriteLine(gc2);
        System.Console.WriteLine();
      }

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
