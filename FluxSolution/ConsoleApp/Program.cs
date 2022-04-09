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
      //if (Flux.Zamplez.IsSupported) { Flux.Zamplez.Run(); return; }

      var x = new System.Collections.Generic.List<double>() { 6, 7, 15, 36, 39, 40, 41, 42, 43, 47, 49 };
      //var x = new System.Collections.Generic.List<double>() { 7, 15, 36, 39, 40, 41 };

      System.Console.WriteLine($"Values = {string.Join(", ", x)}");

      System.Console.WriteLine($"Method 1: {x.QuartilesMethod1()}");
      System.Console.WriteLine($"Method 2: {x.QuartilesMethod2()}");
      //System.Console.WriteLine($"Method 3: {x.QuartileMethod3()}");
      System.Console.WriteLine($"Method 4: {x.QuartilesMethod4()}");

      for (var q = 0.25; q < 1; q += 0.25)
      {
        System.Console.WriteLine($"Q = {q}");
        var qr1 = Maths.Quantile(x, q, Maths.QuantileType.R1);
        System.Console.WriteLine($"R1 = {qr1}");
        var qr2 = Maths.Quantile(x, q, Maths.QuantileType.R2);
        System.Console.WriteLine($"R2 = {qr2}");
        var qr3 = Maths.Quantile(x, q, Maths.QuantileType.R3);
        System.Console.WriteLine($"R3 = {qr3}");
        var qr4 = Maths.Quantile(x, q, Maths.QuantileType.R4);
        System.Console.WriteLine($"R4 = {qr4}");
        var qr5 = Maths.Quantile(x, q, Maths.QuantileType.R5);
        System.Console.WriteLine($"R5 = {qr5}");
        var qr6 = Maths.Quantile(x, q, Maths.QuantileType.R6);
        System.Console.WriteLine($"R6 = {qr6}");
        var qr7 = Maths.Quantile(x, q, Maths.QuantileType.R7);
        System.Console.WriteLine($"R7 = {qr7}");
        var qr8 = Maths.Quantile(x, q, Maths.QuantileType.R8);
        System.Console.WriteLine($"R8 = {qr8}");
        var qr9 = Maths.Quantile(x, q, Maths.QuantileType.R9);
        System.Console.WriteLine($"R9 = {qr9}");
      }

      return;

      System.Console.Write($"It's a ");
      Flux.Console.WriteError($"{nameof(Flux.Console.WriteError)}");
      System.Console.Write($" and some ");
      Flux.Console.WriteInformation($"{nameof(Flux.Console.WriteInformation)}");
      System.Console.Write($" with a bit of ");
      Flux.Console.WriteSuccess($"{nameof(Flux.Console.WriteSuccess)}");
      System.Console.Write($" as well as a ");
      Flux.Console.WriteWarning($"{nameof(Flux.Console.WriteWarning)}");
      System.Console.Write($".");

      //foreach (System.ConsoleColor color in System.Enum.GetValues(typeof(System.ConsoleColor)))
      //{
      //  System.Console.ForegroundColor = color;
      //  System.Console.WriteLine(color.ToString());
      //  System.Console.ResetColor();
      //}
      //Flux.Zamplez.RunReflection();
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
