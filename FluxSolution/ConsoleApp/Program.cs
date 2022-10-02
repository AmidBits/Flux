using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Flux;
using Flux.DataStructures;
using Flux.Numerics;

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

      var array = new int[] { 2, 8, 10, 16 };

      foreach (var radix in array)
      {
        for (var value = 0; value <= (radix + 1); value++)
        {
          if (System.Math.Log(value, radix) is var logfp && logfp == double.NegativeInfinity)
            logfp = 0;

          var logac = value.IntegerLogCeiling(radix);
          var logaf = value.IntegerLogFloor(radix);
          value.TryGetIntegerLog(radix, out var logbf, out var logbc);

          if (radix == 2)
          {
            var lg2ac = value.GetIntegerLog2Ceiling();
            var lg2af = value.GetIntegerLog2Floor();
            value.TryGetIntegerLog2(out var lg2bf, out var lg2bc);

            System.Console.WriteLine($"{(value.IsPow(radix) ? radix.ToString().PadLeft(2, ' ') : "  ")} {value:D2}|{radix} : ({lg2af:D2}, {lg2bf:D2}) : [{logaf:D2}], {logbf:D2}] < {logfp:N3} > [{logac:D2}, {logbc:D2}] : ({lg2ac:D2}, {lg2bc:D2})");
          }
          else
            System.Console.WriteLine($"{(value.IsPow(radix) ? radix.ToString().PadLeft(2, ' ') : "  ")} {value:D2}|{radix} : [{logaf:D2}, {logbf:D2}] < {logfp:N3} > [{logac:D2}, {logbc:D2}]");
        }

        System.Console.WriteLine();
      }
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
