namespace Flux.Text
{
  /// <summary>The functionality of this class relates to U+hhhh style formatting.</summary>
  public static partial class Unicode
  {
    /// <summary>Parses two characters representing the major and minor portions.</summary>
    public static UnicodeCategoryMajorMinorCode ParseCategoryMajorMinor(char codeMajor, char codeMinor)
      => ((UnicodeCategoryMajorMinorCode)System.Enum.Parse(typeof(UnicodeCategoryMajorMinorCode), $"{codeMajor}{codeMinor}"));
    /// <summary>Parses a string with both major and minor portions.</summary>
    public static UnicodeCategoryMajorMinorCode ParseCategoryMajorMinor(string codeMajorMinor)
      => (codeMajorMinor ?? throw new System.ArgumentNullException(nameof(codeMajorMinor))).Length == 2 ? ParseCategoryMajorMinor(codeMajorMinor[0], codeMajorMinor[1]) : throw new System.ArgumentOutOfRangeException(nameof(codeMajorMinor));
    /// <summary>Try to parse a string with both major and minor portions.</summary>
    public static bool TryParseCategoryMajorMinor(string categoryCode, out UnicodeCategoryMajorMinorCode result)
    {
      try
      {
        result = ParseCategoryMajorMinor(categoryCode);
        return true;
      }
      catch { }

      result = default;
      return false;
    }
  }
}
