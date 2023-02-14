namespace Flux
{
  public static partial class ExtensionMethodsCultureInfo
  {
    /// <summary>Indicates whether the content of the string is possibly of slavo/germanic origin.</summary>
    public static bool IsSlavoGermanic(this System.Globalization.CultureInfo source, string text, System.StringComparison comparisonType) => new System.Collections.Generic.List<string>() { "w", "k", "cz", "witz" }.Any(sg => text.Contains(sg, comparisonType));
  }
}
