using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Flux;

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

      

      var range = new Flux.ValueRangeEx<int>(7, 19);

      var il = Flux.Interpolation.LinearInterpolation<double, double>.Interpolate(range.Low, range.High, 0.45);

      var array = new int[] { 0x02, 0x08, 0x0a, 0x10 };

      foreach (var radix in array)
      {
        for (var value = 0; value <= (radix + 1); value++)
        {
          if (System.Math.Log(value, radix) is var logFp && logFp == double.NegativeInfinity)
            logFp = 0;

          var logAc = value.IntegerLogCeiling(radix);
          var logAf = value.IntegerLogFloor(radix);
          value.TryIntegerLog(radix, out var logBf, out var logBc);

          if (radix == 2)
          {
            var log2f = int.Log2(value);
            var log2c = int.IsPow2(value) ? log2f : log2f + 1;

            var lg2Ac = value.GetIntegerLog2Ceiling();
            var lg2Af = value.GetIntegerLog2Floor();

            System.Console.WriteLine($"{(value.IsIntegerPow(radix) ? radix.ToString().PadLeft(2, ' ') : "  ")} ILog{radix.ToSubscriptString(10).ToSequenceBuilder().PadLeft(2, 0.ToSubscriptString(10))}({value:D2}) : ({lg2Af:D2}) : [{logAf:D2}, {logBf:D2}] < {logFp:N3} ({log2f}, {log2c}) > [{logAc:D2}, {logBc:D2}] : ({lg2Ac:D2})");
          }
          else
            System.Console.WriteLine($"{(value.IsIntegerPow(radix) ? radix.ToString().PadLeft(2, ' ') : "  ")} ILog{radix.ToSubscriptString(10).ToSequenceBuilder().PadLeft(2, 0.ToSubscriptString(10))}({value:D2}) : [{logAf:D2}, {logBf:D2}] < {logFp:N3} > [{logAc:D2}, {logBc:D2}]");
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
