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

      var x = -16;
      var mn = x.NearestMultiple(3, true, RoundingMode.HalfToEven, out var mntz, out var mnafz);
      var p10n = x.NearestPowOf(10, true, RoundingMode.HalfToEven, out var p10ntz, out var p10nafz);
      var p2n = x.NearestPow2(true, RoundingMode.HalfToEven, out var p2ntz, out var p2nafz);

      return;

      var types = typeof(Flux.Locale).Assembly.GetTypes().Where(t => !t.IsNested && t.IsPublic).OrderBy(t => t.Name).ToArray();
      //types = System.Linq.Enumerable.Empty<System.Type>().Append(typeof(Flux.Voltage)).ToArray();

      foreach (var type in types)
      {
        var implements = type.GetImplements().Select(t => t.GetNameEx()).ToArray();
        var inheritance = type.GetInheritance().Select(t => t.GetNameEx()).ToArray();
        var derivedTypes = type.GetDerivedTypes().Select(t => t.GetNameEx()).ToArray();

        //if (inheritance.Length > 0 || derivedTypes.Length > 0 || implements.Length > 0)
        {
          System.Console.WriteLine($"{type.GetFullNameEx()}");
          if (inheritance.Length > 0)
            System.Console.WriteLine($" Inheritance:\r\n  {string.Join(" < ", inheritance)}");
          if (derivedTypes.Length > 0)
            System.Console.WriteLine($" Derived:\r\n  {string.Join("\r\n  ", derivedTypes)}");
          if (implements.Length > 0)
            System.Console.WriteLine($" Implements:\r\n  {string.Join(", ", implements)}");
          System.Console.WriteLine();
        }
      }

      //System.Console.WriteLine(System.Environment.ProcessorCount);
      //var fact = int.MaxValue.ToBigInteger();
      //System.Console.WriteLine(fact.FactorialEx());
      //return;


      //var ints1 = new Flux.Loops.Range<int>(0, 10, 1).GetSequence().ToArray();
      //var ints2 = new Flux.Loops.Range<int>(10, 5, -1).GetSequence().Append(new Flux.Loops.Range<int>(1, 5, 1).GetSequence()).ToArray();

      //var se1 = ints1.SliceEquals(10, ints2, 9, 11);
      //var se2 = ints1.SliceEquals(10, ints2, 11, 11);

      //var pcc = ints1.PearsonCorrelationCoefficient(i => i, ints2, i => i);
      //// (-0.75757575757575768, -6.25)
      //return;


      //var range = new Flux.ValueRangeEx<int>(7, 19);

      //var il = Flux.Interpolation.LinearInterpolation<double, double>.Interpolate(range.Low, range.High, 0.45);

      //var array = new int[] { 0x02, 0x08, 0x0a, 0x10 };

      //foreach (var radix in array)
      //{
      //  for (var value = 0; value <= (radix + 1); value++)
      //  {
      //    if (System.Math.Log(value, radix) is var logFp && logFp == double.NegativeInfinity)
      //      logFp = 0;

      //    var logAc = value.IntegerLogCeiling(radix);
      //    var logAf = value.IntegerLogFloor(radix);
      //    value.TryIntegerLog(radix, out var logBf, out var logBc);

      //    if (radix == 2)
      //    {
      //      var log2f = int.Log2(value);
      //      var log2c = int.IsPow2(value) ? log2f : log2f + 1;

      //      var lg2Ac = value.GetIntegerLog2Ceiling();
      //      var lg2Af = value.GetIntegerLog2Floor();

      //      System.Console.WriteLine($"{(value.IsIntegerPow(radix) ? radix.ToString().PadLeft(2, ' ') : "  ")} ILog{radix.ToSubscriptString(10).ToSequenceBuilder().PadLeft(2, 0.ToSubscriptString(10))}({value:D2}) : ({lg2Af:D2}) : [{logAf:D2}, {logBf:D2}] < {logFp:N3} ({log2f}, {log2c}) > [{logAc:D2}, {logBc:D2}] : ({lg2Ac:D2})");
      //    }
      //    else
      //      System.Console.WriteLine($"{(value.IsIntegerPow(radix) ? radix.ToString().PadLeft(2, ' ') : "  ")} ILog{radix.ToSubscriptString(10).ToSequenceBuilder().PadLeft(2, 0.ToSubscriptString(10))}({value:D2}) : [{logAf:D2}, {logBf:D2}] < {logFp:N3} > [{logAc:D2}, {logBc:D2}]");
      //  }

      //  System.Console.WriteLine();
      //}
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
