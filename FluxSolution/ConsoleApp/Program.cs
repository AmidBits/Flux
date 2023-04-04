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
      if (Zamplez.IsSupported) { Zamplez.Run(); return; }

      // At some point? https://github.com/jeffshrager/elizagen.org/blob/master/Other_Elizas/20120310ShragerNorthEliza.c64basic

      var x = 1;
      var y = 2;

      var hgc = Flux.Geometry.HexagonGeometry.FromCircumradius(x);
      var hgi = Flux.Geometry.HexagonGeometry.FromInradius(x);

      var cg = new Flux.Geometry.CircleGeometry(x);

      var eg = new Flux.Geometry.EllipseGeometry(x, y);

      var value = (17).ToType(out System.Numerics.BigInteger bi);

      var nppm = Flux.NumberSequences.PrimeNumber.GetNearestPotentialPrimeMultiple(value, RoundingMode.HalfAwayFromZero, out var nppmo);

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



      var exp = "2.0*(-2--3)";

      var t = new Flux.Text.MathTokenizer(false);
      var ts = t.GetTokens(exp);
      var tss = string.Join(" ", ts);


      var sr = new System.IO.StringReader("Hello\u241F\"World,\r\n\"\u241EGoodbye\u241FWorld\u241D");

      var table = Flux.Unicode.Utility.ReadGroup(sr, out var read);
      var text = Flux.Unicode.Utility.WriteGroup(table);
      var table2 = Flux.Unicode.Utility.ReadGroup(new System.IO.StringReader(text), out var rd);
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
