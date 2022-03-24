#if ZAMPLEZ
using System.Linq;

namespace Flux
{
  public static partial class Zamplez
  {
    /// <summary>Run the coordinate systems zample.</summary>
    public static void RunReflection()
    {
      System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunReflection));
      System.Console.WriteLine();

      Write(typeof(Flux.ISiBaseUnitQuantifiable<,>));
      Write(typeof(Flux.ISiDerivedUnitQuantifiable<,>));
      Write(typeof(Flux.IUnitQuantifiable<,>), typeof(Flux.ISiDerivedUnitQuantifiable<,>), typeof(Flux.ISiBaseUnitQuantifiable<,>));
      Write(typeof(Flux.IQuantifiable<>), typeof(Flux.IUnitQuantifiable<,>));

      static void Write(System.Type type, params System.Type[] excludingTypes)
      {
        var exclusions = excludingTypes.SelectMany(type => type.GetDerivedTypes());
        var implementations = type.GetDerivedTypes().OrderBy(t => t.Name).Where(t => !exclusions.Contains(t)).ToList();
        System.Console.WriteLine($"{type.Name} ({implementations.Count}) : {string.Join(", ", implementations)}");
        System.Console.WriteLine();
      }
    }
  }
}
#endif
