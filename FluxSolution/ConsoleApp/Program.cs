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


      var ints = new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 };

      var sum = ints.GetMaximumSumSubarray(out var startIndex, out var count);
      System.Console.WriteLine($"Total sum equals {sum} ({sum.ToNamedGrouping().ToLowerInvariant()}) from {count} values starting at index {startIndex} (ending index is {startIndex + count - 1}).");
      return;

      for (var n = 0; n <= 31; n++)
        System.Console.WriteLine($"{n}{Flux.ExtensionMethods.GetOrdinalIndicator(n)}");

      return;


      var ea = new Flux.EulerAngles(0, 0, Angle.ConvertDegreeToRadian(45));

      //var mtbZYX = ea.ToMatrixTaitBryanZYX();
      //System.Console.WriteLine(mtbZYX.ToArray().ToConsoleBlock(uniformWidth: true, centerContent: true));
      //System.Console.WriteLine();
      //var b = Flux.Matrix4x4.Invert(mtbZYX, out var mtbZYXi);
      //System.Console.WriteLine(mtbZYXi.ToArray().ToConsoleBlock(uniformWidth: true, centerContent: true));
      //System.Console.WriteLine();
      ////return;
      //var mtbYXZ = ea.ToMatrixTaitBryanYXZ();
      //System.Console.WriteLine(mtbYXZ.ToArray().ToConsoleBlock(uniformWidth: true, centerContent: true));
      //System.Console.WriteLine();
      var mpeZXZ = ea.ToMatrixLhProperEulerZXZ();
      var mtbZYX = ea.ToMatrixLhTaitBryanZYX();
      var mtbYXZ = ea.ToMatrixLhTaitBryanYXZ();
      var mpeZXZi = mpeZXZ.GetInverseGeneral();
      var mtbZYXi = mtbZYX.GetInverseGeneral();
      var mtbYXZi = mtbYXZ.GetInverseGeneral();
      System.Console.WriteLine(mpeZXZi.ToArray().ToConsoleBlock(uniformWidth: true, centerContent: true));
      System.Console.WriteLine();
      mpeZXZ.GetInverseOptimized(out var mpeZXZi2);
      mtbZYX.GetInverseOptimized(out var mtbZYXi2);
      mtbYXZ.GetInverseOptimized(out var mtbYXZi2);
      System.Console.WriteLine(mpeZXZi2.ToArray().ToConsoleBlock(uniformWidth: true, centerContent: true));
      System.Console.WriteLine();

      //var eatbZYX = mtbZYX.ToEulerAnglesTaitBryanZYX();
      ////var eatbYXZ = mtbZYX.ToEulerAnglesTaitBryanYXZ();
      //var eapeZXZ = mpeZXZ.ToEulerAnglesProperEulerZXZ();

      //var v = new Flux.Vector4(0, 0, 1, 0);

      var m = System.Numerics.Matrix4x4.CreateFromYawPitchRoll(0, 0, (float)Angle.ConvertDegreeToRadian(45));
      //System.Console.WriteLine(m.ToArray().ToConsoleBlock(uniformWidth: true, centerContent: true));
      //System.Console.WriteLine();

      //var vtbZYX = Flux.Vector4.Normalize(Flux.Vector4.Transform(v, mtbZYX));
      //var vtbYXZ = Flux.Vector4.Normalize(Flux.Vector4.Transform(v, mtbYXZ));
      //var vpeZXZ = Flux.Vector4.Normalize(Flux.Vector4.Transform(v, mpeZXZ));
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
