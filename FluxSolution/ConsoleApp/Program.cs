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


      var ea = new Flux.EulerAngles(0, 0, Angle.ConvertDegreeToRadian(45));

      var mtb = ea.ToMatrixTaitBryanZYX();
      var mtbYXZ = ea.ToMatrixTaitBryanYXZ();
      var mpe = ea.ToMatrixProperEulerZXZ();

      var eatb = mtb.ToEulerAnglesTaitBryanZYX();
      var eape = mpe.ToEulerAnglesProperEulerZXZ();

      var v = new Flux.Vector4(0, 0, 1, 0);

      var m = System.Numerics.Matrix4x4.CreateFromYawPitchRoll(0, 0, (float)Angle.ConvertDegreeToRadian(45));

      var vtb = Flux.Vector4.Normalize(Flux.Vector4.Transform(v, mtb));
      var vpe = Flux.Vector4.Normalize(Flux.Vector4.Transform(v, mpe));
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
