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



      var ilog2 = (8.1).Log2F();

      for (var i = -5; i < 20; i++)
      {
        var tz = i.Pow2Tz(false);
        var afz = i.Pow2Afz(false);
        var ptz = i.Pow2Tz(true);
        var pafz = i.Pow2Afz(true);

        System.Console.WriteLine($"{i.ToString().PadLeft(3)} : {tz.ToString().PadLeft(3)} ({ptz.ToString().PadLeft(3)}) : {afz.ToString().PadLeft(3)} ({pafz.ToString().PadLeft(3)})");
      }
      return;

      var proper = true;

      var n = 8.ToBigInteger();

      var ms1b = n.MostSignificant1Bit();
      var ms1bp = ms1b == n ? ms1b : ms1b << 1;

      var pow2tz = proper && ms1b == n ? ms1b >> 1 : ms1b;

      var pow2afz = proper && ms1bp == n ? ms1bp << 1 : ms1bp;
      // need to use the above two lightweight functions. 

      var afz1 = n.Pow2Afz(false);
      var afz2 = n.Pow2Afz(true);

      var tz1 = n.Pow2Tz(false);
      var tz2 = n.Pow2Tz(true);

      670530.ToBigInteger().DigitMeta(2, out var count, out var digits, out var sum);

      var a4 = new Flux.Units.Frequency(440);

      var r = new Flux.Units.Ratio(3, 2);

      var r1200 = Flux.Music.Cent.ConvertCentToFrequencyRatio(700);

      var a5 = Flux.Units.Frequency.PitchShift(a4, new Flux.Music.Cent(700));

      var ar = Flux.Fraction.ApproximateRational(r1200);

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
