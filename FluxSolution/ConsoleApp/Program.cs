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
using Flux.Formatting;
using Flux.Geometry;
using Flux.Interpolation;
using Flux.Quantities;
using Flux.Text;
using Microsoft.VisualBasic.FileIO;

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

      var ur = ((System.Text.Rune)'\0').GetUnicodeRange();

      return;

      var text = "Some things never change 😀123👨‍👩‍👧‍👦.";

      var rt = new Flux.Text.RuneTokenizer().GetTokens(text);
      System.Console.WriteLine(string.Join(System.Environment.NewLine, rt));
      var tet = new Flux.Text.TextElementTokenizer().GetTokens(text);
      System.Console.WriteLine(string.Join(System.Environment.NewLine, tet));

      foreach (var te in text.EnumerateTextElements())
        System.Console.WriteLine(te);


      return;

      var v4a = new System.Numerics.Vector4(1, 2, 3, 4.1f);

      System.Console.WriteLine(v4a.ToString());

      var v3 = new Flux.Numerics.CartesianCoordinate3<double>(1, 2, 3.1);
      System.Console.WriteLine(v3.ToString());

      var sc = v3.ToSphericalCoordinate();
      System.Console.WriteLine(sc.ToString());


      var s = new Flux.Numerics.CartesianCoordinate2<double>(2, 3);
      var t = new Flux.Numerics.CartesianCoordinate2<double>(9, 10);

      var (mx, my) = Flux.Numerics.CartesianCoordinate2<double>.Slope(s, t);

      var a = new int[] { 1, 2, 2, 3, 5 };
      var b = new int[] { 1, 2, 2, 3, 5 };

      var ha = a.ToHistogram(i => i, i => 1);
      var hb = b.ToHistogram(i => i, i => 1);

      var hab = new Flux.DataStructures.Histogram<int, int>();
      hab.AddRange(ha);
      hab.Add(2, 1);
      hab.AddRange(hb);

      var ccdfpr = hab.ComputeCdfPercentRank(2, 1.0);
      var cpmfp = hab.ComputePmfProbability(2, 1.0);

      var h2 = ha[2];
      var h4 = ha[4];

      var hv = ha.Keys;

      var pmf = ha.ToProbabilityMassFunction(1.0);

      var hp2 = ha.ComputePmfProbability(2, 1.0);
      var hp4 = ha.ComputePmfProbability(4, 1.0);

      var isnb = pmf.IsNormalized();

      pmf.Add(4, .2 / (.8));

      var isna = pmf.IsNormalized();

      pmf.Normalize();

      var pTotal = pmf.Total;

      var p2 = pmf.Pmf(2);
      var p4 = pmf.Pmf(4);

      var cdf = ha.ToCumulativeDistributionFunction(1.0);

      var hc0 = ha.ComputeCdfPercentRank(0, 1.0);
      var hc1 = ha.ComputeCdfPercentRank(1, 1.0);
      var hc2 = ha.ComputeCdfPercentRank(2, 1.0);
      var hc3 = ha.ComputeCdfPercentRank(3, 1.0);
      var hc4 = ha.ComputeCdfPercentRank(4, 1.0);
      var hc5 = ha.ComputeCdfPercentRank(5, 1.0);

      var c0 = cdf.Cdf(0);
      var c1 = cdf.Cdf(1);
      var c2 = cdf.Cdf(2);
      var c3 = cdf.Cdf(3);
      var c4 = cdf.Cdf(4);
      var c5 = cdf.Cdf(5);
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
