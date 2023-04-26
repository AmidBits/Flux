using System;
using System.Linq;
using Flux;
using Flux.Formatting;
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
    private static void TimedMain(string[] _)
    {
      //if (args.Length is var argsLength && argsLength > 0) System.Console.WriteLine($"Args ({argsLength}):{System.Environment.NewLine}{string.Join(System.Environment.NewLine, System.Linq.Enumerable.Select(args, s => $"\"{s}\""))}");
      //if (Zamplez.IsSupported) { Zamplez.Run(); return; }

      // At some point? https://github.com/jeffshrager/elizagen.org/blob/master/Other_Elizas/20120310ShragerNorthEliza.c64basic

      var f = 369.ToBigInteger();
      var i = 1;

      System.Console.WriteLine(Flux.Services.Performance.Measure(() => f.SplitFactorial(), i));
      System.Console.WriteLine(Flux.Services.Performance.Measure(() => f.Factorial(), i));
      System.Console.WriteLine(Flux.Services.Performance.Measure(() => f.FactorialParallel(), i));
      System.Console.WriteLine(Flux.Services.Performance.Measure(() => f.GroupedFactorial(8), i));

      //int n = 4, r = 2;

      //var nCr = GenericMath.BinomialCoefficient(n, r);

      //System.Console.WriteLine($"n ={n}, r ={r}, nCr ={nCr}");

      //for (var i = 0; i < nCr; i++)
      //{
      //  var c = GenericMath.PermuteAlgorithm515(n, r, i);
      //  System.Console.WriteLine(string.Join(", ", c));
      //}

      return;

      //var r = 256;
      //var rbfl = r.BitFoldLeft();
      //var rbfr = r.BitFoldRight();
      //var rls1b = r.LeastSignificant1Bit();
      //var rms1b = r.MostSignificant1Bit();
      //var rlzc = r.LeadingZeroCount();
      //var rtzc = r.TrailingZeroCount();
      //var rbl = r.BitLength();
      //var rc1b = r.Count1Bits();
      //var ril2 = r.ILog2();
      //var rmb = r.MirrorBits();
      //var ripo2 = r.IsPowOf2();
      //var ripo10 = r.IsPowOf(10);
      //var ripo12 = r.IsPowOf(12);
      //var ripo16 = r.IsPowOf(16);


      return;

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
