using Flux;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ConsoleApp
{
  class Program
  {
    private static void TimedMain(string[] _)
    {
      //using var fs = System.IO.File.OpenRead(@"C:\Test\Geoposition.cs");
      //using var ms = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(@"𐓏𐓘𐓻𐓘𐓻𐓟 𐒻𐓟"));
      //using var sr = new System.IO.StreamReader(ms, System.Text.Encoding.UTF8);

      //var index = 0;
      //foreach (var rune in sr.ReadRunes())
      //  System.Console.WriteLine($"{++index} {rune} : 0x{rune.Value:X4}");

      var a = @"a cat";
      var b = @"a abct";

      var lcs = new Flux.SequenceMetrics.DamerauLevenshteinDistance<char>();

      System.Console.WriteLine(lcs.GetMetricDistance(a, b));
      var grid = lcs.GetGrid(a, b, 0.5, 0.5, 0.5, 0.5);
      System.Console.WriteLine(grid.ToConsoleString2d((e, i) => $"{e:N1}", ' ', '\0'));
      //var index = 0;
      //foreach (var item in lcs.GetList(a, b))
      //  System.Console.WriteLine($"{++index} {item}");
    }

    static void Main(string[] args)
    {
      System.Console.InputEncoding = System.Text.Encoding.UTF8;
      System.Console.OutputEncoding = System.Text.Encoding.UTF8;

      System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => TimedMain(args), 1));

      System.Console.WriteLine($"{System.Environment.NewLine}Press any key to exit...");
      System.Console.ReadKey();
    }
  }
}
