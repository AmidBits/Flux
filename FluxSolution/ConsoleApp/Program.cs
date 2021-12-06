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

      //var type = typeof(Flux.Quantity.IValuedUnit<>);
      //System.Console.WriteLine(string.Join(System.Environment.NewLine, type.GetDerivedTypes()));

      Flux.Net.UdpCast.Chat(Flux.Net.UdpCast.MulticastTestEndPoint);

      var index = 0;
      foreach (var type in typeof(Flux.Locale).Assembly.GetTypes().OrderBy(t => t.FullName))
        //if (type.IsValueType && !type.IsEnum)
        //if (type.IsClass && !type.IsAbstract && !type.IsNested)
        if (!type.IsAbstract && !type.IsEnum && !type.IsNested && !type.IsNotPublic && type.GetInterfaces().Count() == 0)
          System.Console.WriteLine($"{++index:D3} : {type.FullName}");
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
