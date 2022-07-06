using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Flux;

// C# Interactive commands:
// #r "System.Runtime"
// #r "System.Runtime.Numerics"
// #r "C:\Users\Rob\source\repos\Flux\FluxSolution\BaseLibrary\bin\Debug\net5.0\BaseLibrary.dll"

namespace ConsoleApp
{
  public class Program
  {
    private static void TimedMain(string[] args)
    {
      //if (args.Length is var argsLength && argsLength > 0) System.Console.WriteLine($"Args ({argsLength}):{System.Environment.NewLine}{string.Join(System.Environment.NewLine, System.Linq.Enumerable.Select(args, s => $"\"{s}\""))}");
      //if (Flux.Zamplez.IsSupported) { Flux.Zamplez.Run(); return; }

      var x = new double[] { 19, 23, 28, 34, 36, 39, 41, 47, 48, 52, 58, 59, 61, 65, 68 };
      //var y = Percentiles.PercentileValue(x.Select(x => (double)x), 0.80, Percentiles.LerpVariant.ExcelExc);
      //var z = Percentiles.PercentileValue(x.Select(x => (double)x), 0.80, Percentiles.LerpVariant.ExcelInc);
      var y = Percentiles.PercentileValue(x, .49, Percentiles.LerpVariant.ExcelExc);
      var ze = Percentiles.PercentRank(1.0, x.Length, Percentiles.LerpVariant.ExcelExc);
      var w = Percentiles.PercentNearestOrdinalRank(1, 40);

      var eq = Quantiles.EmpiricalDistributionFunction(x, 50);

      x.Average();
      //args = null;
      var e = args.SubstituteIfEmpty(new string[] { "Hello", "World" });

      var a = e.ToArray();

      var oldValues = new char[] { 'A', 'B', 'C' };// new System.ValueTuple<System.Globalization.CompareOptions, string>[] { (System.Globalization.CompareOptions.IgnoreCase, "Case"), (System.Globalization.CompareOptions.IgnoreNonSpace, "NonSpace"), (System.Globalization.CompareOptions.IgnoreSymbols, "Symbols"), (System.Globalization.CompareOptions.IgnoreWidth, "Width") };
      var newValues = oldValues.PermuteHeapsAlgorithm().ToArray();

      var grid = new Flux.Model.Grid<System.Text.Rune>(11, 11);

      grid[3, 8] = (System.Text.Rune)'X';
      grid[4, 1] = (System.Text.Rune)'Y';
      grid[7, 6] = (System.Text.Rune)'Z';

      foreach (var kvp in grid)
        System.Console.WriteLine(kvp);

      System.Console.WriteLine(grid.ToConsoleBlock(v => v == default ? (System.Text.Rune)'\u00B7' : v));

      for (var i = 0; i <= 1000000; i++)
        System.Console.WriteLine($"{i} = {Flux.Model.BattleShip.Fleet.ProximityProbability(i):N9}%");
    }

    private static void Main(string[] args)
    {
      var originalOutputEncoding = SetEncoding();

      SetSize(0.75);

      System.Console.WriteLine(Flux.Services.Performance.Measure(() => TimedMain(args), 1));

      ResetEncoding(originalOutputEncoding);

      System.Console.WriteLine($"{System.Environment.NewLine}Press any key to exit...");
      System.Console.ReadKey();

      #region Support functions
      static void ResetEncoding(System.Text.Encoding originalOutputEncoding)
      {
        System.Console.OutputEncoding = originalOutputEncoding;
      }
      static System.Text.Encoding SetEncoding()
      {
        var originalOutputEncoding = System.Console.OutputEncoding;

        try { System.Console.OutputEncoding = new System.Text.UnicodeEncoding(!System.BitConverter.IsLittleEndian, false); }
        catch { System.Console.OutputEncoding = System.Text.UnicodeEncoding.UTF8; }

        System.Console.ForegroundColor = System.ConsoleColor.Blue;
        System.Console.WriteLine($"The console encoding is {System.Console.OutputEncoding.EncodingName} {System.Console.OutputEncoding.HeaderName.ToUpper()} (code page {System.Console.OutputEncoding.CodePage})");
        System.Console.ResetColor();

        return originalOutputEncoding;
      }
      static void SetSize(double percentOfLargestWindowSize)
      {
        if (System.OperatingSystem.IsWindows())
        {
          if (percentOfLargestWindowSize < 0.1 || percentOfLargestWindowSize >= 1.0) throw new System.ArgumentOutOfRangeException(nameof(percentOfLargestWindowSize));

          var width = System.Math.Min(System.Math.Min(System.Convert.ToInt32(System.Console.LargestWindowWidth * percentOfLargestWindowSize), System.Console.LargestWindowWidth), short.MaxValue);
          var height = System.Math.Min(System.Math.Min(System.Convert.ToInt32(System.Console.LargestWindowHeight * percentOfLargestWindowSize), System.Console.LargestWindowHeight), short.MaxValue);

          System.Console.SetWindowSize(width, height);
        }
      }
      #endregion Support functions
    }
  }
}
