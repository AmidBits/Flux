﻿#if ZAMPLEZ
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

      Write(typeof(Quantities.IUnitQuantifiable<,>));
      Write(typeof(Quantities.IQuantifiable<>), typeof(Quantities.IUnitQuantifiable<,>));

      static void Write(System.Type type, params System.Type[] excludingTypes)
      {
        var exclusions = excludingTypes.SelectMany(type => type.GetDerivedTypes());
        var implementations = type.GetDerivedTypes().OrderBy(t => t.Name).Where(t => !exclusions.Contains(t)).ToList();
        System.Console.WriteLine($"{type.Name} ({implementations.Count}) : {string.Join(", ", implementations.Select(i => i.Name))}");
        System.Console.WriteLine();
      }

      System.Console.WriteLine(string.Join(", ", typeof(Flux.Quantities.IQuantifiable<>).GetDerivedTypes().OrderBy(t => t.Name).Where(t => !t.IsInterface && !t.Name.Contains("Fraction")).Select(q => q.GetDefaultValue()?.ToString() ?? "Null")));
    }
  }
}
#endif