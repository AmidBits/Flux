using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Xml.Schema;
using Flux;
using Flux.ApproximateEquality;
using Flux.Geometry;
using Flux.Interpolation;

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

      var sbc = new Flux.SpanBuilder<char>();
      sbc.Append("hé\u0142\u0142ö");
      sbc.Append(" 35 \U0001F469\U0001F3FD\u200D\U0001F692 ");
      sbc.Append("wø\u024D\u0142\u0111");
      sbc.Append('.');
      sbc.AsSpan().ToCamelCase();
      sbc.AsSpan().FromCamelCase();
      sbc.InsertOrdinalIndicatorSuffix();
      sbc.MakeNumbersFixedLength(10, '0');
      var sbr = sbc.ToSpanBuilderRune();
      var s = sbr.ToString();
      var s1 = s.RemoveUnicodeMarks();
      var s2 = s1.AsSpan().ReplaceUnicodeLatinStrokes();
      var sh = s2.Shuffle(null);

      var sc = s.AsSpan();
      var sr = sc.ToSpanRune();
      var ste = sc.ToSpanTextElement();
      var sr1 = ste.ToSpanRune();
      var sc1 = sr.ToSpanChar();

      var sb = new Flux.SpanBuilder<char>();
      for (var i = 0; i < 1000; i++)
        sbc.Append(s);

      //var e = rosr.EnumerateChars();

      //foreach (var t in e)
      //  System.Console.WriteLine(t);

      //System.Console.WriteLine();
      return;

      // Compute quantiles:
      {
        var aav = new int[][] { new int[] { 3, 6, 7, 8, 8, 10, 13, 15, 16, 20 }, new int[] { 3, 6, 7, 8, 8, 9, 10, 13, 15, 16, 20 } };

        foreach (var av in aav)
        {
          System.Console.WriteLine($"Sequence: [{string.Join(", ", av)}]");



          var htt = Flux.DataStructures.Histogram<int, int>.Create(av, k => k, f => 1);

          var h = Flux.DataStructures.Histogram<int, int>.Create(av, k => k, v => 1);
          System.Console.WriteLine($"HIST:{System.Environment.NewLine}{h.ToConsoleString()}{System.Environment.NewLine}");

          var v = 13;

          var pmf = Flux.DataStructures.ProbabilityMassFunction<int, double>.Create(h, 1.5d);
          var pmfv = h.ToPmfProbability(v, 1d);
          System.Console.WriteLine($"PMF:{System.Environment.NewLine}{pmf.ToConsoleString()}{System.Environment.NewLine}PV={pmfv}");

          var cmf = Flux.DataStructures.CumulativeMassFunction<int, double>.Create(h, 1.5d);
          var cmfv = h.ToCmfPercentRank(v, 1d);
          System.Console.WriteLine($"CMF:{System.Environment.NewLine}{cmf.ToConsoleString()}{System.Environment.NewLine}CV={cmfv}");

          //continue;

          foreach (QuantileAlgorithm a in System.Enum.GetValues<QuantileAlgorithm>())
          {
            var ac = av.Length;

            var qr = (ac.ComputeQuantileRank(0d, a), ac.ComputeQuantileRank(0.25, a), ac.ComputeQuantileRank(0.50, a), ac.ComputeQuantileRank(0.75, a), ac.ComputeQuantileRank(1d, a));
            var qv = (av.EstimateQuantileValue(0d, a), av.EstimateQuantileValue(0.25, a), av.EstimateQuantileValue(0.50, a), av.EstimateQuantileValue(0.75, a), av.EstimateQuantileValue(1d, a));
            //var qv = (av.EstimateQuantileValue(0.25, a), av.EstimateQuantileValue(0.50, a), av.EstimateQuantileValue(0.75, a));

            System.Console.WriteLine($"{a} : qR = {qr}, qV = {qv}");
          }

          System.Console.WriteLine();
        }

        return;
      }



      //// Compute roundings:
      //{
      //  var v = -1.5;
      //  var c = v.Round(RoundingMode.Ceiling);
      //  var e = v.Round(RoundingMode.Envelop);
      //  var f = v.Round(RoundingMode.Floor);
      //  var t = v.Round(RoundingMode.Truncate);
      //  var hafz = v.Round(RoundingMode.HalfAwayFromZero);
      //  var heven = v.Round(RoundingMode.HalfToEven);
      //  var hninf = v.Round(RoundingMode.HalfToNegativeInfinity);
      //  var hodd = v.Round(RoundingMode.HalfToOdd);
      //  var hpinf = v.Round(RoundingMode.HalfToPositiveInfinity);
      //  var htz = v.Round(RoundingMode.HalfTowardZero);
      //}
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
