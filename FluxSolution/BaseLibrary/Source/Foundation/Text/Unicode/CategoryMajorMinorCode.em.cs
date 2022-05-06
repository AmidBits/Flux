namespace Flux
{
  public static partial class Unicode
  {
    public static UnicodeCategoryMajorMinorCode ToMajorMinorCode(this System.Globalization.UnicodeCategory unicodeCategory)
      => (UnicodeCategoryMajorMinorCode)unicodeCategory;

    /// <summary>Converts a MajorMinorCode enum to a UnicodeCategory enum.</summary>
    public static System.Globalization.UnicodeCategory ToUnicodeCategory(this UnicodeCategoryMajorMinorCode categoryCode)
      => (System.Globalization.UnicodeCategory)categoryCode;
  }
}