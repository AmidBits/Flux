namespace Flux.Globalization
{
  public static partial class General
  {
    /// <summary>Indicates whether the name in the string can be considered of slavo/germanic origin.</summary>
    public static bool IsSlavoGermanic(this string source)
      => Xtensions.IndexOfAny(source, System.StringComparison.OrdinalIgnoreCase, @"w", @"k", @"cz", @"witz") > -1;
  }
}
