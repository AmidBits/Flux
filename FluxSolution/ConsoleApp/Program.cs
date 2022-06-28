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

      var bmr = new Flux.SpanBuilder<char>("bbaaccaadd");
      var bmr2 = (Span<char>)bmr.ToString(0).ToCharArray();
      System.Console.WriteLine($"{nameof(Flux.BoothsAlgorithm)} = {bmr.FindMinimalRotationB()}");


      var hw = new Flux.SpanBuilder<char>("Hello 2 World");

      var rsb = hw.ToRuneSpanBuilder();
      rsb.Reverse();
      var hw2 = rsb.ToCharSpanBuilder();

      //                                   01234567890

      System.Console.WriteLine($"CountEqualAtEnd = {hw.AsReadOnlySpan().CountEqualAtEnd(" World")}");
      System.Console.WriteLine($"CountEqualAtStart = {hw.AsReadOnlySpan().CountEqualAtStart("Hello ")}");
      System.Console.WriteLine($"EndsWith = {hw.AsReadOnlySpan().EndsWith(" World")}");
      System.Console.WriteLine($"EqualsAt = {hw.AsReadOnlySpan().EqualsAt(5, " ")}");
      System.Console.WriteLine($"{nameof(Flux.BoyerMooreHorspoolAlgorithm)} = {hw.FindIndexBMH("l")}");
      System.Console.WriteLine($"{nameof(Flux.KnuthMorrisPrattAlgorithm)} = {string.Join(',', hw.FindIndicesKMP("l"))}");
      System.Console.WriteLine($"GetExtremum = {hw.AsReadOnlySpan().GetExtremum(v => v)}");
      System.Console.WriteLine($"GetInfimumAndSupremum = {hw.AsReadOnlySpan().GetInfimumAndSupremum('o', v => v)}");
      System.Console.WriteLine($"IndexOf = {hw.AsReadOnlySpan().IndexOf(" ")}");
      System.Console.WriteLine($"IndexOfAny = {hw.AsReadOnlySpan().IndexOfAny(new char[] { 'd', 'r' })}");
      System.Console.WriteLine($"IndicesOfAll = {string.Join(',', hw.AsReadOnlySpan().IndicesOfAll(new char[] { 'd', 'r', 'l' }))}");
      hw.MakeNumbersFixedLength(4);
      System.Console.WriteLine($"MakeIntegersFixedLength = {hw.AsReadOnlySpan()}");
      hw.InsertOrdinalIndicatorSuffix((s1, s2) => true);
      System.Console.WriteLine($"InsertOrdinalIndicatorSuffix = {hw.AsReadOnlySpan()}");
      System.Console.WriteLine($"StartsWith = {hw.AsReadOnlySpan().StartsWith("Hello ")}");
      hw.PadRight(20, '-');
      System.Console.WriteLine($"PadRight(20) = {hw.AsReadOnlySpan()}");
      hw.PadLeft(23, '-');
      System.Console.WriteLine($"PadLeft(23) = {hw.AsReadOnlySpan()}");
      hw.PadEven(28, "LEFT", "RIGHT");
      System.Console.WriteLine($"PadEven(28) = {hw.AsReadOnlySpan()}");

      hw.AsReadOnlySpan().CircularCopyTo(bmr2, 0, hw.Length);

      var map = hw.AsReadOnlySpan().CreateIndexMap(item => item);

      hw.RemoveAll(new char[] { 'l' });

      bmr.Reverse();
      System.Console.WriteLine($"Reverse = {bmr.AsReadOnlySpan()}");

      bmr.Repeat(1);

      //      var x = new Flux.SpanBuilder<int>(new int[] { 2, 3, 5, 5, 7, 11 });

      var x = new Flux.SpanBuilder<char>("Hello    World!");
      var y = x.Clone();
      x.NormalizeAdjacent(new char[] { ' ' });
      //var w = MemoryExtensions.IndexOf(y, 7);
      //var z = MemoryExtensions.IndexOf(y, new int[] { 5, 7 });


      var tle = new TwoLineElementSet2() { Inclination = 51.6416, RightAscensionOfAscendingNode = 247.4627, Eccentricity = 0.0006703, ArgumentOfPerigee = 130.5360, MeanAnomaly = 325.0288, MeanMotion = 15.72125391 };


      var g = new Flux.Model.Grid<int>(10, 10);
      g[2, 7] = 'A';
      g[5, 4] = 'X';
      System.Console.WriteLine(g.ToConsoleBlock(v => v == default ? "\u00B7" : ((System.Text.Rune)v).ToString()));
    }

    private static void Main(string[] args)
    {
      var originalOutputEncoding = SetEncoding();

      SetSize(0.9);

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
