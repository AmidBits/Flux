using System;
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
      if (Flux.Zamplez.IsSupported) { Flux.Zamplez.Run(); return; }

      var source = "Robert";
      System.Console.WriteLine($"source: \"{source}\"");
      var target = "Rupert";
      System.Console.WriteLine($"target: \"{target}\"");

      var metric = new Flux.Metrical.OptimalStringAlignment<char>();
      var matrix = metric.GetMatrix(source, target);
      var btrack = metric.Backtrack(matrix, source, target, source.Length, target.Length, '-');
      var s = matrix.ToConsoleStrings();

      var (left, top) = System.Console.GetCursorPosition();
      var max1 = s.WriteToConsole(left, top);
      var max2 = s.WriteToConsole(max1.maxLeft + 3, top);

      System.Console.WriteLine($"btrack: \"{string.Concat(btrack)}\"");
    }

    private static void Main(string[] args)
    {
      var originalOutputEncoding = System.Console.OutputEncoding;

      try { System.Console.OutputEncoding = new System.Text.UnicodeEncoding(!System.BitConverter.IsLittleEndian, false); }
      catch { System.Console.OutputEncoding = System.Text.UnicodeEncoding.UTF8; }

      var percentOfLargestWindowSize = .95;
      var width = System.Math.Min(System.Math.Min(System.Convert.ToInt32(System.Console.LargestWindowWidth * percentOfLargestWindowSize), System.Console.LargestWindowWidth), short.MaxValue);
      var height = System.Math.Min(System.Math.Min(System.Convert.ToInt32(System.Console.LargestWindowHeight * percentOfLargestWindowSize), System.Console.LargestWindowHeight), short.MaxValue);

      System.Console.SetWindowSize(width, height);

      System.Console.WriteLine($"The console encoding is {System.Console.OutputEncoding.EncodingName} {System.Console.OutputEncoding.HeaderName.ToUpper()} (code page {System.Console.OutputEncoding.CodePage})");
      System.Console.WriteLine();

      System.Console.WriteLine(Flux.Services.Performance.Measure(() => TimedMain(args), 1));

      System.Console.OutputEncoding = originalOutputEncoding;

      System.Console.WriteLine($"{System.Environment.NewLine}Press any key to exit...");
      System.Console.ReadKey();
    }
  }
}
