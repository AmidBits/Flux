﻿#if ZAMPLEZ
using System.Linq;

namespace Flux
{
  public static partial class Zamplez
  {
    /// <summary>Run the coordinate systems zample.</summary>
    public static void RunReflection()
    {
      System.Console.WriteLine(nameof(RunReflection));
      System.Console.WriteLine();

      Write(typeof(Flux.IValueSiBaseUnit<>));
      Write(typeof(Flux.IValueSiDerivedUnit<>));
      Write(typeof(Flux.IValueGeneralizedUnit<>));

      static void Write(System.Type type)
      {
        var implementations = type.GetDerivedTypes().OrderBy(t => t.Name).ToList();
        System.Console.WriteLine($"{type.Name} ({implementations.Count}) : {string.Join(", ", implementations)}");
        System.Console.WriteLine();
      }
    }
  }
}
#endif