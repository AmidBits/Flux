namespace Flux
{
  public static partial class ExtensionMethodsCultureInfo
  {
    /// <summary>Indicates whether the content of the string is possibly of slavo/germanic origin.</summary>
    public static bool IsSlavoGermanic(this System.Globalization.CultureInfo source, string text, System.StringComparison comparisonType)
      => text.Contains('k', comparisonType) || text.Contains('w', comparisonType) || text.Contains("cz", comparisonType) || text.Contains("witz", comparisonType);
  }
}
