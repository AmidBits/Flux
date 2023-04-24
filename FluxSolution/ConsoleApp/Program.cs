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
    /// <summary>
    /// Permutation indices of algorithm 515.
    /// <para><see href="https://github.com/sleeepyjack/alg515"/></para>
    /// <para><seealso href="https://stackoverflow.com/questions/561/how-to-use-combinations-of-sets-as-test-data#794"/></para>
    /// </summary>
    /// <param name="n"></param>
    /// <param name="p"></param>
    /// <param name="l"></param>
    /// <returns></returns>
    public static int[] PermuteAlgorithm515(int n, int p, int l)
    {
      var c = new int[p];
      var x = 1;
      var r = Binom(n - x, p - 1);
      var k = r;

      while (k <= l)
      {
        x++;
        r = Binom(n - x, p - 1);
        k += r;
      }

      k -= r;
      c[0] = x - 1;

      for (var i = 2; i < p; i++)
      {
        x++;
        r = Binom(n - x, p - i);
        k += r;

        while (k <= l)
        {
          x++;
          r = Binom(n - x, p - i);
          k += r;
        }

        k -= r;

        c[i - 1] = x - 1;
      }

      c[p - 1] = x + l - k;

      return c;
    }

    public static int Binom(int n, int k)
    {
      var k1 = k;
      var p = n - k1;

      if (k1 < p)
      {
        p = k1;
        k1 = n - p;
      }

      var r = p == 0 ? 1 : k1 + 1;

      for (var i = 2; i <= p; i++)
        r = (r * (k1 + i)) / i;

      return r;
    }

    private static void TimedMain(string[] _)
    {
      //if (args.Length is var argsLength && argsLength > 0) System.Console.WriteLine($"Args ({argsLength}):{System.Environment.NewLine}{string.Join(System.Environment.NewLine, System.Linq.Enumerable.Select(args, s => $"\"{s}\""))}");
      //if (Zamplez.IsSupported) { Zamplez.Run(); return; }

      // At some point? https://github.com/jeffshrager/elizagen.org/blob/master/Other_Elizas/20120310ShragerNorthEliza.c64basic

      int n = 5, r = 3;

      var nCr = Binom(n, r);

      System.Console.WriteLine($"n ={n}, r ={r}, nCr ={nCr}");

      for (var i = 0; i < nCr; i++)
      {
        var c = PermuteAlgorithm515(n, r, i);
        System.Console.WriteLine(string.Join(", ", c));
      }

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
