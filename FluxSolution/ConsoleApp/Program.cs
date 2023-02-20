using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Xml.Schema;
using Flux;
using Flux.ApproximateEquality;
using Flux.Formatting;
using Flux.Geometry;
using Flux.Interpolation;
using Flux.Quantities;
using Microsoft.VisualBasic.FileIO;

// C# Interactive commands:
// #r "System.Runtime"
// #r "System.Runtime.Numerics"
// #r "C:\Users\Rob\source\repos\Flux\FluxSolution\BaseLibrary\bin\Debug\net5.0\BaseLibrary.dll"

namespace ConsoleApp
{
  public class Program
  {
    // https://en.wikipedia.org/wiki/Azimuth
    public static void FindAzimuth(double lat1, double lon1, double lat2, double lon2, double flattening = 1 / 298.257223563, double eccentricity = 0.0167086)
    {
      var e2 = flattening * (2 - flattening);

      var inve2 = 1 - e2;

      var tlat2 = double.Tan(lat2);
      var tlat1 = double.Tan(lat1);

      var delta = inve2 * (tlat2 / tlat1) + e2 * double.Sqrt((1 + inve2 * double.Pow(tlat2, 2)) / (1 + inve2 * double.Pow(tlat1, 2)));

      var L = double.Abs(lon2 - lon1);

      var tanLat = double.Sin(L) / ((delta - double.Cos(L)) * tlat2);

      var atanLat = double.Atan(tanLat);

      var lat = Angle.ConvertRadianToDegree(atanLat);
    }

    private static int DecimalDigits(decimal number)
    {
      var fractionalPart = number - decimal.Truncate(number);

      var count = 0;

      while (fractionalPart.GetFraction(out int _) > 0)
      {
        count++;

        fractionalPart *= 10;
      }

      return count;
    }
    private static int DecimalDigits(double number)
      => DecimalDigits(System.Convert.ToDecimal(number));


    private static void TimedMain(string[] args)
    {
      //if (args.Length is var argsLength && argsLength > 0) System.Console.WriteLine($"Args ({argsLength}):{System.Environment.NewLine}{string.Join(System.Environment.NewLine, System.Linq.Enumerable.Select(args, s => $"\"{s}\""))}");
      //if (Flux.Zamplez.IsSupported) { Flux.Zamplez.Run(); return; }

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
