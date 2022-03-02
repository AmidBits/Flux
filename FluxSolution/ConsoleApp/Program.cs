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

      var p1 = System.Linq.Enumerable.Range(2, 10000000).AsParallel().AsOrdered().Select(p => Flux.Numerics.PrimeNumber.GetPrimeFactors(p).Prepend(p).ToList()).ToArray();
      //foreach (var p in p1)
      //{
      //  System.Console.WriteLine(string.Join(',', p));
      //  System.Console.WriteLine();
      //}

      //var p2 = Flux.Numerics.PrimeNumber.GetAscendingPrimes(2).AsParallel().AsOrdered().PartitionTuple6(0, (p0, p1, p2, p3, p4, p5, i) => (p0, p1, p2, p3, p4, p5, i)).Where(list => list.p1 - list.p0 == 4 && list.p2 - list.p1 == 2 && list.p3 - list.p1 == 6 && list.p4 - list.p1 == 8 && list.p5 - list.p1 == 12).Take(70).ToArray();
      //System.Console.WriteLine(string.Join(System.Environment.NewLine, p2));

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
