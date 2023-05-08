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

      for (var bits = 8; bits <= 64; bits <<= 1)
        System.Console.WriteLine($"{bits:D4} = {Flux.Convert.BitCountToMaxDigitCount(bits, 10, true)} / {Flux.Convert.BitCountToMaxDigitCount(bits, 10, false)}");

      return;

      var e = 240;
      System.Console.WriteLine($"  {e.ToOctString()}");
      System.Console.WriteLine($"  {e.ToHexadecimalString()}");

      var me = -e;
      System.Console.WriteLine($" -{me.ToBinaryString()}");

      var re = (e.ReverseBits() & -e.ReverseBits()).ReverseBits();
      System.Console.WriteLine($"re{re.ToBinaryString()}");

      var em1 = e - 1;
      System.Console.WriteLine($"1-{em1.ToBinaryString()}");

      var ce = ~e;
      System.Console.WriteLine($" ~{ce.ToBinaryString()}");

      var mce = -~e;
      System.Console.WriteLine($"-~{mce.ToBinaryString()}");

      var a = (byte)0x55;
      var b = (byte)0xAA;
      var z = a.MortonNumber(b);

      var n = 6235854;

      var nlpo2 = n.NextLargestPowerOf2();
      var bi = n.ToBigInteger();
      var bis = bi.ToString("X8");
      var bl = bi.BitLength();
      var bln = bi.BitLengthN();
      var l2 = bi.IntegerLog2();
      var ms1b = bi.MostSignificant1Bit();
      var bmr = bi.BitMaskRight();
      var bmrs = bmr.ToString("X8");
      var bml = bi.BitMaskLeft();
      var bmls = bml.ToString("X8");
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
