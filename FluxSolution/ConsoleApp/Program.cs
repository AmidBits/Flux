using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Flux;
using Flux.ApproximateEquality;
using Flux.Geometry;
using Flux.Interpolation;
using Flux.Sorting;

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

      // if (Flux.Zamplez.IsSupported) { Flux.Zamplez.RunStatistics(); return; }

      var st = new double[] { 6, 7, 15, 36, 39, 40, 41, 42, 43, 47, 49 };

      var stq61 = st.EstimateQuantile(0.25, QuantileAlgorithm.R6);
      var stq62 = st.EstimateQuantile(0.50, QuantileAlgorithm.R6);
      var stq63 = st.EstimateQuantile(0.75, QuantileAlgorithm.R6);

      var stq71 = st.EstimateQuantile(0.25, QuantileAlgorithm.R7);
      var stq72 = st.EstimateQuantile(0.50, QuantileAlgorithm.R7);
      var stq73 = st.EstimateQuantile(0.75, QuantileAlgorithm.R7);

      var ai = new int[] { 4, 18, 26, 31, 43, 57, 69, 72, 85 };

      var ngram = ai.PartitionNgram(5, (e, i) => e.ToArray()).ToArray();
      var windowed = ai.PartitionWindowed(5, 1, false, (e) => e.ToArray()).ToArray();
      var pt2 = ai.PartitionTuple2(false, (e1, e2, i) => (e1, e2)).ToArray();

      //var v = -1.5;
      //var c = v.Round(RoundingMode.Ceiling);
      //var e = v.Round(RoundingMode.Envelop);
      //var f = v.Round(RoundingMode.Floor);
      //var t = v.Round(RoundingMode.Truncate);
      //var hafz = v.Round(RoundingMode.HalfAwayFromZero);
      //var heven = v.Round(RoundingMode.HalfToEven);
      //var hninf = v.Round(RoundingMode.HalfToNegativeInfinity);
      //var hodd = v.Round(RoundingMode.HalfToOdd);
      //var hpinf = v.Round(RoundingMode.HalfToPositiveInfinity);
      //var htz = v.Round(RoundingMode.HalfTowardZero);
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
