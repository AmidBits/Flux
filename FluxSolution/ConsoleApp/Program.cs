using System;
using System.Linq;
using Flux;
using Flux.Numerics;
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
      //if (Zamplez.IsSupported) { Zamplez.Run(); return; }

      // At some point? https://github.com/jeffshrager/elizagen.org/blob/master/Other_Elizas/20120310ShragerNorthEliza.c64basic

      var value = (16).ToType(out System.Numerics.BigInteger bi);

      var quotient = System.Numerics.BigInteger.DivRem(value, 6, out var remainder);

      var mode = RoundingMode.HalfTowardZero;

      var nm = Flux.GenericMath.NearestMultiple(value, 6, false, mode, out var tz, out var afz);
      var eq = value == nm;
      var slo = nm == tz;
      var shi = nm == afz;

      Flux.BoundaryRounding<System.Numerics.BigInteger, System.Numerics.BigInteger>.MeasureDistanceToBoundaries(value, tz, afz, out System.Numerics.BigInteger dtz, out System.Numerics.BigInteger dafz);

      var td = dtz < dafz ? -1 : dafz < dtz ? 1 : 0;

      var tlo = dtz <= 3;
      var thi = dafz < 3;

      var loop = new Flux.Loops.AlternatingLoop<System.Numerics.BigInteger>(nm, 20, 6, shi ? Flux.Loops.AlternatingLoopDirection.TowardsMean : Flux.Loops.AlternatingLoopDirection.AwayFromMean);
      System.Console.WriteLine(string.Join(", ", loop.Take(20)));
      var br = Flux.BoundaryRounding<System.Numerics.BigInteger, System.Numerics.BigInteger>.Round(value, mode, tz, afz);
      System.Console.WriteLine(string.Join(", ", Flux.NumberSequences.PrimeNumber.GetClosestPotentialPrimes(value).Take(40)));



      var exp = "2.0*(-2-3)";
      //exp = "-3";

      var mt = new Flux.Text.MathTokenizer(false);

      var ts = mt.GetTokens(exp).ToList();

      for (var i = 0; i < ts.Count; i++)
      {
        if (ts[i] is MathTokenOperator mto && mto.Value == MathTokenOperator.SymbolSubtract)
        {
          if ((i == 0 || ts[i - 1] is not MathTokenNumber) && (i <= ts.Count - 1 && ts[i + 1] is MathTokenNumber mtn))
          {
            ts[i + 1] = mtn.GetNegated();
            ts.RemoveAt(i);
          }
        }
      }

      var npn = Flux.Text.MathTokenizer.GetTokensNPN(ts);
      var enpn = Flux.Text.MathTokenizer.EvaluateNPN(npn);

      var rpn = Flux.Text.MathTokenizer.GetTokensRPN(ts);
      var erpn = Flux.Text.MathTokenizer.EvaluateRPN(rpn);

      return;

      var sr = new System.IO.StringReader("Hello\u241F\"World,\r\n\"\u241EGoodbye\u241FWorld\u241D");

      var table = Flux.UnicodeData.ReadGroup(sr, out var read);

      var c1 = new Flux.Numerics.CartesianCoordinate2<double>(5, 15);
      var c2 = new Flux.Numerics.CartesianCoordinate2<double>(25, 55);

      // var li = Flux.Interpolation.LinearInterpolation<Flux.Numerics.CartesianCoordinate2<double>, double>.Interpolate(c1, c2, 0.5);

      var cc2 = Flux.Numerics.CartesianCoordinate2<double>.CreateChecked(10);
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
