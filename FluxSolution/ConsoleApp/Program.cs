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


      var sh = new Flux.DataStructures.Statistics.SimpleHistogram<string>();
      for (var i = 0; i < 100; i++)
      {
        var lo = i / 10 * 10 + 1;
        var hi = lo + 9;
        sh.Add($"{lo}-{hi}", i);
      }
      System.Console.WriteLine(sh.Count);
      System.Console.WriteLine(sh.Frequencies);
      System.Console.WriteLine(string.Join(',', sh.Keys));
      System.Console.WriteLine(string.Join(',', sh.Values));

      var sb1 = new Flux.SpanBuilder<char>();
      var sb2 = new Flux.SequenceBuilder<char>();

      sb1.Append("Hello World!");
      sb2.Append("Hello World!");

      sb1.Repeat(1);
      sb2.Repeat(1);

      System.Console.WriteLine(sb1.AsReadOnlySpan().ToString());
      System.Console.WriteLine(sb2.AsReadOnlySpan().ToString());

      sb1.Insert(0, "1234567890");
      sb2.Insert(0, "1234567890");

      System.Console.WriteLine(sb1.AsReadOnlySpan().ToString());
      System.Console.WriteLine(sb2.AsReadOnlySpan().ToString());

      sb1.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
      sb2.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ");

      System.Console.WriteLine(sb1.AsReadOnlySpan().ToString());
      System.Console.WriteLine(sb2.AsReadOnlySpan().ToString());

      sb1.Remove(10, 12);
      sb2.Remove(10, 12);

      System.Console.WriteLine(sb1.AsReadOnlySpan().ToString());
      System.Console.WriteLine(sb2.AsReadOnlySpan().ToString());

      sb1.Insert(0, "ZYXWVUTSRQPONMLKJIHGFEDCBA");
      sb2.Insert(0, "ZYXWVUTSRQPONMLKJIHGFEDCBA");

      System.Console.WriteLine(sb1.AsReadOnlySpan().ToString());
      System.Console.WriteLine(sb2.AsReadOnlySpan().ToString());

      sb1.Append("0987654321");
      sb2.Append("0987654321");

      System.Console.WriteLine(sb1.AsReadOnlySpan().ToString());
      System.Console.WriteLine(sb2.AsReadOnlySpan().ToString());


      var g = new Flux.Model.Grid<int>(10, 10);
      g[2, 7] = 'A';
      g[5, 4] = 'X';
      System.Console.WriteLine(g.ToConsoleBlock(v => v == default ? "\u00B7" : ((System.Text.Rune)v).ToString()));
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
