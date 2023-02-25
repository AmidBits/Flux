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

    private static void TimedMain(string[] args)
    {
      //if (args.Length is var argsLength && argsLength > 0) System.Console.WriteLine($"Args ({argsLength}):{System.Environment.NewLine}{string.Join(System.Environment.NewLine, System.Linq.Enumerable.Select(args, s => $"\"{s}\""))}");
      //if (Flux.Zamplez.IsSupported) { Flux.Zamplez.Run(); return; }

      static Flux.Numerics.CartesianCoordinate2<double> Poap(Flux.Numerics.CartesianCoordinate2<double> p, Flux.Numerics.CartesianCoordinate2<double> o, double r)
      {
        var po = p * o;
        var abspo = Flux.Numerics.CartesianCoordinate2<double>.Abs(po);
        var powabspo = abspo * abspo;
        var subpowabspo = powabspo - r;
        return subpowabspo;
      }

      var p = new Flux.Numerics.CartesianCoordinate2<double>(0.5, 2);
      var o = new Flux.Numerics.CartesianCoordinate2<double>(1, 1);
      var r = 6;

      var pp = Poap(p, o, r);

      var pi = Flux.Rational.Pi;
      var tau = Flux.Rational.Tau;
      var e = Flux.Rational.E;
      var t = new Flux.Rational(11) * new Flux.Rational(-12);

      var a = 10.5;
      var b = 2.5;
      var abm = a % b;
      var ab = a + b;

      var ar = Rational.ApproximateRational(a);
      var br = Rational.ApproximateRational(b);
      var abr = ar + br;

      var am3 = abr - new Rational(1, 5);

      var c = double.Pow(a, b);

      ar = -ar;

      var ars = ar.ToString();

      abr /= 2;

      ars = abr.ToString();

      var x = new Flux.Rational(3, 2);
      var y = new Flux.Rational(5, 2);
      var z = Flux.Rational.Pow(x, y); // new Flux.Rational(System.Numerics.BigInteger.Pow(x.Numerator, int.CreateChecked(y.Numerator)), System.Numerics.BigInteger.Pow(x.Denominator, int.CreateChecked(y.Denominator)), true);


      //var radix = 10.ToBigInteger();

      //for (var number = -3.ToBigInteger(); number > -100; number--)
      //{
      //  var sqrt = (number).IntegerSqrt();

      //  System.Console.WriteLine($"{number} : {sqrt}");
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
