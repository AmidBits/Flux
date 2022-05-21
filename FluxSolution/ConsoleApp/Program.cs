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

      var coord = Flux.GeographicCoordinate.TucsonAzUsa;
      Flux.Formatting.LatitudeFormatter.TryParse("40° 26′ 46″ N", out var lat);
      Flux.Formatting.LongitudeFormatter.TryParse("79° 58′ 56″ W", out var lon);
      coord = new Flux.GeographicCoordinate(lat, lon);

      System.Console.WriteLine($"{string.Format(new Flux.Formatting.LatitudeFormatter() { InsertSpaces = true, PreferUnicode = true }, @"{0:D3}", coord.Latitude.Value)} {string.Format(new Flux.Formatting.LongitudeFormatter() { InsertSpaces = true, PreferUnicode = true }, @"{0:D3}", coord.Longitude.Value)}");

      return;

      var s = "3\u00D7(\U0001F92D9\u22126)";

      var rt = new Flux.RuneTokenizer();
      var tet = new Flux.TextElementTokenizer();

      var rts = rt.GetTokens(s).ToArray();
      var tets = tet.GetTokens(s).ToArray();

      System.Console.WriteLine(string.Join(System.Environment.NewLine, rts.Select(t => t.ToString())));
      System.Console.WriteLine();
      System.Console.WriteLine(string.Join(System.Environment.NewLine, tets.Select(t => t.ToString())));
      System.Console.WriteLine();

      //var types = typeof(Flux.IMetricOneQuantifiable).GetDerivedTypes().OrderBy(t => t.Name).ToList();
      //foreach (var type in types)
      //{
      //  var instance = type.CreateInstance(1, null);
      //  System.Console.WriteLine(instance.ToString());
      //  System.Console.WriteLine(((Flux.IMetricOneQuantifiable)instance).ToMetricOneString(MetricMultiplicativePrefix.Milli, null, false, false));
      //}
      ////System.Console.WriteLine($"{type.Name} ({implementations.Count}) : {string.Join(", ", implementations)}");
      //System.Console.WriteLine();

      System.Console.WriteLine(((System.Text.Rune)0x00b0).ToString());

      var a = (Flux.IUnitQuantifiable<double, AngleUnit>)new Flux.Angle(1 / 60.0, AngleUnit.Degree);

      var r = a.ToUnitValue(AngleUnit.Arcminute);
      //var mr = a.ToMetricOneString(MetricMultiplicativePrefix.Micro);
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
