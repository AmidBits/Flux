namespace Flux
{
  /// <summary>The functionality of this class relates to U+hhhh style formatting.</summary>
  public static partial class UnicodeEm
  {
    public static Text.UnicodeCategoryMajorMinorCode ToMajorMinorCode(this System.Globalization.UnicodeCategory unicodeCategory)
      => (Text.UnicodeCategoryMajorMinorCode)unicodeCategory;

    /// <summary>Converts a MajorMinorCode enum to a UnicodeCategory enum.</summary>
    public static System.Globalization.UnicodeCategory ToUnicodeCategory(this Text.UnicodeCategoryMajorMinorCode categoryCode)
      => (System.Globalization.UnicodeCategory)categoryCode;
  }
}