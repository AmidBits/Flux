namespace Flux
{
  public static partial class General
  {
    /// <summary>Indicates whether the string content is considered of slavo/germanic origin.</summary>
    public static bool IsSlavoGermanic(this string source)
      => (source.IndexOfAny(System.StringComparison.OrdinalIgnoreCase, @"w", @"k", @"cz", @"witz") > -1);
  }
}
