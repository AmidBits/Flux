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
// #r "C:\Users\Rob\source\repos\AmidBits\Flux\FluxSolution\BaseLibrary\bin\Debug\net6.0\BaseLibrary.dll"

namespace ConsoleApp
{
  public class Program
  {
    private static void TimedMain(string[] _)
    {
      //if (args.Length is var argsLength && argsLength > 0) System.Console.WriteLine($"Args ({argsLength}):{System.Environment.NewLine}{string.Join(System.Environment.NewLine, System.Linq.Enumerable.Select(args, s => $"\"{s}\""))}");
      //if (Zamplez.IsSupported) { Zamplez.Run(); return; }

      // At some point? https://github.com/jeffshrager/elizagen.org/blob/master/Other_Elizas/20120310ShragerNorthEliza.c64basic

      var radix = 10;
      for (var index = 0; index < 123; index += 1)
      {

        //System.Console.WriteLine($" {index.RoundToPowOf2TowardZero(true)} : {index} : {index.RoundToPowOf2AwayFromZero(true)}");
        //System.Console.WriteLine($" {index.IntegerLog2()} : {index:D2} : {(index > 0 ? (index - 1).IntegerLog2() + 1 : 0)}");
        System.Console.WriteLine($">{index.IntegerLog(radix)} : {index} : {(index >= 1 ? (index - 1).IntegerLog(radix) + 1 : 0)}");
        //System.Console.WriteLine();
      }

      return;

      //var ilog2 = (17).IntegerLogTz(2);
      //(17).LocateIntegerLog(2, out var ilogTowardZero, out var ilogAwayFromZero);

      var fp = 5.5;

      var raafz = fp.RoundAwayFromZero();
      var ratpi = fp.RoundToPositiveInfinity();
      var ratni = fp.RoundToNegativeInfinity();
      var ratz = fp.RoundTowardZero();
      var rhafz = fp.RoundHalfAwayFromZero();
      var rhte = fp.RoundHalfToEven();
      var rhtni = fp.RoundHalfToNegativeInfinity();
      var rhto = fp.RoundHalfToOdd();
      var rhtpi = fp.RoundHalfToPositiveInfinity();
      var rhtz = fp.RoundHalfTowardZero();

      var i = 5;

      var bfl = i.BitFoldLeft();
      var bfr = i.BitFoldRight();
      var ls1b = i.LeastSignificant1Bit();
      var ms1b = i.MostSignificant1Bit();
      var lzc = i.LeadingZeroCount();
      var tzc = i.TrailingZeroCount();
      var bl = i.BitLength();
      var c1b = i.Count1Bits();
      var il2 = i.IntegerLog2();
      var rb = i.ReverseBits();
      var ipo2 = i.IsPowOf2();
      var ipo10 = i.ToBigInteger().IsPowOf(10);
      var ipo12 = i.ToBigInteger().IsPowOf(12);
      var ipo16 = i.ToBigInteger().IsPowOf(16);

      var iaea = fp.EqualsWithinAbsoluteTolerance(i, 0.4);
      var iaer = fp.EqualsWithinRelativeTolerance(i, 0.05);
      var iaep = fp.EqualsWithinSignificantDigits(i, 1, 10);
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
