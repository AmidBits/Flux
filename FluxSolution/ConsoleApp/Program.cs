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
  /*
  public class HorseGame
  {
    public enum LegType
    {
      Front,
      Back,
    }

    [System.Flags]
    public enum Attributes
    {
      Speed,
    }

    System.Collections.Generic.Dictionary<Attributes, double> m_baseStats = new System.Collections.Generic.Dictionary<Attributes, double>();

    System.Collections.Generic.Dictionary<Attributes, double> m_breedStats = new System.Collections.Generic.Dictionary<Attributes, double>();

    System.Collections.Generic.Dictionary<Attributes, double> m_gradeStats = new System.Collections.Generic.Dictionary<Attributes, double>();

    System.Collections.Generic.Dictionary<Attributes, double> m_trainingStats = new System.Collections.Generic.Dictionary<Attributes, double>();

  }
  */
  public class Program
  {
    private static void TimedMain(string[] args)
    {
      //if (args.Length is var argsLength && argsLength > 0) System.Console.WriteLine($"Args ({argsLength}):{System.Environment.NewLine}{string.Join(System.Environment.NewLine, System.Linq.Enumerable.Select(args, s => $"\"{s}\""))}");
      //if (Flux.Zamplez.IsSupported) { Flux.Zamplez.Run(); return; }

      for (var index = 253.ToBigInteger(); index < 258; index++)
      //for (var index = -10; index < 2; index++)
      {
        System.Console.WriteLine($"{index:D2} : {index.ToString("X4")}");

        System.Console.WriteLine($"{index:D2} : {Flux.BitOps.BitLength(index)} (BitLength)");
        System.Console.WriteLine($"{index:D2} : {Flux.BitOps.FoldLeft(index)} (FoldLeft) {Flux.BitOps.FoldLeft(index).ToRadixString(2)}");
        System.Console.WriteLine($"{index:D2} : {Flux.BitOps.FoldRight(index)} (FoldRight) {Flux.BitOps.FoldRight(index).ToRadixString(2)}");
        System.Console.WriteLine($"{index:D2} : {Flux.BitOps.IsPowerOf2(index)} (IsPowerOf2)");
        System.Console.WriteLine($"{index:D2} : {Flux.BitOps.LeadingZeroCount(index)} (LeadingZeroCount)");
        System.Console.WriteLine($"{index:D2} : {Flux.BitOps.LeastSignificant1Bit(index)} (LeastSignificant1Bit)");
        System.Console.WriteLine($"{index:D2} : {Flux.BitOps.PowerOf2BitIndex(Flux.BitOps.LeastSignificant1Bit(index))} (LeastSignificant1-BitIndex)");
        System.Console.WriteLine($"{index:D2} : {Flux.BitOps.Log2(index)} (Log2)");
        System.Console.WriteLine($"{index:D2} : {Flux.BitOps.MostSignificant1Bit(index)} (MostSignificant1Bit)");
        System.Console.WriteLine($"{index:D2} : {Flux.BitOps.PowerOf2BitIndex(Flux.BitOps.MostSignificant1Bit(index))} (MostSignificant1-BitIndex)");
        System.Console.WriteLine($"{index:D2} : {Flux.BitOps.PopCount(index)} (PopCount)");
        System.Console.WriteLine($"{index:D2} : {Flux.BitOps.RoundDownToPowerOf2(index, true)} (SmallerPowerOf2-Proper)");
        System.Console.WriteLine($"{index:D2} : {Flux.BitOps.RoundDownToPowerOf2(index, false)} (SmallerPowerOf2)");
        System.Console.WriteLine($"{index:D2} : {Flux.BitOps.RoundToNearestPowerOf2(index, true, out var gtp, out var ltp)} (GT:{gtp}, LT:{ltp}) (RoundToNearestPowerOf2-Proper)");
        System.Console.WriteLine($"{index:D2} : {Flux.BitOps.RoundToNearestPowerOf2(index, false, out var gt, out var lt)} (GT:{gt}, LT:{lt}) (RoundToNearestPowerOf2)");
        System.Console.WriteLine($"{index:D2} : {Flux.BitOps.RoundUpToPowerOf2(index, true)} (LargerPowerOf2-Proper)");
        System.Console.WriteLine($"{index:D2} : {Flux.BitOps.RoundUpToPowerOf2(index, false)} (LargerPowerOf2)");
        System.Console.WriteLine($"{index:D2} : {Flux.BitOps.TrailingZeroCount(index)} (TrailingZeroCount)");
        System.Console.WriteLine();
      }



      var xx = new Flux.Resources.DotNet.FxSequence(TimeZoneInfo.GetSystemTimeZones());

      var tz = TimeZoneInfo.GetSystemTimeZones();

      var t = tz.GetType();

      return;

      var weights = new int[] { 23, 26, 20, 18, 32, 27, 29, 26, 30, 27 };
      var values = new int[] { 505, 352, 458, 220, 354, 414, 498, 545, 473, 543 };

      var ks = new Flux.Model.Knapsack(67, 10, weights, values);

      var dg = ks.ComputeDynamicGrid(out var maxValue);
      //dg = dg.Remove(0, 0);
      //dg = dg.Remove(1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 10, 11, 12, 13, 14, 15, 16, 17);
      //dg.FlipInPlace(0);
      System.Console.WriteLine(string.Join(System.Environment.NewLine, dg.ToConsoleStrings()));
      System.Console.WriteLine();

      var rg = ks.ComputeRecursiveGrid(out maxValue, true);
      //rg = rg.Remove(0, 0);
      //rg = rg.Remove(1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19);
      //rg.FlipInPlace(0);
      System.Console.WriteLine(string.Join(System.Environment.NewLine, rg.ToConsoleStrings()));
      System.Console.WriteLine();

      return;

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
      var newValues1 = oldValues.PermuteAlgorithmL().ToArray();
      var newValues2 = oldValues.PermuteHeapsAlgorithm().ToArray();

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
