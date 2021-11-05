using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

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
      if (args.Length is var argsLength && argsLength > 0) { System.Console.WriteLine($"Args ({argsLength}):{System.Environment.NewLine}{string.Join(System.Environment.NewLine, System.Linq.Enumerable.Select(args, s => $"\"{s}\""))}"); }

      // if (Flux.Zamplez.IsSupported) { Flux.Zamplez.Run(); return; }



      var jdc = new Flux.JulianDayNumber();
      var jcs = jdc.ToDateString(ConversionCalendar.JulianCalendar);
      var gcs = jdc.ToDateString(ConversionCalendar.GregorianCalendar);

      var gc = System.DateTime.Now.ToJulianDate(ConversionCalendar.GregorianCalendar);
      var gcd = System.Math.Floor(gc.Value - 2299159.5);
      var jc = System.DateTime.Now.ToJulianDate(ConversionCalendar.JulianCalendar);
      var jde = new Flux.JulianDayNumber(1994, 08, 25, ConversionCalendar.GregorianCalendar);
    }

    private static void Main(string[] args)
    {
      var originalOutputEncoding = System.Console.OutputEncoding;

      try { System.Console.OutputEncoding = new System.Text.UnicodeEncoding(!System.BitConverter.IsLittleEndian, false); }
      catch { System.Console.OutputEncoding = System.Text.UnicodeEncoding.UTF8; }

      System.Console.WriteLine($"The console encoding is {System.Console.OutputEncoding.EncodingName} {System.Console.OutputEncoding.HeaderName.ToUpper()} (code page {System.Console.OutputEncoding.CodePage})");
      System.Console.WriteLine();

      System.Console.WriteLine(Flux.Services.Performance.Measure(() => TimedMain(args), 1));

      System.Console.OutputEncoding = originalOutputEncoding;

      System.Console.WriteLine($"{System.Environment.NewLine}Press any key to exit...");
      System.Console.ReadKey();
    }
  }
}
