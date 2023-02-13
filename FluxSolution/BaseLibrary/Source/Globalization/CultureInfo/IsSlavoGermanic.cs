namespace Flux
{
  public static partial class ExtensionMethodsCultureInfo
  {
    /// <summary>Indicates whether the content of the string is possibly of slavo/germanic origin.</summary>
    public static bool IsSlavoGermanic(this System.Globalization.CultureInfo source, string text) => text.AsSpan().IndexOfAny(new Flux.StringComparerEx(source ?? System.Globalization.CultureInfo.CurrentCulture, true), "w", "k", "cz", "witz") > -1;
  }
}
