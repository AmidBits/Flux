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
  public record struct Test
  {
    public int Trial { get; init; }
  }

  public class Program
  {
    private static void TimedMain(string[] args)
    {
      if (args.Length is var argsLength && argsLength > 0) System.Console.WriteLine($"Args ({argsLength}):{System.Environment.NewLine}{string.Join(System.Environment.NewLine, System.Linq.Enumerable.Select(args, s => $"\"{s}\""))}");

      if (Flux.Zamplez.IsSupported) { Flux.Zamplez.Run(); return; }

      var tt = new Test() { Trial = 7 };

      var type = typeof(Flux.Quantity.IValuedUnit<int>);//.GetGenericTypeDefinition();

      var ints2 = typeof(Flux.Reflect).Assembly.GetTypes().Where(t => t.GetTypeChain().Any(tc => (type.IsGenericTypeDefinition && tc.IsGenericType ? tc.GetGenericTypeDefinition() : tc) == type)).ToArray();
      var ints = Flux.Reflect.GetTypesDerivedFrom(type).ToArray();

      var ints3 = type.GetDerivedTypes().ToArray();
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
