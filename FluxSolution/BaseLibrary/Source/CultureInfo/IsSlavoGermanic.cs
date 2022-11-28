namespace Flux
{
  public static partial class CultureInfo
  {
    /// <summary>Indicates whether the name in the string can be considered of slavo/germanic origin.</summary>
    public static bool IsSlavoGermanic(this System.Globalization.CultureInfo source, string text)
      => text.AsSpan().IndexOfAny(new Flux.StringComparerEx(source, true), @"w", @"k", @"cz", @"witz") > -1;
  }
}
