using System;

namespace Flux.Cultural
{
  public static partial class General
  {
    /// <summary>Indicates whether the name in the string can be considered of slavo/germanic origin.</summary>
    public static bool IsSlavoGermanic(this string source)
      => source.AsSpan().IndexOfAny(System.Collections.Generic.EqualityComparer<char>.Default, @"w", @"k", @"cz", @"witz") > -1;
    //=> ExtensionMethods.IndexOfAny(source, System.StringComparison.OrdinalIgnoreCase, @"w", @"k", @"cz", @"witz") > -1;
  }
}
