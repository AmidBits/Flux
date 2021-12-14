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
      var u = new Flux.SimpleFraction(355, 113);
      var v = new Flux.SimpleFraction(3, 20);
      var x = new Flux.SimpleFraction(20, 3);
      var y = new Flux.SimpleFraction(2);
      var z = x * y;
      var w = z % 4;

      System.Console.WriteLine($"{u.ToFractionString()}, {v.ToFractionString()}, {w.ToFractionString()}, {x.ToFractionString()}, {y.ToFractionString()}, {z.ToFractionString()}");

      var mp = new Flux.MetricPrefix(1, MetricPrefixUnit.Kilo);
      System.Console.WriteLine(mp.ToUnitString(MetricPrefixUnit.Kilo));
      System.Console.WriteLine(mp.ToUnitString(MetricPrefixUnit.None));
      System.Console.WriteLine(mp.ToUnitString(MetricPrefixUnit.Milli));

      var ppn = new Flux.PartsPerNotation(3, PartsPerNotationUnit.Hundred);
      System.Console.WriteLine(ppn.ToUnitString(PartsPerNotationUnit.Hundred));
      System.Console.WriteLine(ppn.ToUnitString(PartsPerNotationUnit.Thousand));
      System.Console.WriteLine(ppn.ToUnitString(PartsPerNotationUnit.TenThousand));
      System.Console.WriteLine(ppn.ToUnitString(PartsPerNotationUnit.HundredThousand));
      System.Console.WriteLine(ppn.ToUnitString(PartsPerNotationUnit.Million));
      System.Console.WriteLine(ppn.ToUnitString(PartsPerNotationUnit.Billion));

      //if (args.Length is var argsLength && argsLength > 0) System.Console.WriteLine($"Args ({argsLength}):{System.Environment.NewLine}{string.Join(System.Environment.NewLine, System.Linq.Enumerable.Select(args, s => $"\"{s}\""))}");
      //if (Flux.Zamplez.IsSupported) { Flux.Zamplez.RunTemporal(); return; }

      var value = System.Numerics.BigInteger.Parse("-17000000000000000000000000");
      System.Console.WriteLine($"{value}  = \"{Flux.Convert.ToNamedGrouping(value)}\"");
      System.Console.WriteLine();

      var type = typeof(Flux.IValueGeneralizedUnit<>);
      //      System.Console.WriteLine(string.Join(System.Environment.NewLine, type.GetDerivedTypes().OrderBy(t => t.FullName)));
      Write(typeof(Flux.IValueSiBaseUnit<>));
      Write(typeof(Flux.IValueSiDerivedUnit<>));
      Write(typeof(Flux.IValueGeneralizedUnit<>));
      int a = 1;

      static void Write(System.Type type)
      {
        var implementations = type.GetDerivedTypes().OrderBy(t => t.Name).ToList();
        System.Console.WriteLine($"{type.Name} ({implementations.Count}) : {string.Join(", ", implementations)}");
        System.Console.WriteLine();
      }

      System.Console.WriteLine(string.Join(System.Environment.NewLine, typeof(Flux.IValueGeneralizedUnit<>).GetDerivedTypes().OrderBy(t => t.Name).Where(t => t.GetMethods().Count(mi => mi.Name.Equals("ToUnitString")) == 0)));

      //var index = 0;
      //foreach (var type in typeof(Flux.Locale).Assembly.GetTypes().OrderBy(t => t.FullName))
      //  //if (type.IsValueType && !type.IsEnum)
      //  //if (type.IsClass && !type.IsAbstract && !type.IsNested)
      //  if (!type.IsAbstract && !type.IsEnum && !type.IsNested && !type.IsNotPublic && type.GetInterfaces().Count() == 0)
      //    System.Console.WriteLine($"{++index:D3} : {type.FullName}");

      var bst = Flux.DataStructures.Immutable.AvlTree<int, string>.Empty;

      for (var i = 0; bst.GetNodeCount() < 16; i++)
      {
        var r = System.Security.Cryptography.RandomNumberGenerator.GetInt32(0, 256);

        if (!bst.Contains(r))
          bst = bst.Add(r, Flux.Convert.ToNamedGrouping(r).ToString());
      }

      //bst = bst.Add(1, "One");
      //bst = bst.Add(2, "Two");
      //bst = bst.Add(3, "Three");
      //bst = bst.Add(4, "Four");
      //bst = bst.Add(5, "Five");
      //bst = bst.Add(6, "Six");
      //bst = bst.Add(7, "Seven");
      //bst = bst.Add(8, "Eight");
      //bst = bst.Add(9, "Nine");
      //bst = bst.Add(10, "Ten");
      //bst = bst.Add(11, "Eleven");
      //bst = bst.Add(12, "Twelve");
      //bst = bst.Add(13, "Thirteen");
      //bst = bst.Add(14, "Fourteen");
      //bst = bst.Add(15, "Fifteen");

      System.Console.WriteLine(bst.ToConsoleBlock());

      //var a = bst.Search(2);
      //var b = bst.Search(4);

      //foreach (var item in bst.GetNodesPostOrder())
      //  System.Console.WriteLine(item.Value);

      //bst = bst.Remove(1);
      //bst = bst.Remove(2);
      //bst = bst.Remove(3);
      //bst = bst.Remove(4);
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
