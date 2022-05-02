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

      var intervals = new double[] { 18, 25, 35, 45, 55, 65 };

      var hbi1 = new Flux.Randomization.Xoshiro256SS().GetRandomInt32s(0, 100).Take(1000).CreateClosedOpen(intervals, i => i, (lo, hi) => $"[{lo},{hi})", i => 1, true);
      System.Console.WriteLine(hbi1);

      //var hbi = new Flux.DataStructures.Statistics.HistogramByInterval(18, 25, 35, 45, 55, 65);
      ////var hbi = new Flux.DataStructures.Statistics.HistogramByInterval(0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100);

      var a = new int[] { 13, 12, 11, 8, 4, 3, 2, 1, 1, 1 };
      a = a.Reverse().ToArray();
      //a = new int[] { 1, 2, 3, 6, 6, 6, 7, 8, 9 };
      //a = new int[] { 1, 2, 3, 4, 4, 5, 6, 7, 8 };
      a = new int[] { 2, 4, 5, 6, 6, 7, 9, 12, 14, 15, 18, 19, 22, 24, 26, 28 };
      //a = new int[] { 3, 6, 7, 8, 8, 10, 13, 15, 16, 20 };
      a = new int[] { 15, 20, 35, 40, 50 };

      //      var v1 = Flux.Enumerable.PercentRankMatlab(2, 5);

      var pmf = a.ProbabilityMassFunction(item => item, item => 1, 1);
      var cmf = a.CumulativeMassFunction(a => a, a => 1, 1);

      var pr1 = a.ToHistogram(out var sum).PercentRank();
      var prl = a.ToHistogram(out var sum1).PercentileRank(sum1);
      var pr2 = a.PercentRank(a => a, a => 1, 1);

      var percentile = 0.40;

      var prexc = Flux.Percentiles.PercentRank(percentile, a.Length, Percentiles.LerpVariant.ExcelExc);
      var pvexc = a.Select(i => (double)i).PercentileValue(percentile, Percentiles.LerpVariant.ExcelExc);

      var princ = Flux.Percentiles.PercentRank(percentile, a.Length, Percentiles.LerpVariant.ExcelInc);
      var pvinc = a.Select(i => (double)i).PercentileValue(percentile, Percentiles.LerpVariant.ExcelInc);

      //var prmlb = a.Select(i => (double)i).PercentileRanksMatlab();
      //var pvmlb = a.Select(i => (double)i).PercentileValuesMatlab(percentile);

      var pon = Flux.Percentiles.PercentNearestOrdinalRank(percentile, a.Length);

      //foreach (var av in new Flux.Randomization.Xoshiro256SS().GetRandomInt32s(0, 100).Take(100))
      //  hbi.AddValue(av);

      var hbi2 = new Flux.Randomization.Xoshiro256SS().GetRandomInt32s(0, 20).Take(20).CreateDegenerate(i => (double)i, i => 1);
      System.Console.WriteLine(hbi2);

      //var hg = new Histogram<int>();
      //hg.Add(a, (e, i) => e, (e, i) => 1);

      //var h = a.ToHistogram(out var sof); 
      //System.Console.WriteLine($"{string.Join(System.Environment.NewLine, h)} ({sof})");
      //System.Console.WriteLine();

      //var pmf = h.ProbabilityMassFunction(sof);
      //System.Console.WriteLine($"PMF: {string.Join(System.Environment.NewLine, pmf)}");
      //System.Console.WriteLine();

      //var cdf = h.CumulativeMassFunction(sof);
      //System.Console.WriteLine($"CDF: {string.Join(System.Environment.NewLine, cdf)}");
      //System.Console.WriteLine();

      //var pr = h.PercentRank(sof);
      //System.Console.WriteLine($" PR: {string.Join(System.Environment.NewLine, pr)}");
      //System.Console.WriteLine();

      //var pl = h.PercentileRank(sof);
      //System.Console.WriteLine($" PL: {string.Join(System.Environment.NewLine, pl)}");
      //System.Console.WriteLine();
      //return;

      //h.Add(a.Select(i => (double)i));

      //var lower = 0;
      //var middle = 0;
      //var upper = 0;

      // var values = new int[] { 8, 9, 10, 11, 11, 11, 11, 12, 12, 12, 13 };
      // var values = new int[] { 2, 3, 4, 4, 4, 4, 5, 5, 5, 5, 5 };
      // var values = new int[] { 2, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5 };
      //var values = a;// new int[] { 13, 17, 17, 18, 19, 19, 19, 21, 21, 23, 24 };

      //var n = values.Length;
      //var index = 0.0;
      //foreach (var i in values)
      //{
      //  //var pr = ((values.Count(v => v < i) + (.5 * values.Count(v => v == i))) / n);
      //  var pr = index / (index + (values.Length - index - 1));
      //  if (pr < .27)
      //    lower += 1;
      //  else if (pr > .73)
      //    upper += 1;
      //  else
      //    middle += 1;
      //  index++;
      //}

      //System.Console.WriteLine("Upper: " + upper);
      //System.Console.WriteLine("Middle: " + middle);
      //System.Console.WriteLine("Lower: " + lower);

      //System.Console.WriteLine(string.Join(System.Environment.NewLine, a.ToHistogram((e, i) => e, out var sum1).CumulativeMassFunction(sum1)));
      //System.Console.WriteLine(string.Join(System.Environment.NewLine, a.ToHistogram((e, i) => e, out var sum2).PercentRank(sum2)));
      //System.Console.WriteLine(string.Join(System.Environment.NewLine, a.ToHistogram((e, i) => e, out var sum3).PercentileRank(sum3)));
      ////return;

      //var b = a.Select((e, i) => System.Collections.Generic.KeyValuePair.Create(e, (double)i / (double)(i + (a.Length - i - 1)))).ToArray();
      //System.Console.WriteLine(string.Join(System.Environment.NewLine, b));

      //var e = b.ExtremaClosestToKey(t => t.Key, 5);
      //System.Console.WriteLine(e);

      //var ipx = LinearInterpolation.ImputeUnit(e.elementLt.Key, e.elementGt.Key, 5);

      //var ip = LinearInterpolation.Interpolate(5, e.elementGt.Value, e.elementLt.Value);
      //System.Console.WriteLine(ip);


      //System.Console.WriteLine(string.Join(System.Environment.NewLine, a.ToHistogram((e, i) => e, out var sum).CumulativeMassFunction(sum)));
      //return;

      //var enumElements = Flux.AssemblyInfo.Flux.Assembly.GetTypes().Where(t => t.IsEnum).ToArray();
      //foreach (var enumType in enumElements)
      //  System.Console.WriteLine(enumType.FullName);
      //return;

      //System.Console.Write($"It's a ");
      //Flux.Console.WriteError($"{nameof(Flux.Console.WriteError)}");
      //System.Console.Write($" and some ");
      //Flux.Console.WriteInformation($"{nameof(Flux.Console.WriteInformation)}");
      //System.Console.Write($" with a bit of ");
      //Flux.Console.WriteSuccess($"{nameof(Flux.Console.WriteSuccess)}");
      //System.Console.Write($" as well as a ");
      //Flux.Console.WriteWarning($"{nameof(Flux.Console.WriteWarning)}");
      //System.Console.WriteLine($".");

      //foreach (System.ConsoleColor color in System.Enum.GetValues(typeof(System.ConsoleColor)))
      //{
      //  System.Console.ForegroundColor = color;
      //  System.Console.WriteLine(color.ToString());
      //  System.Console.ResetColor();
      //}
    }

    private static void Main(string[] args)
    {
      var originalOutputEncoding = SetEncoding();

      SetSize(0.9);

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
