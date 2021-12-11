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
      //if (Flux.Zamplez.IsSupported) { Flux.Zamplez.RunTemporal(); return; }

      //var value = -17L;
      //System.Console.WriteLine($"{value}  = \"{Flux.Convert.IntegerToWords(value)}\"");
      //System.Console.WriteLine();

      var type = typeof(Flux.IUnitValueGeneralized<>);
      System.Console.WriteLine(string.Join(System.Environment.NewLine, type.GetDerivedTypes()));

      //var index = 0;
      //foreach (var type in typeof(Flux.Locale).Assembly.GetTypes().OrderBy(t => t.FullName))
      //  //if (type.IsValueType && !type.IsEnum)
      //  //if (type.IsClass && !type.IsAbstract && !type.IsNested)
      //  if (!type.IsAbstract && !type.IsEnum && !type.IsNested && !type.IsNotPublic && type.GetInterfaces().Count() == 0)
      //    System.Console.WriteLine($"{++index:D3} : {type.FullName}");

      var bst = Flux.DataStructures.Immutable.AvlTree<int, string>.Empty;

      for(var i = 0; i < 8; i++)
      {
        var r = System.Security.Cryptography.RandomNumberGenerator.GetInt32(0, 100);

        bst = bst.Add(r, Flux.Convert.IntegerToWords(r).ToString());
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
