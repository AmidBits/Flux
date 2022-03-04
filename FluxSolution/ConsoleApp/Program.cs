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

      var ba = new Flux.BitArray(long.MaxValue);
      //ba.SetBit(32, true);
      //ba.SetByte(4, 1);
      //ba.SetUInt16(2, 1);
      //ba.SetUInt32(1, 1);
      //ba.SetUInt64(0, 1UL << 32);
      ba[32] = true;
      ba[33] = true;
      ba[34] = true;
      ba[35] = true;
      ba[36] = true;
      ba[37] = true;
      ba[38] = true;
      ba[39] = true;
      //ba.SetInt64(0, -1);
      //ba.SetByte(1, 5);
      var i64 = ba.GetUInt64(0);
      var i32 = ba.GetUInt32(1);
      var i16 = ba.GetUInt16(2);
      var i8 = ba.GetByte(4);
      var i1 = ba.GetBit(32);
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
