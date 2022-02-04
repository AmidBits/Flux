using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Intrinsics;

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

      var cc235 = new Flux.CartesianCoordinate3(2, 3.5, 5);
      var cc357 = new Flux.CartesianCoordinate3(3.5, 5, 7);
      var cp1 = Flux.CartesianCoordinate3.CrossProduct(cc235, cc357);
      var cp2 = cc235.ToVector256().CrossProduct3D(cc357.ToVector256());
      System.Console.WriteLine($"{cp1}");
      System.Console.WriteLine($"{cp2}");

      var x = 0.7854;
      var y = 0.1;
      var z = 0;


      var aa = new Flux.AxisAngle(1, 0, 0, Angle.ConvertDegreeToRadian(90));
      var aaq = aa.ToQuaternion();
      var aaqn = aaq.Normalized();
      var aaea = aa.ToEulerAngles();
      
      var ea = aaq.ToEulerAngles();
      var eaq = ea.ToQuaternion();
      var eaaa = ea.ToAxisAngle();

      var acos = System.Math.Acos(1);

      var q1 = new Flux.Numerics.Quaternion(-4, 4, 4, 1);
      var l1 = q1.Length();
      var q2 = Flux.Numerics.Quaternion.Normalize(q1);

      var dbl = 25d / double.Epsilon;

      var q3 = q1 * q1.Inverse();
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
