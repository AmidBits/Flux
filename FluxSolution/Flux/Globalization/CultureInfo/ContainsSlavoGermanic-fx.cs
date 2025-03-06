namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Indicates whether the content of the string is possibly of slavo/germanic origin.</summary>
    public static bool ContainsSlavoGermanic(this System.Globalization.CultureInfo source, string text, System.StringComparison comparisonType)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return text.Contains('k', comparisonType) || text.Contains('w', comparisonType) || text.Contains("cz", comparisonType) || text.Contains("witz", comparisonType);
    }
  }
}
